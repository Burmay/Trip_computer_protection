using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractorsBase
{
    private Dictionary<Type, Interactor> interactorsRoll;
    private SceneConfig sceneConfig;

    public InteractorsBase(SceneConfig sceneConfig)
    {
        this.sceneConfig = sceneConfig;
    }

    public void CreateAllInteractors()
    {
        this.interactorsRoll = this.sceneConfig.CreateAllInteractors();
    }

    public void SendOnCreateToAllInteractors()
    {
        var allInteractors = this.interactorsRoll.Values;
        foreach(var interactor in allInteractors)
        {
            interactor.OnCreate();
        }
    }

    public void InitializeAllInteractors()
    {
        var allInteractors = this.interactorsRoll.Values;
        foreach (var interactor in allInteractors)
        {
            interactor.Initialize();
        }
    }

    public void SendOnStartToAllInteractors()
    {
        var allInteractors = this.interactorsRoll.Values;
        foreach (var interactor in allInteractors)
        {
            interactor.OnStart();
        }
    }

    public T GetInteractor <T>() where T : Interactor
    {
        var type = typeof(T);
        return (T)this.interactorsRoll[type];
    }
}
