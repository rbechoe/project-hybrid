/**
 * Ardity (Serial Communication for Arduino + Unity)
 * Author: Daniel Wilches <dwilches@gmail.com>
 *
 * This work is released under the Creative Commons Attributions license.
 * https://creativecommons.org/licenses/by/2.0/
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SampleUserPollingReadWrite : MonoBehaviour
{
    public SerialController serialController;

    public float valY;
    public float valX;

    private void Start()
    {
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
        Debug.Log("Press A or Z to execute some actions");
    }

    private void Update()
    {
        //Send Data
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Sending A");
            serialController.SendSerialMessage("A");
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Sending Z");
            serialController.SendSerialMessage("Z");
        }

        //Receive Data
        string message = serialController.ReadSerialMessage();
        if (message == null)
            return;

        if (message.Contains("Button: 1"))
        {
            Debug.Log("Shoot");
            EventSystem.InvokeEvent(EventType.SHOOT);
        }

        getValue("Value Y: ", valY, EventType.TURRET_Y);
        getValue("value X: ", valX, EventType.TURRET_X);

        if (message.Contains("Switch: "))
        {
            string valSwitch = message.Replace("Switch: ", "");
            int valueSwitch = int.Parse(valSwitch);
            Debug.Log("Switch: " + valueSwitch);
            if (valueSwitch == 0)
            {
                EventSystem.InvokeEvent(EventType.DIVE_OFF);
            }
            if (valueSwitch == 1)
            {
                EventSystem.InvokeEvent(EventType.DIVE_ON);
            }
        }

        // Check if the message is plain data or a connect/disconnect event.
        if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_CONNECTED))
        {
            Debug.Log("Connection established");
        }
        else if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_DISCONNECTED))
        {
            Debug.Log("Connection attempt failed or disconnection detected");
        }
        else
        {
            Debug.Log("Message arrived: " + message);
        }
    }

    private void getValue( string strToRemove, float returnValue, EventType eType)
    {
        string message = serialController.ReadSerialMessage();
        if (message.Contains(strToRemove))
        {
            string val = message.Replace(strToRemove, "");
            returnValue = float.Parse(val);
            EventSystem<float>.InvokeEvent(eType,returnValue);
        }
    }
}