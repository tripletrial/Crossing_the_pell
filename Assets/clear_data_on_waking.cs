using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clear_data_on_waking : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerData playerData;
    public BlyncControllerData blyncControllerData;
    public FloatVariable[] floatVariables;
    public SceneData[] sceneDatas;
    void Start(){
        playerData.clearData();
        blyncControllerData.sessionStarted = false;
        foreach(FloatVariable floatVariable in floatVariables){
            floatVariable.value = 0;
        }
        foreach(SceneData sceneData in sceneDatas){
            sceneData.clearData();
        }

    }
    
}
