using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.IO.Ports;

public class BetterUduino : MonoBehaviour
{
    public string arduinoPort = "COM3";

    private SerialPort _stream;
    private bool _connectionStatus;
    // Start is called before the first frame update
    void Start()
    { 
        _stream = new SerialPort(arduinoPort, 9600);
        _stream.ReadTimeout = 50;
        _stream.Open();
        _connectionStatus = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_connectionStatus)
        {
        }
        else
        {
            ConnectionTrial();
        }
    }
    
    private string ReadFromArduino ()
    {
        StartCoroutine
        (

            AsynchronousReadFromArduino
            ((string s) => Debug.Log(s), // Callback
                () => Debug.LogError("No reads from arduino"), // Error callback
                1000f // Timeout (milliseconds)
            )
        );
    }

    private void WriteToArduino(string message)
    {
        _stream.WriteLine(message);
        _stream.BaseStream.Flush();
    }

    private void ConnectionTrial()
    {
        try
        {
            WriteToArduino("ConnectArduino");
        }
        catch(IOException e)
        {
            _stream.Close();
            _stream.Open();
        }
    }

    public IEnumerator AsynchronousReadFromArduino(Action<string> callback, Action fail = null,
        float timeout = float.PositiveInfinity)
    {
        DateTime initialTime = DateTime.Now;
        TimeSpan diff = default(TimeSpan);
        do
        {
            string dataString = null;
            try
            {
                dataString = _stream.ReadLine();
            }
            catch (TimeoutException)
            {
                dataString = null;
            }

            if (dataString != null)
            {
                callback(dataString);
                yield break; // Terminates the Coroutine
            }
            else
                yield return null; // Wait for next frame

            var nowTime = DateTime.Now;
            diff = nowTime - initialTime;
        } while (diff.Milliseconds < timeout);

        if (fail != null)
            fail();
        yield return null;
    }

}
