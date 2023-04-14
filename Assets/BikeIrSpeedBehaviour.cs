using System;
using Unity;
using UnityEngine;
using Uduino;

public class BikeIrSpeedBehaviour : MonoBehaviour
{
    public float speedScale;
    public FloatVariable BlyncSensorSpeed;
    public const int DELTA_SPEED = 1200;

    private float _prevSpeed;



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
        _prevSpeed = 0;
    }

    void DataReceived(string data, UduinoDevice board)
    {
        bool success = float.TryParse(data, out var rawSpeed);
        if(Math.Abs(rawSpeed - _prevSpeed) < DELTA_SPEED)
        {
            _speed = rawSpeed * speedScale;
            _prevSpeed = _speed;
        }
    }
   
    void FixedUpdate()
    {
        BlyncSensorSpeed.value = _speed;
    }
}
