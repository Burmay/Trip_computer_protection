using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    BuildInteractor buildInteractor;
    InventoryInteractor inventoryInteractor;

    private void Start()
    {
        inventoryInteractor = Game.GetInteractor<InventoryInteractor>();
        buildInteractor = Game.GetInteractor<BuildInteractor>();
    }

    public void ChoiceMGTurret()
    {
        if (inventoryInteractor.Money > buildInteractor.mGTurretBP.Cost)
        {
            buildInteractor.SetTurretToBuild(buildInteractor.mGTurretBP);
        }
    }

    public void ChoiseRocketLauncher()
    {
        if (inventoryInteractor.Money > buildInteractor.rocketLauncherBP.Cost)
        {
            buildInteractor.SetTurretToBuild(buildInteractor.rocketLauncherBP);
        }
    }

    public void ChoiseLaserTurret()
    {
        if(inventoryInteractor.Money > buildInteractor.laserTurretBP.Cost)
        {
            buildInteractor.SetTurretToBuild(buildInteractor.laserTurretBP);
        }
    }
}
