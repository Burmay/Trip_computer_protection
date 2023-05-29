using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaweInteractor : Interactor
{
    bool onSpawnEnd = false;

    private List<GameObject> enemies;
    public List<GameObject> Enemies
    {
        get
        {
            if (enemies == null) return null;
            enemies.RemoveAll(s => s == null);
            if (enemies.Count == 0 && onSpawnEnd == true) { EndWawe(); return null; }
            return enemies;
        }
        private set { enemies = value; }
    }

    float timeBetweenSpawn = 0.2f;
    float countEnemy = 80f;

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
        var obj = GameObject.FindGameObjectWithTag("SpawnPoint");
        spawnPointT = obj.transform;
        enemyPtefab = Resources.Load<GameObject>(ENEMY_PATH);
        // test
        StartWawe();
    }


    public void StartWawe()
    {
        onSpawnEnd = false;
        spawnRoutine = Coroutines.StartRoutine(SpawnEnemyRoutine());
    }

    private void EndWawe()
    {
        Debug.Log("Tactical phase ON");
    }

    private void ShowBuildMenu()
    {

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
