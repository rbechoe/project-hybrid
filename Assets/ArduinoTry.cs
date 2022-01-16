using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class ArduinoTry : MonoBehaviour
{
    SerialPort dataStream = new SerialPort("COM5", 9600);
    public string receivedString;

    public string[] datas;

    // Start is called before the first frame update
    void Start()
    {
        dataStream.Open();
    }

    // Update is called once per frame
    void Update()
    {
        receivedString = dataStream.ReadLine();

        string[] datas = receivedString.Split(',');
        Debug.Log("First" + float.Parse(datas[0]));
        Debug.Log("Second" + float.Parse(datas[1]));
        
    }
}
