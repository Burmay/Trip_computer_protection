using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RepositoriesBase
{
    private Dictionary<Type, Repository> repositoriesRoll;
    private SceneConfig sceneConfig;

    public RepositoriesBase(SceneConfig sceneConfig)
    {
        this.sceneConfig = sceneConfig;
    }

    public void CreateAllRepositories()
    {
        this.repositoriesRoll = this.sceneConfig.CreateAllRepositories();
    }

    public void SendOnCreateToAllRepository()
    {
        var allRepository = this.repositoriesRoll.Values;
        foreach (var repository in allRepository)
        {
            repository.OnCreate();
        }
    }

    public void InitializeAllRepository()
    {
        var allRepository = this.repositoriesRoll.Values;
        foreach (var repository in allRepository)
        {
            repository.Initialize();
        }
    }

    public void SendOnStartAllRepository()
    {
        var allRepository = this.repositoriesRoll.Values;
        foreach (var repository in allRepository)
        {
            repository.OnStart();
        }
    }

    public T GetRepository<T>() where T : Repository
    {
        var type = typeof(T);
        return (T)this.repositoriesRoll[type];
    }
}
