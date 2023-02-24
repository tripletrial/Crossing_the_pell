using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wasd_tester : MonoBehaviour
{
   
  private int speed = 10;
  public GameObject spawnPoint;
     void Update(){
        Move();
     }
     public void Move()
     {
     
     Vector3 Movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
     
     transform.position += Movement * speed * Time.deltaTime;
     if (Input.GetKey(KeyCode.Q)){
        //rotate left
        transform.Rotate(0, -1, 0);
     }
        if (Input.GetKey(KeyCode.E)){
            //rotate right
            transform.Rotate(0, 1, 0);
        }
     }
     void reset(){
        transform.position =spawnPoint.transform.position;
     }
}