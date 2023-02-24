using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDataReseter : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private PlayerData Player;
    public bool reset = false;
    public void OnApplicationQuit()
    {
        if (reset == true){
            Player.DistanceTraveled = 0;
            Player.ProjectExperienced = 0;
            Player.lastScene = "0_Start";
            Player.visitedScenes.Clear();
        }
    }
}
