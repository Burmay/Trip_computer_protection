using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGTurretController : TurretBase
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void LoadResources()
    {
        base.BULLET_PATH = "Prefabs/Shell/MachineGun/MachineGunBullet";
        base.LoadResources();
    }

    protected override void Update()
    {
        base.Update();
    }
}
