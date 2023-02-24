using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;    

public class DisplayFinalResultsv2 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] 
    private PlayerData Player;
    public GameObject txt_Distance;
    public GameObject txt_Distance2;
    public GameObject txt_Time;
    float tempTime = 0;
    float tempDistance = 0;
    float tempMinutes;
    float tempSeconds;
    float unvisitedCount;
    List<string> all_scene_names = new List<string>();
    List<string> number_order = new List<string>();
    void Start()
    {
        //sum up all the distance traveled in each project from scene data
        Player.DistanceTraveled = Player.Scene_1.distanceTraveled + Player.Scene_2.distanceTraveled + Player.Scene_3.distanceTraveled + Player.Scene_4.distanceTraveled;
        tempTime = Player.Scene_1.timeSpent + Player.Scene_2.timeSpent + Player.Scene_3.timeSpent + Player.Scene_4.timeSpent;
        tempMinutes = Mathf.Floor(tempTime / 60);
        tempSeconds = Mathf.RoundToInt(tempTime % 60);

        //display the total distance traveled
        txt_Distance.GetComponent<TextMeshPro>().text = (Player.DistanceTraveled*0.00062137).ToString("F2") + "miles";
        txt_Distance2.GetComponent<TextMeshPro>().text = (Player.DistanceTraveled*0.00062137).ToString("F2") + "miles";
        txt_Time.GetComponent<TextMeshPro>().text = tempMinutes.ToString("F0") + "m " + tempSeconds.ToString("F0") + "s";
    }
}
