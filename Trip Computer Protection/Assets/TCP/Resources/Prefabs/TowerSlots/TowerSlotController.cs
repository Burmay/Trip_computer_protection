using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerSlotController : MonoBehaviour
{
    [SerializeField] Color selectionColor;

    Renderer render;
    Color startColor;
    TurretController turret;
    BuildInteractor buildInteractor;

    // Start is called before the first frame update
    void Start()
    {
        render = gameObject.GetComponent<Renderer>();  
        startColor = render.material.color;
        buildInteractor = Game.GetInteractor<BuildInteractor>();
    }

     void OnMouseEnter()
    {
        if (buildInteractor.CanBuild)
        {
            render.material.color = selectionColor;
        }
    }

    void OnMouseExit()
    {
        render.material.color = startColor;
    }

    private void OnMouseDown()
    {
        if (turret != null || buildInteractor.turretToBuild.Prefab == null) return;
        if (EventSystem.current.IsPointerOverGameObject()) return;

        turret =  buildInteractor.BuildTurret(this);
    }

}
