using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.iOS;
using UnityEngine;

public class BuildInteractor : Interactor
{
    InventoryInteractor inventoryInteractor;
    public GameObject machineGunPrefab { get; private set; }
    public GameObject rocketLauncherPrefab { get; private set; }
    public GameObject laserTurretPrefab { get; private set; }
    string PATH_MG = "Prefabs/Turret/MGTurret/Turret";
    string PATH_ROCKET = "Prefabs/Turret/RocketLauncher/RocketLauncher";
    string PATH_LASER = "Prefabs/Turret/LaserTurret/LaserTurret";
    public TurretBluePrint mGTurretBP { get; private set; }
    public TurretBluePrint rocketLauncherBP { get; private set; }
    public TurretBluePrint laserTurretBP { get; private set; }
    public TurretBluePrint turretToBuild { private set; get; }
    public List<TurretBase> turrets { get; private set; }


    public override void OnStart()
    {
        LoadResources();
        SetTurretType();
    }

    void LoadResources()
    {
        machineGunPrefab = Resources.Load<GameObject>(PATH_MG);
        rocketLauncherPrefab = Resources.Load<GameObject>(PATH_ROCKET);
        laserTurretPrefab = Resources.Load< GameObject>(PATH_LASER);

        inventoryInteractor = Game.GetInteractor<InventoryInteractor>();

        turrets = new List<TurretBase>();
    }

    void SetTurretType()
    {
        mGTurretBP = new TurretBluePrint
        {
            Prefab = machineGunPrefab,
            Cost = 50,
            offsetY = 0.05f
        };
        rocketLauncherBP = new TurretBluePrint
        {
            Prefab = rocketLauncherPrefab,
            Cost = 100,
            offsetY = 0.05f
        };
        laserTurretBP = new TurretBluePrint
        {
            Prefab = laserTurretPrefab,
            Cost = 75,
            offsetY = -0.1f
        };
    }

    public bool CanBuild { get { return turretToBuild.Prefab != null; } }

    public TurretBase BuildTurret(TowerSlotController slot)
    {
        if (inventoryInteractor.Money - turretToBuild.Cost < 0) return null;
        inventoryInteractor.SpendMoney(turretToBuild.Cost);
        Vector3 pos = new Vector3(slot.transform.position.x, slot.transform.position.y + turretToBuild.offsetY, slot.transform.position.z);
        GameObject instance = Instantiate(turretToBuild.Prefab, pos, Quaternion.identity);
        var turret = instance.GetComponent<TurretBase>();
        turrets.Add(turret);
        return turret;
    }

    public void SetTurretToBuild(TurretBluePrint turret)
    {
        turretToBuild = turret;
    }
}
