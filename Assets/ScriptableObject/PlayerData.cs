using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    public SceneData Scene_1;
    public SceneData Scene_2;
    public SceneData Scene_3;
    public SceneData Scene_4;
public float DistanceTraveled;
public int ProjectExperienced;
public string lastScene;
public List<string> visitedScenes;

public void clearData(){
    DistanceTraveled = 0;
    ProjectExperienced = 0;
    lastScene = "";
    visitedScenes = new List<string>();
}

}

