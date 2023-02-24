using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetRotationToCenter : MonoBehaviour
{
    public BlyncControllerData blyncControllerData;
    // Start is called before the first frame update
    void Start()
    {
        blyncControllerData.setCenterCorrection();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
