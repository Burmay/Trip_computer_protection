using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Game
{
    public static SceneManagerBase sceneManager { get; private set; }

    public static event EventHandler OnGameInitializedEvent;

    public static void Run()
    {
        sceneManager = new SceneManagerMain();
        Coroutines.StartRoutine(InitializeGameRoutine());
    }

    private static IEnumerator InitializeGameRoutine()
    {
        sceneManager.InitScenesRoll();
        yield return sceneManager.LoadCurrentSceneAsync();
        if(OnGameInitializedEvent != null) { OnGameInitializedEvent(new object(), new EventArgs()); }
    }

    public static T GetRepository<T>() where T : Repository
    {
        return sceneManager.GetRepository<T>();
    }

    public static T GetInteractor<T>() where T : Interactor
    {
        return sceneManager.GetInteractor<T>(); 
    }
}
