using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headTiltingTurning : MonoBehaviour
{
    public GameObject cameraObject;
    public FloatVariable BlyncSensorangle;


    // Update is called once per frame
    void Update()
    {
        // Get the z rotation of the camera and set it ot BlyncSensorangle.value
        if(cameraObject.transform.rotation.eulerAngles.z>180)
        {
            BlyncSensorangle.value = cameraObject.transform.rotation.z- 360;
        }
        else
        {
            BlyncSensorangle.value = cameraObject.transform.rotation.eulerAngles.z;
        }

        Debug.Log(BlyncSensorangle.value+  " Current Turn");
        
    }
}
