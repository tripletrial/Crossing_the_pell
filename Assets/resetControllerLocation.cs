using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetControllerLocation : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform resetLocation;
    public GameObject controller;

    // Update is called once per frame
    void Update()
    {
        var distanceDiff = resetLocation.position- controller.transform.position;
        controller.transform.position += distanceDiff;

    }
}
