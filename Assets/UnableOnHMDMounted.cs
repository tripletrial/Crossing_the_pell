using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OVRManager;

public class UnableOnHMDMounted : MonoBehaviour
{
public Camera playerHead;
public GameObject[] Next_Tutorial;
    // Update is called once per frame
    public void Update()
    {
         if (playerHead.transform.localPosition.y == 0 & playerHead.transform.localPosition.x == 0 & playerHead.transform.localPosition.z == 0){
                        Debug.Log("VR not active");
                        Debug.Log("Paused");
                    }
                    else
                    {
                        Debug.Log("VR is active");
                        unenableCurentObject();
                    }
    }
    public void unenableCurentObject()
    {
            gameObject.SetActive(false);
    }

    public void OnDisable()
    {
        //Debug.Log("Disabled");
        foreach(GameObject tutorial in Next_Tutorial){
            tutorial.SetActive(true);
        }
    }
}
