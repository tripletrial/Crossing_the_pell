using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class SceneData :ScriptableObject
{
    public string sceneName;
    public float timeSpent;
    public float distanceTraveled;
    public bool visited;

    public void clearData(){
        timeSpent = 0;
        distanceTraveled = 0;
        visited = false;
    }
}