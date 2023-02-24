using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class collisionDetection : MonoBehaviour
{
    // Start is called before the first frame update
    // string array containg all 4 scenes' name
    // public string[] sceneNameStrArray =  new string[] {"1_Inhabited_Bridge_Bike","2_Conductivity_Bike","3_All_The_Worlds_a_Stage","4_The_Net_bike"};


    // Update is called once per frame
    private void OnTriggerEnter(Collider collision)
    {

        
        string hitName = collision.GetComponent<Collider>().name;
        if (hitName  == "1_Inhabited_Bridge_Bike" || hitName =="2_Conductivity_Bike" || hitName == "3_All_The_Worlds_a_Stage" || hitName == "4_The_Net_bike"){
            Debug.Log("Collided into"+ collision.GetComponent<Collider>().name);
            SceneManager.LoadScene(hitName);
        }
        
        
    }

}
