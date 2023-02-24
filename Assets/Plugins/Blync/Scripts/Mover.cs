using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mathf = UnityEngine.Mathf;
using UnityEngine.SceneManagement;
using static OVRManager;
//using OVRManager.isUserPresent;

public class Mover : MonoBehaviour
{
    public BlyncControllerData blyncControllerData;

    // Get Bike type
    public GameObject red_adult_bike;
    public GameObject blue_adult_bike;
    public GameObject red_kids_bike;
    public GameObject blue_kids_bike;
    public BikeType bikeType;
    //public PlayerData playerData;

    //import the pause menu component
    public GameObject pauseMenu;
    public float CountdownTime = 30f;
    public GameObject CountdownText;

    CharacterController character;
    public FloatVariable BlyncSensorSpeed;
    public FloatVariable BlyncTurnAngle;
    public GameObject axis1; 
    public GameObject axis2;
    public Transform front;
    public Transform bike;
    //public FloatVariable Angler;
    Transform resetTransform;
    [SerializeField] GameObject player;
    [SerializeField] Camera playerHead;

    Vector3 respawnPosition;
    bool isPlayerAlive = true;
    [SerializeField] GameObject startPositionObj;
    [SerializeField] GameObject deadLevelObj;

    //call the health components
    public int health;
    public int numOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    

    //This is the level of the death plane
    float deadLevelY; 
    public AudioSource deathSoundEffect;

    // Make wheels spin around their x axis
    public GameObject FrontWheel;
    public GameObject BackWheel;
    public GameObject Pedal;

    bool isPaused = false;
    public AudioSource audioData;

    int direction = -1;
    float turnAngle = 0.0f, motion, verticalVelocity;
    public GameObject currentSpeed;
    public GameObject traveledDistance;
    public GameObject current_Project;
    Vector3 movement;
    Quaternion originalFrontRot;

    bool centerCorrected = false;
    //get player data 
    Vector3 oldPos;
    float totalDistance  = 0;
    

    // Get traveled distance MTpro
    public GameObject distanceText;
    void Awake(){
        checkBikeType();
        // the bike shall be updated earlier(before the Start() in controllerInputManager), so that the controllerInputManager can get the correct menu in the actived bike
    }

    // Start is called before the first frame update
    void Start()
    {
        oldPos = transform.position;
        
        
        StartCoroutine(recenter_countdown());
        //BlyncTurnAngle = 20;
        //blyncControllerData.resetCenterCorrection();
       // blyncControllerData.setCenterCorrection();
        blyncControllerData.centerCorrection = -20;
        Debug.Log("Mover Start");
        deadLevelY = deadLevelObj.transform.position.y;
        respawnPosition = startPositionObj.transform.position;
        character = GetComponent<CharacterController>();
        originalFrontRot = front.transform.localRotation;
        Debug.Log("originalFrontRot: " + originalFrontRot);
        // for (int i = 0; i<hearts.Length; i++){
        //         if (i < numOfHearts){
        //         hearts[i].enabled = true;
        //     } else {
        //         hearts[i].enabled = false;
        //     };
        // }
        
    // headset connected
    Debug.Log("Headset connected");
    OVRManager.HMDMounted +=HandleHMDMounted;
    OVRManager.HMDUnmounted += HandleHMDUnmounted;
        }

        IEnumerator recenter_countdown(){
            yield return new WaitForSeconds(1);
            
        recenter();
        }
void distanceTraveledFunction(){
    Vector3 distanceVector = transform.position - oldPos;
     float distanceThisFrame = distanceVector.magnitude;
     totalDistance += distanceThisFrame;
     oldPos = transform.position;
    // Debug.Log("Distance Traveled: " + totalDistance);
     //traveledDistance.GetComponent<TextMeshPro>().text = (totalDistance * 0.00062137f).ToString("f2").ToString() ;
}

void HandleHMDMounted() {
   // Do stuff
    Debug.Log("HMD Mounted");
}

void HandleHMDUnmounted() {
    // Do stuff
    Debug.Log("HMD Unmounted");
}

void FixedUpdate()
    {
        //checkBikeType();
    distanceTraveledFunction();
        if (isPaused ==true){
            //Debug.Log("Game is paused");
        }else{
           // Debug.Log("Game is not paused");
            setTurnAngle();
            checkRotation();
            moveForward();
             CheckLevel();
            //checkHealth();
        spin();
        //blyncControllerData.centerCorrection =  0f;
        }
        
        Check_Vr_on_or_off();
        if (health == 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void checkHealth(){
    for (int i = 0; i<hearts.Length; i++){
            if(i<health){
            hearts[i].sprite = fullHeart;
        }else{
            hearts[i].sprite = emptyHeart;
        }
    }
    }
    public void spin(){
        float rotationalSpeed = BlyncSensorSpeed.value;
        FrontWheel.transform.Rotate(rotationalSpeed,0,0);
        BackWheel.transform.Rotate(rotationalSpeed,0,0);
        Pedal.transform.Rotate(rotationalSpeed/2, 0, 0);
    }
    public void recenter()
    {
        Debug.Log("recenter");
        var rotationAngley = playerHead.transform.rotation.eulerAngles.y - resetTransform.rotation.eulerAngles.y;
        player.transform.Rotate(0, -rotationAngley, 0, Space.Self);

        var distanceDiff = resetTransform.position- playerHead.transform.position;
        player.transform.position += distanceDiff;
        blyncControllerData.resetCenterCorrection();

    }
    void checkRotation()
    {
        //rotate front bar
        front.transform.localRotation = originalFrontRot * Quaternion.Euler(0, direction * turnAngle, 0);
        if (BlyncSensorSpeed.value > 0f)
        {
            // front rotation
            transform.Rotate(0, direction * Time.deltaTime * (turnAngle), 0, Space.World);

        }
    }
    void Check_Vr_on_or_off()
    {   
        if (isPaused == false){
            
            if (playerHead.transform.localPosition.y == 0 & playerHead.transform.localPosition.x == 0 & playerHead.transform.localPosition.z == 0){
                        Debug.Log("VR is off");
                        Debug.Log("Paused");
                        // recenter();
                        isPaused = true;
                        pauseMenu.SetActive(true);
                        

                        

                    }
                    else
                    {
                        //Debug.Log("VR is on");
                    }
            } else{
                Countdown();
                        
                CountdownText.GetComponent<TextMeshProUGUI>().text = CountdownTime.ToString("f0")+ " seconds";
                if (playerHead.transform.localPosition.y != 0 & playerHead.transform.localPosition.x != 0 & playerHead.transform.localPosition.z != 0){
                        Debug.Log("VR is on");
                        Debug.Log("Unpaused");
                        
                        // recenter();
                        isPaused = false;
                        pauseMenu.SetActive(false);
                    }
                    else
                    {
                        //Debug.Log("VR is still off");
                    }

            }
        
        }
        void checkCountdown(){
            if (CountdownTime <= 0){
            SceneManager.LoadScene("5_Ending");
            }
        }
    void Countdown(){
        //Start Counting Down
        CountdownTime -= Time.deltaTime;
        checkCountdown();
    }
    void setTurnAngle()
    {
        turnAngle = BlyncTurnAngle.value/2;
    }

    void moveForward()
    {
        motion = 0f;
        
        if (BlyncSensorSpeed.value > 0f)
        {
            if(centerCorrected == false){
                StartCoroutine(recenter_Wheel());
                Debug.Log("Center Corrected");
                centerCorrected = true;
            }
            motion = 1f;
            //Debug.Log( 2.237 * BlyncSensorSpeed.value);
            currentSpeed.GetComponent<TextMeshPro>().text =  (Mathf.Round((float)2.237 * BlyncSensorSpeed.value*100f)/100f).ToString();
            if(SceneManager.GetActiveScene().name == "0_TutorialScene"){
                current_Project.GetComponent<TextMeshPro>().text = "Tutorial";
            }else if(SceneManager.GetActiveScene().name == "1_Inhabited_Bridge_Bike"){
                current_Project.GetComponent<TextMeshPro>().text = "INHABITED BRIDGE";
            }else if(SceneManager.GetActiveScene().name == "2_Conductivity_Bike"){
                current_Project.GetComponent<TextMeshPro>().text = "CONDUCTIVITY";
            }else if(SceneManager.GetActiveScene().name == "3_All_The_Worlds_a_Stage"){
                current_Project.GetComponent<TextMeshPro>().text = "ALL THE WORLDS A STAGE";
            }else if(SceneManager.GetActiveScene().name == "4_The_Net_bike"){
                current_Project.GetComponent<TextMeshPro>().text = "THE NET";
            }
        }

        movement = transform.TransformDirection(new Vector3(0, 0, motion));
        //Debug.Log(BlyncSensorSpeed.value);
        //Debug.Log("---movement-pre");
        movement = movement * BlyncSensorSpeed.value;
        //Debug.Log(movement);
        //Debug.Log("---movement");

        if (character.isGrounded)
        {
            verticalVelocity = 0;
        }
        else
        {
            verticalVelocity = -10f;
        }
        movement.y = verticalVelocity - (20 * Time.deltaTime);
        verticalVelocity = movement.y;
        //Debug.Log(verticalVelocity);
        //Debug.Log("---verticalVelocity");
        character.Move(movement * Time.deltaTime);
     //   Debug.Log(movement * Time.deltaTime);
    }

    void RespawnBike()
    {
        // move the player to the position of last checkpoint
        transform.position = respawnPosition;
        //recenter();
    }

    void CheckLevel()
    {
        Vector3 position = transform.position;
        if (position.y <= deadLevelY)
        {
            Debug.Log("Lost Health from Deathplane");
            health--;
            deathSoundEffect.Play();
            RespawnBike();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Trigger");
        //using OnTriggerEnter2D instead of OnCollisionEnter2D, we can get overlaping and obj can go through
        
        if (other.gameObject.tag == "checkingZone")
        {
            // this time, the player passes one checkpointzone
            // update the respawnPosition with the current collision zone center.
            // // update the respawnPosition with the LAST CHECKED PLACE.
            respawnPosition = other.transform.position;
            Debug.Log("Checkpoint Saved");
        } else{
            //health--;
            Debug.Log("Lost Health from Collision");
        }
}
void checkBikeType(){
    if (bikeType.kidsBike){
            if (bikeType.Blue){
                red_kids_bike.SetActive(false);
                blue_kids_bike.SetActive(true);
                red_adult_bike.SetActive(false);
                blue_adult_bike.SetActive(false);
               //Debug.Log("Blue Kids Bike");

                // change transform elements of the bike into corresponoding model
                
                front = blue_kids_bike.transform.Find("steering").transform;
                FrontWheel = blue_kids_bike.transform.Find("steering").transform.Find("wheel_f").gameObject;
                BackWheel = blue_kids_bike.transform.Find("wheel_b").gameObject;
                Pedal = blue_kids_bike.transform.Find("crankset").gameObject;
                resetTransform = blue_kids_bike.transform.Find("resetpt").transform;
                currentSpeed = blue_kids_bike.transform.Find("steering").transform.Find("SpeedoMeter").transform.Find("Text").transform.Find("Matrices").transform.Find("Variables").transform.Find("Speed_t").gameObject;
                //traveledDistance = blue_kids_bike.transform.Find("steering").transform.Find("SpeedoMeter").transform.Find("Text").transform.Find("Matrices").transform.Find("Variables").transform.Find("Distance_var").gameObject;
                current_Project = blue_kids_bike.transform.Find("steering").transform.Find("SpeedoMeter").transform.Find("Text").transform.Find("Matrices").transform.Find("Variables").transform.Find("Project_var").gameObject;
            }
            else{
                red_kids_bike.SetActive(true);
                blue_kids_bike.SetActive(false);
                red_adult_bike.SetActive(false);
                blue_adult_bike.SetActive(false);
                //Debug.Log("Red Kids Bike");

                // change transform elements of the bike into corresponoding model

                front = red_kids_bike.transform.Find("steering").transform;
                FrontWheel = red_kids_bike.transform.Find("steering").transform.Find("wheel_f").gameObject;
                BackWheel = red_kids_bike.transform.Find("wheel_b").gameObject;
                Pedal = red_kids_bike.transform.Find("crankset").gameObject;
                resetTransform = red_kids_bike.transform.Find("resetpt").transform;
                currentSpeed = red_kids_bike.transform.Find("steering").transform.Find("SpeedoMeter").transform.Find("Text").transform.Find("Matrices").transform.Find("Variables").transform.Find("Speed_t").gameObject;
                //traveledDistance = red_kids_bike.transform.Find("steering").transform.Find("SpeedoMeter").transform.Find("Text").transform.Find("Matrices").transform.Find("Variables").transform.Find("Distance_var").gameObject;
                current_Project = red_kids_bike.transform.Find("steering").transform.Find("SpeedoMeter").transform.Find("Text").transform.Find("Matrices").transform.Find("Variables").transform.Find("Project_var").gameObject;
                
            }
        }
        else{
            if (bikeType.Blue){
                red_adult_bike.SetActive(false);
                blue_adult_bike.SetActive(true);
                red_kids_bike.SetActive(false);
                blue_kids_bike.SetActive(false);
                //Debug.Log("Blue Adult Bike");

                // change transform elements of the bike into corresponoding model

                front = blue_adult_bike.transform.Find("steering").transform;
                FrontWheel = blue_adult_bike.transform.Find("steering").transform.Find("wheel_f").gameObject;
                BackWheel = blue_adult_bike.transform.Find("wheel_b").gameObject;
                Pedal = blue_adult_bike.transform.Find("crankset").gameObject;
                resetTransform = blue_adult_bike.transform.Find("resetpt").transform;
                currentSpeed = blue_adult_bike.transform.Find("steering").transform.Find("SpeedoMeter").transform.Find("Text").transform.Find("Matrices").transform.Find("Variables").transform.Find("Speed_t").gameObject;
                //traveledDistance = blue_adult_bike.transform.Find("steering").transform.Find("SpeedoMeter").transform.Find("Text").transform.Find("Matrices").transform.Find("Variables").transform.Find("Distance_var").gameObject;
                current_Project = blue_adult_bike.transform.Find("steering").transform.Find("SpeedoMeter").transform.Find("Text").transform.Find("Matrices").transform.Find("Variables").transform.Find("Project_var").gameObject;
            }
            else{
                red_adult_bike.SetActive(true);
                blue_adult_bike.SetActive(false);
                red_kids_bike.SetActive(false);
                blue_kids_bike.SetActive(false);
                //Debug.Log("Red Adult Bike");

                // change transform elements of the bike into corresponoding model

                front = red_adult_bike.transform.Find("steering").transform;
                FrontWheel = red_adult_bike.transform.Find("steering").transform.Find("wheel_f").gameObject;
                BackWheel = red_adult_bike.transform.Find("wheel_b").gameObject;
                Pedal = red_adult_bike.transform.Find("crankset").gameObject;
                resetTransform = red_adult_bike.transform.Find("resetpt").transform;
                currentSpeed = red_adult_bike.transform.Find("steering").transform.Find("SpeedoMeter").transform.Find("Text").transform.Find("Matrices").transform.Find("Variables").transform.Find("Speed_t").gameObject;
                //traveledDistance = red_adult_bike.transform.Find("steering").transform.Find("SpeedoMeter").transform.Find("Text").transform.Find("Matrices").transform.Find("Variables").transform.Find("Distance_var").gameObject;
                current_Project = red_adult_bike.transform.Find("steering").transform.Find("SpeedoMeter").transform.Find("Text").transform.Find("Matrices").transform.Find("Variables").transform.Find("Project_var").gameObject;
                //Debug.Log("Red Adult Bike");
            }
        }
}
IEnumerator recenter_Wheel(){
    yield return new WaitForSeconds(1.5f);
    blyncControllerData.setCenterCorrection();
}
}
