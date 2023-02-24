using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recenter_wheel : MonoBehaviour
{
    // Start is called before the first frame update
    public bool recentered = false;
    public BlyncControllerData sensorData;
    // Update is called once per frame
    void Update()
    {
        if (recentered)
        {
            //sensorData.centerCorrection = -20f;
            sensorData.setCenterCorrection();
            Debug.Log("Recentering Wheel");
            recentered = false;
        }
    }
}
