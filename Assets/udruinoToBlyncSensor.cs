using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

public class udruinoToBlyncSensor : MonoBehaviour
{
    public BlyncControllerData blyncControllerData;
    // public bool isSensorOn = false;

    public void enableController()
    {
        blyncControllerData.sessionStarted = true;
    }
    public void OnBoardConnected(UduinoDevice connectedDevice)
    {
        // When the board is connected, we can call the function enableController
        // UduinoManager.Instance.OnBoardConnected -= OnBoardConnected;
        enableController();
    }
}
