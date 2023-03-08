using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;
using UnityEngine.Rendering.UI;

public class BikeIrSpeedBehaviour : MonoBehaviour
{
    public float speedScale;
    public FloatVariable BlyncSensorSpeed;

    private int _pinState;
    private int _lastPinState;

    // VD Variables
    public float radius;
    public int numOfStrips;
    private float oldSpeed = 0f;
    private float newSpeed;
    private float acceleration;
    private List<float> speedArray = new List<float>();
    public float setRPM;
    public bool isTesting;
    
    //The final calculation of speed(only accessor)
    private float _speed = 0.23f;

    // Counter for measuring the number of times DataReceived is called per second
    private int _dataReceivedCount = 0;

    // Timer for measuring the time elapsed in seconds
    private float _timer = 0;

    // Start is called before the first frame update
    //establish communication with Uduino
    //test if data is reading or not
    //sending data
    void Start()
    {
        UduinoManager.Instance.OnDataReceived += DataReceived;
        _speed = 0;
    }

    void DataReceived(string data, UduinoDevice board)
    {
        float _rawSpeed;
        // Debug.Log(data);
        bool success = float.TryParse(data, out _rawSpeed);
        // Debug.Log(_rawSpeed);
        if(isTesting){
            _rawSpeed = setRPM;
        }
        // check if _rawSpeed is higher than 600, if so set it to 600
        if (_rawSpeed > 1500)
        {
            _rawSpeed = 1500;
        }
        // _speed = _rawSpeed * speedScale;
        _speed = 2f * Mathf.PI * radius * (_rawSpeed / 60f) / 4;
        // Debug.Log(_speed);

        // Append the new speed to the list
        speedArray.Add(_speed);

        // Increment the counter each time DataReceived is called
        _dataReceivedCount++;
    }

    void FixedUpdate()
    {
        // Check if the list has more than 10 elements
        if (speedArray.Count >= 10)
        {
            // calculate the average speed
            float sum = 0;
            for (int i = 0; i < speedArray.Count; i++)
            {
                sum += speedArray[i];
            }
            float averageSpeed = sum / speedArray.Count;

            // set the new target speed and acceleration
            newSpeed = averageSpeed;
            acceleration = (newSpeed - oldSpeed) / 2;
            Debug.Log("Old speed: " + oldSpeed);
            // set the old speed to the current speed
            oldSpeed = BlyncSensorSpeed.value;

            // clear the list
            speedArray.Clear();
            Debug.Log("New target speed: " + newSpeed);
            Debug.Log("Acceleration: " + acceleration);
        }
        
        // Update the value of the FloatVariable
        BlyncSensorSpeed.value +=acceleration * Time.deltaTime;
        Debug.Log("Speed: " + BlyncSensorSpeed.value);

        // Update the timer each frame
        _timer += Time.deltaTime;

        // Check if one second has elapsed
        if (_timer >= 1f)
        {
            // Print the number of times DataReceived was called in the last second
            // Debug.Log("DataReceived called " + _dataReceivedCount + " times in the last second");

            // Reset the counter and timer
            _dataReceivedCount = 0;
            _timer = 0;
        }
    }
}
