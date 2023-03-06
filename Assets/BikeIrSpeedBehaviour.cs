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
    

    //The final calculation of speed(only accessor)
    private float _speed;


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
        Debug.Log(data);
        bool success = float.TryParse(data, out _rawSpeed);
        _speed = _rawSpeed * speedScale;
    }
    
    void FixedUpdate()
    {
        BlyncSensorSpeed.value = _speed;
    }



}
