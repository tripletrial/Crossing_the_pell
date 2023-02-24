using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;    

public class DisplayFinalResults : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] 
    private PlayerData Player;
    public GameObject txt_Distance;
    public GameObject txt_Projects;
    public GameObject txt_project1;
    public GameObject txt_project2;
    public GameObject txt_project3;
    public GameObject txt_project4;
    public GameObject bikeGif;
    public GameObject GifLocations;
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
        //add all scene names to list
        all_scene_names.Add("1_Inhabited_Bridge_Bike");
        all_scene_names.Add("2_Conductivity_Bike");
        all_scene_names.Add("3_All_The_Worlds_a_Stage");
        all_scene_names.Add("4_The_Net_bike");
        if (Player.visitedScenes.Count == 1){
            //move the bike gif to the GifLocations
            bikeGif.transform.position = GifLocations.transform.Find("1").transform.position;
        }else if(Player.visitedScenes.Count == 2){
            //move the bike gif to the GifLocations
            bikeGif.transform.position = GifLocations.transform.Find("2").transform.position;}
        else if(Player.visitedScenes.Count == 3){
            //move the bike gif to the GifLocations
            bikeGif.transform.position = GifLocations.transform.Find("3").transform.position;}
        else if(Player.visitedScenes.Count == 4){
            //move the bike gif to the GifLocations
            bikeGif.transform.position = GifLocations.transform.Find("4").transform.position;}
        //read scene data
        //check for unvisited scenes
        foreach(string visted in Player.visitedScenes){
            all_scene_names.Remove(visted);
            number_order.Add(visted.Substring(0, 1));
        }
        //this function display the visited projects
        for(int i =0; i < Player.visitedScenes.Count; i++){
            string tempSceneNumber =  Player.visitedScenes[i].Substring(0, 1);
            if (tempSceneNumber == "1"){
                //use the time and distance from the scene_1 data
                tempTime = Player.Scene_1.timeSpent;
                tempDistance = Player.Scene_1.distanceTraveled;
                tempMinutes = tempTime / 60;
                tempSeconds = tempTime % 60;
            }else if(tempSceneNumber == "2"){
                //use the time and distance from the scene_2 data
                 tempTime = Player.Scene_2.timeSpent;
                 tempDistance = Player.Scene_2.distanceTraveled;
                 tempMinutes = tempTime / 60;
                 tempSeconds = tempTime % 60;}
            else if(tempSceneNumber == "3"){
                //use the time and distance from the scene_3 data
                 tempTime = Player.Scene_3.timeSpent;
                 tempDistance = Player.Scene_3.distanceTraveled;
                 tempMinutes = tempTime / 60;
                 tempSeconds = tempTime % 60;}
            else if(tempSceneNumber == "4"){
                //use the time and distance from the scene_4 data
                 tempTime = Player.Scene_4.timeSpent;
                 tempDistance = Player.Scene_4.distanceTraveled;
                 tempMinutes = tempTime / 60;
                 tempSeconds = tempTime % 60;
        }
            if (i == 0 ){
                txt_project1.transform.Find("Intro").GetComponent<TextMeshPro>().text = Player.visitedScenes[i];
                //Display distance traveled
                txt_project1.transform.Find("Distance").GetComponent<TextMeshPro>().text = "Distance: " + (tempDistance*0.000621371f).ToString("F2")+"miles";
                //Display time spent
                txt_project1.transform.Find("Time").GetComponent<TextMeshPro>().text = "Time: " + tempMinutes.ToString("F0") + "m " + tempSeconds.ToString("F0") + "s";
                //Display Project image
                txt_project1.transform.Find("Image Selections").transform.Find(Player.visitedScenes[i]).gameObject.SetActive(true);


            }
            if (i == 1 ){
                txt_project2.transform.Find("Intro").GetComponent<TextMeshPro>().text = Player.visitedScenes[i];
                //Display distance traveled
                txt_project2.transform.Find("Distance").GetComponent<TextMeshPro>().text = "Distance: " + (tempDistance*0.000621371f).ToString("F2")+"miles";
                //Display time spent
                txt_project2.transform.Find("Time").GetComponent<TextMeshPro>().text = "Time: " + tempMinutes.ToString("F0") + "m " + tempSeconds.ToString("F0") + "s";
                //Display Project image
                txt_project2.transform.Find("Image Selections").transform.Find(Player.visitedScenes[i]).gameObject.SetActive(true);
            }
            if (i == 2 ){
                txt_project3.transform.Find("Intro").GetComponent<TextMeshPro>().text = Player.visitedScenes[i];
                //Display distance traveled
                txt_project3.transform.Find("Distance").GetComponent<TextMeshPro>().text = "Distance: " + (tempDistance*0.000621371f).ToString("F2")+"miles";
                //Display time spent
                txt_project3.transform.Find("Time").GetComponent<TextMeshPro>().text = "Time: " + tempMinutes.ToString("F0") + "m " + tempSeconds.ToString("F0") + "s";
                //Display Project image
                txt_project3.transform.Find("Image Selections").transform.Find(Player.visitedScenes[i]).gameObject.SetActive(true);
            }
            if (i == 3 ){
                txt_project4.transform.Find("Intro").GetComponent<TextMeshPro>().text = Player.visitedScenes[i];
                //Display distance traveled
                txt_project4.transform.Find("Distance").GetComponent<TextMeshPro>().text = "Distance: " + (tempDistance*0.000621371f).ToString("F2")+"miles";
                //Display time spent
                txt_project4.transform.Find("Time").GetComponent<TextMeshPro>().text = "Time: " + tempMinutes.ToString("F0") + "m " + tempSeconds.ToString("F0") + "s";
                //Display Project image
                txt_project4.transform.Find("Image Selections").transform.Find(Player.visitedScenes[i]).gameObject.SetActive(true);
            }
        }

        //this function display the unvisited projects
        for( int i = 0 ; i < all_scene_names.Count ; i++){
            if(all_scene_names.Count == 0){
                //this means there's no unvisited projects
                unvisitedCount = 0;
                Debug.Log("No unvisited projects");
            }else if(all_scene_names.Count == 1){
                //this means there's only one unvisited project

                if(i == 0){
                    txt_project4.transform.Find("Intro").GetComponent<TextMeshPro>().text = all_scene_names[i];
                    txt_project4.transform.Find("Distance").GetComponent<TextMeshPro>().text = "Distance: 0miles";
                    txt_project4.transform.Find("Time").GetComponent<TextMeshPro>().text = "Time: 0m 0s";
                    txt_project4.transform.Find("Image Selections").transform.Find(all_scene_names[i]+"_bw").gameObject.SetActive(true);
                }

            }else if(all_scene_names.Count == 2){
                //this means there's only two unvisited project
                if(i == 0){
                    txt_project3.transform.Find("Intro").GetComponent<TextMeshPro>().text = all_scene_names[i];
                    txt_project3.transform.Find("Distance").GetComponent<TextMeshPro>().text = "Distance: 0miles";
                    txt_project3.transform.Find("Time").GetComponent<TextMeshPro>().text = "Time: 0m 0s";
                    txt_project3.transform.Find("Image Selections").transform.Find(all_scene_names[i]+"_bw").gameObject.SetActive(true);
                }
                if(i == 1){
                    txt_project4.transform.Find("Intro").GetComponent<TextMeshPro>().text = all_scene_names[i];
                    txt_project4.transform.Find("Distance").GetComponent<TextMeshPro>().text = "Distance: 0miles";
                    txt_project4.transform.Find("Time").GetComponent<TextMeshPro>().text = "Time: 0m 0s";
                    txt_project4.transform.Find("Image Selections").transform.Find(all_scene_names[i]+"_bw").gameObject.SetActive(true);
                }
            }
            else if(all_scene_names.Count == 3){
                //this means there's only three unvisited project
                if(i == 0){
                    txt_project2.transform.Find("Intro").GetComponent<TextMeshPro>().text = all_scene_names[i];
                    txt_project2.transform.Find("Distance").GetComponent<TextMeshPro>().text = "Distance: 0miles";
                    txt_project2.transform.Find("Time").GetComponent<TextMeshPro>().text = "Time: 0m 0s";
                    txt_project2.transform.Find("Image Selections").transform.Find(all_scene_names[i]+"_bw").gameObject.SetActive(true);
                }
                if(i == 1){
                    txt_project3.transform.Find("Intro").GetComponent<TextMeshPro>().text = all_scene_names[i];
                    txt_project3.transform.Find("Distance").GetComponent<TextMeshPro>().text = "Distance: 0miles";
                    txt_project3.transform.Find("Time").GetComponent<TextMeshPro>().text = "Time: 0m 0s";
                    txt_project3.transform.Find("Image Selections").transform.Find(all_scene_names[i]+"_bw").gameObject.SetActive(true);
                }
                if(i == 2){
                    txt_project4.transform.Find("Intro").GetComponent<TextMeshPro>().text = all_scene_names[i];
                    txt_project4.transform.Find("Distance").GetComponent<TextMeshPro>().text = "Distance: 0miles";
                    txt_project4.transform.Find("Time").GetComponent<TextMeshPro>().text = "Time: 0m 0s";
                    txt_project4.transform.Find("Image Selections").transform.Find(all_scene_names[i]+"_bw").gameObject.SetActive(true);
                }
            }

            //display the unvisited projects
            
        }

        foreach(string sc in all_scene_names){
            Debug.Log(sc);
        }

        Debug.Log(Player.visitedScenes.Count);
        //Display traveled distance in feet
        txt_Distance.GetComponent<TextMeshPro>().text = "Distance Traveled: " + (Player.DistanceTraveled*0.000621371f).ToString("F2")+"miles";
        txt_Projects.GetComponent<TextMeshPro>().text = "Projects Experienced: " + Player.ProjectExperienced.ToString();
        //Player.DistanceTraveled += 10f;
        //Debug.Log("Displaying Final Results");

        // // Project 1
        // txt_project1.transform.Find("Intro").GetComponent<TextMeshPro>().text = "for "+ Player.visitedScenes[0] + ":";
        // txt_project1.transform.Find("Distance").GetComponent<TextMeshPro>().text = "Distance: " + (Player.Distance1*0.000621371f).ToString("F2")+"miles";
        // //Project 1 image
        // txt_project1.transform.Find("Image Selections").transform.Find(Player.visitedScenes[0]).gameObject.SetActive(true);
        // //Project 1 time
        // float minute1 = Mathf.Floor(Player.TimeSpent1 / 60);
        // float second1 = Player.TimeSpent1 % 60;
        // txt_project1.transform.Find("Time").GetComponent<TextMeshPro>().text = "Time: " + minute1.ToString("F0") + ":" + second1.ToString("F0");
  

        // // Project 2
        // txt_project2.transform.Find("Intro").GetComponent<TextMeshPro>().text = "for "+ Player.visitedScenes[1] + ":";
        // txt_project2.transform.Find("Distance").GetComponent<TextMeshPro>().text = "Distance: " + (Player.Distance2*0.000621371f).ToString("F2")+"miles";
        // //Project 2 image
        // txt_project2.transform.Find("Image Selections").transform.Find(Player.visitedScenes[1]).gameObject.SetActive(true);
        // //Project 2 time
        // float minute2 = Mathf.Floor(Player.TimeSpent2 / 60);
        // float second2 = Player.TimeSpent2 % 60;
        // txt_project2.transform.Find("Time").GetComponent<TextMeshPro>().text = "Time: " + minute2.ToString("F0") + ":" + second2.ToString("F0");

        // // Project 3
        // txt_project3.transform.Find("Intro").GetComponent<TextMeshPro>().text = "for "+ Player.visitedScenes[2] + ":";
        // txt_project3.transform.Find("Distance").GetComponent<TextMeshPro>().text = "Distance: " + (Player.Distance3*0.000621371f).ToString("F2")+"miles";
        // //Project 3 image
        // //txt_project3.transform.Find("Image Selections").transform.Find(Player.visitedScenes[2]).gameObject.SetActive(true);
        // //Project 3 time
        // float minute3 = Mathf.Floor(Player.TimeSpent3 / 60);
        // float second3 = Player.TimeSpent3 % 60;
        // txt_project3.transform.Find("Time").GetComponent<TextMeshPro>().text = "Time: " + minute3.ToString("F0") + ":" + second3.ToString("F0");

        // // Project 4
        // txt_project4.transform.Find("Intro").GetComponent<TextMeshPro>().text = "for "+ Player.visitedScenes[3] + ":";
        // txt_project4.transform.Find("Distance").GetComponent<TextMeshPro>().text = "Distance: " + (Player.Distance4*0.000621371f).ToString("F2")+"miles";
        // //Project 4 image
        // //txt_project4.transform.Find("Image Selections").transform.Find(Player.visitedScenes[3]).gameObject.SetActive(true);
        // //Project 4 time
        // float minute4 = Mathf.Floor(Player.TimeSpent4 / 60);
        // float second4 = Player.TimeSpent4 % 60;
        // txt_project4.transform.Find("Time").GetComponent<TextMeshPro>().text = "Time: " + minute4.ToString("F0") + ":" + second4.ToString("F0");

    }
}
