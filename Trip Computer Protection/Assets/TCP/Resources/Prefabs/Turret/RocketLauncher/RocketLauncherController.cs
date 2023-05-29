using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncherController : TurretController
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void LoadResources()
    {
        base.BULLET_PATH = "Prefabs/Shell/Rocket/Rocket";
        base.LoadResources();
    }

    protected override void Update()
    {
        base.Update();
    }
}
