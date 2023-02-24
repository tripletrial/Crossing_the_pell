using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void Start(){
        Load_1();
    }
    public void Load_1(){
        SceneManager.LoadScene("1_Inhabited_Bridge_Bike");
        
        
    }
}
