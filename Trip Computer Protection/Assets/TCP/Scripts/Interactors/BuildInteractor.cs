using System.Collections;
using System.Collections.Generic;
using UnityEditor.iOS;
using UnityEngine;

public class BuildInteractor : Interactor
{
    GameObject machineGunPrefab;
    string PATH_MG = "Prefabs/TestTurret/Turret";
    float offsetY = 0.05f;

    public GameObject turretToBuild { private set; get; }

    public override void OnStart()
    {
        LoadResources();
        // test
        turretToBuild = machineGunPrefab;
    }

    void LoadResources()
    {
        machineGunPrefab = Resources.Load<GameObject>(PATH_MG);
    }

    public TurretController BuildTurret(TowerSlotController slot)
    {
        Vector3 pos = new Vector3(slot.transform.position.x, slot.transform.position.y + offsetY, slot.transform.position.z);
        GameObject instance = Instantiate(turretToBuild, pos, Quaternion.identity);
        return instance.GetComponent<TurretController>();
    }
}
