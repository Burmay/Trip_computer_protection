using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneConfigurator : SceneConfig
{
    public const string SCENE_NAME = "A1Scene";

    public override string sceneName => SCENE_NAME;

    public override Dictionary<Type, Interactor> CreateAllInteractors()
    {
        var interactorsRoll = new Dictionary<Type, Interactor>();
        this.CreateInteractor<WaweInteractor>(interactorsRoll);
        this.CreateInteractor<BuildInteractor>(interactorsRoll);
        this.CreateInteractor<AmmunitionInteractor>(interactorsRoll);
        this.CreateInteractor<InventoryInteractor>(interactorsRoll);
        // Int

        return interactorsRoll;
    }

    public override Dictionary<Type, Repository> CreateAllRepositories()
    {
        var repositoriesRoll = new Dictionary<Type, Repository>();

        // Repo

        return repositoriesRoll;
    }
}
