using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class WaweInteractor : Interactor
{
    int waweNumber;
    float timeBetweenSpawn = 0.2f;
    int countEnemy = 0;
    bool onSpawnEnd = false;
    bool tacticalPhase = true;

    private List<GameObject> enemies;
    public List<GameObject> Enemies
    {
        get
        {
            if (enemies == null) { EndWawe(); return null; }
            enemies.RemoveAll(s => s == null);
            if (enemies.Count == 0 && onSpawnEnd == true) { EndWawe(); return null; }
            return enemies;
        }
        private set { enemies = value; }
    }

    BuildInteractor buildInteractor;
    Transform spawnPointT;
    string ENEMY_PATH = "Prefabs/Enemy/Enemy";
    GameObject enemyPtefab;
    Coroutine spawnRoutine;

    public override void OnCreate()
    {
        enemies = new List<GameObject>();
    }

    public override void OnStart()
    {
        Network();
        waweNumber = 0;
        // test
    }

    void Network()
    {
        enemyPtefab = Resources.Load<GameObject>(ENEMY_PATH);
        buildInteractor = Game.GetInteractor<BuildInteractor>();
        var obj = GameObject.FindGameObjectWithTag("SpawnPoint");
        spawnPointT = obj.transform;

        GlobalEventManager.TacticalPhaseOn += ReloadTurrets;
        GlobalEventManager.TacticalPhaseOff += StartWawe;
    }


    public void StartWawe()
    {
        waweNumber++;
        WaweData.WaveStats(waweNumber);
        countEnemy = WaweData.TestEnemy.Count;
        tacticalPhase = false;
        onSpawnEnd = false;
        spawnRoutine = Coroutines.StartRoutine(SpawnEnemyRoutine());
    }

    private void EndWawe()
    {
        if (tacticalPhase) return;

        Debug.Log("Tactical phase ON");
        GlobalEventManager.SendTacticalPhaseOn();

        tacticalPhase = true;
    }

    void ReloadTurrets()
    {
        foreach(TurretBase turret in buildInteractor.turrets)
        {
            turret.WaweReload();
        }
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        for(int i = 0; i < countEnemy; i++)
        {
            yield return new WaitForSeconds(timeBetweenSpawn);
            enemies.Add(Instantiate(enemyPtefab, spawnPointT.position, spawnPointT.rotation));
        }

        onSpawnEnd = true;
        Coroutines.StopRoutine(spawnRoutine);
    }

    //private void ActionOn(Action action)
    //{
    //    if(action != null) action.Invoke();
    //}
}
