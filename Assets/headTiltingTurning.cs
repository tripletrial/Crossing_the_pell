using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mathf = UnityEngine.Mathf;

public class headTiltingTurning : MonoBehaviour
{
    public GameObject cameraObject;
    public FloatVariable BlyncSensorangle;

    // Update is called once per frame
    void Update()
    {
        // Get the z rotation of the camera and set it to BlyncSensorangle.value
        float zRotation = cameraObject.transform.rotation.eulerAngles.z;
        if (zRotation > 180)
        {
            BlyncSensorangle.value = Mathf.Lerp(-100, 0f, 1+ (zRotation-360) / 65f);
            Debug.Log("zRotation: " + zRotation);
            Debug.Log((zRotation-360) / 65f);
        }
        else
        {
            BlyncSensorangle.value = Mathf.Lerp(0f, 100f, zRotation / 65f);
            Debug.Log("zRotation: " + zRotation);
            Debug.Log(zRotation / 65f);
        }

        Debug.Log(BlyncSensorangle.value + " Current Turn");
    }
}
