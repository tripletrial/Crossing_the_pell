using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class distanceTraveledFunction : MonoBehaviour
{
    // Start is called before the first frame update
        //get player data 
    Vector3 oldPos;
    float totalDistance  = 0;
    public GameObject distanceText;
    [SerializeField] private PlayerData Player;
    void distanceTravele(){
        Vector3 distanceVector = transform.position - oldPos;
        float distanceThisFrame = distanceVector.magnitude;
        Player.DistanceTraveled += distanceThisFrame;
        oldPos = transform.position;
        // Debug.Log("Distance Traveled: " + totalDistance);
        distanceText.GetComponent<TextMeshPro>().text = "Distance Traveled: " + Player.DistanceTraveled.ToString("0") + "m";
    }
    // Update is called once per frame
    void Update()
    {
        distanceTravele();
    }
}
