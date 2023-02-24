using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayFinalResults2 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private PlayerData Player;
    public TextMeshPro txt_Distance;
    public TextMeshPro txt_Projects;
    public GameObject txt_project1;
    public GameObject txt_project2;
    public GameObject txt_project3;
    public GameObject txt_project4;
    
    void Start()
    {
        txt_Distance.text = "Distance Traveled: " + Player.DistanceTraveled.ToString()+"m";
        txt_Projects.text = "Projects Experienced: " + Player.ProjectExperienced.ToString();

    }

    
}
