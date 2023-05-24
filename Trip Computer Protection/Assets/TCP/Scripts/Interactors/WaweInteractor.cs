using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaweInteractor : Interactor
{
    private List<GameObject> enemies;
    public List<GameObject> Enemies
    {
        get
        {
            if(enemies == null) return null;
            enemies.RemoveAll(s => s == null); return enemies;
        }
        private set { enemies = value; }
    }

    float timeBetweenWave = 5f;
    float timeBetweenSpawn = 1f;
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
        spawnRoutine = Coroutines.StartRoutine(SpawnEnemyRoutine());
    }


    private void StartSpawnRoutine()
    {
        spawnRoutine = Coroutines.StartRoutine(SpawnEnemyRoutine());
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        for(int i = 0; i < timeBetweenSpawn; i++)
        {
            yield return new WaitForSeconds(timeBetweenSpawn);
            enemies.Add(Instantiate(enemyPtefab, spawnPointT.position, spawnPointT.rotation));
        }

        //
        ReloadSpawnRoutine();
    }


    void ReloadSpawnRoutine()
    {
        Coroutines.StopRoutine(spawnRoutine);
        StartSpawnRoutine();
    }

    //private void ActionOn(Action action)
    //{
    //    if(action != null) action.Invoke();
    //}
}
