using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextWaweButton : MonoBehaviour
{
    public void NextWawe()
    {
        GlobalEventManager.SendTacticalPhaseOff();
    }
}
