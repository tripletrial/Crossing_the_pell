using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finishLineHit : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject AllBridges;
    public GameObject Bike;
    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Bike.GetComponent<Mover>().enabled = false;
            AllBridges.GetComponent<bridgeLooping>().isLooping = false;
            Debug.Log("Stopping the Bike");
        }
        Debug.Log("Collided into"+ other.GetComponent<Collider>().name);
    }
}
