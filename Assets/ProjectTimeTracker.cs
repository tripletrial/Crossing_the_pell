using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProjectTimeTracker : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private PlayerData Player;
    [SerializeField] private SceneData Scene;
    Scene m_Scene;
    string sceneName;
    string last_project;
    Vector3 oldPos;
    float timeThisProject;
    float distanceThisProject;
    float totalDistance = 0;
    // Update is called once per frame
    void Start(){
       m_Scene = SceneManager.GetActiveScene();
       sceneName = m_Scene.name;
       last_project = Player.lastScene;
        timeThisProject = Scene.timeSpent;
        distanceThisProject = Scene.distanceTraveled;
        if (Player.visitedScenes.Contains(sceneName)){
            Debug.Log("Already visited");
        }
        else{
            Player.visitedScenes.Add(sceneName);
            Player.lastScene = sceneName;
            Debug.Log("Added to visited scenes");
            Player.ProjectExperienced += 1;
        }
         oldPos = transform.position;
        
    }
    void Update()
    {
        Time_Distance_Count();
    }
    void Time_Distance_Count(){
        
        Vector3 distanceVector = transform.position - oldPos;
        float distanceThisFrame = distanceVector.magnitude;
        distanceThisProject += distanceThisFrame;
        timeThisProject += Time.deltaTime;
        oldPos = transform.position;
        Scene.timeSpent = timeThisProject;
        Scene.distanceTraveled = distanceThisProject;
        Player.DistanceTraveled = Player.Scene_1.distanceTraveled + Player.Scene_2.distanceTraveled + Player.Scene_3.distanceTraveled + Player.Scene_4.distanceTraveled;
    }

    public void OnDestory(){
        
        //Scene.timeSpent = timeThisProject;
        //Scene.distanceTraveled += distanceThisProject;


        
    }  
}
