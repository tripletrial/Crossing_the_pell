using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bike_Type_Detection : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject red_adult_bike;
    public GameObject blue_adult_bike;
    public GameObject red_kids_bike;
    public GameObject blue_kids_bike;
    public BikeType bikeType;
    void Start()
    {
        if (bikeType.kidsBike){
            if (bikeType.Blue){
                red_kids_bike.SetActive(false);
                blue_kids_bike.SetActive(true);
                red_adult_bike.SetActive(false);
                blue_adult_bike.SetActive(false);
                Debug.Log("Blue Kids Bike");
            }
            else{
                red_kids_bike.SetActive(true);
                blue_kids_bike.SetActive(false);
                red_adult_bike.SetActive(false);
                blue_adult_bike.SetActive(false);
                Debug.Log("Red Kids Bike");
            }
        }
        else{
            if (bikeType.Blue){
                red_adult_bike.SetActive(false);
                blue_adult_bike.SetActive(true);
                red_kids_bike.SetActive(false);
                blue_kids_bike.SetActive(false);
                Debug.Log("Blue Adult Bike");
            }
            else{
                red_adult_bike.SetActive(true);
                blue_adult_bike.SetActive(false);
                red_kids_bike.SetActive(false);
                blue_kids_bike.SetActive(false);
                Debug.Log("Red Adult Bike");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
