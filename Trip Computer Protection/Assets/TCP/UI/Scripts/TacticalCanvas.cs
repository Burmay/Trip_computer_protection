using UnityEngine.UI;
using UnityEngine;

public class TacticalCanvas : MonoBehaviour
{

    Canvas canvas;
    
    void Start()
    {
        Network();
    }

    void Network()
    {
        canvas = GetComponent<Canvas>();

        GlobalEventManager.TacticalPhaseOn += ShowCanvas;
        GlobalEventManager.TacticalPhaseOff += HideCanvas;
    }

    void ShowCanvas()
    {
        canvas.enabled = true;
    }

    void HideCanvas()
    {
        canvas.enabled = false;
    }
}
