using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WaweData
{
    public static int enemyCount;
    public static TestEnemy TestEnemy;

    static WaweData()
    {
        TestEnemy = new TestEnemy();
    }

    public static void WaveStats(int waveNumber)
    {
        switch (waveNumber)
        {
            case 1:
                TestEnemy.Count = 15;
                break;
            case 2:
                TestEnemy.Count = 25;
                break;
            case 3:
                TestEnemy.Count = 30;
                break;
            case 4:
                TestEnemy.Count = 45;
                break;
            case 5:
                TestEnemy.Count = 60;
                break;
        }
    }
}

enum EnemyTypes
{
    TestEnemy
}
