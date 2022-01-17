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

/**
 * When creating your message listeners you need to implement these two methods:
 *  - OnMessageArrived
 *  - OnConnectionEvent
 */
public class SampleMessageListener : MonoBehaviour
{
    // Invoked when a line of data is received from the serial device.
    void OnMessageArrived(string msg)
    {
        //Debug.Log("Message arrived: " + msg);

        if(msg.Contains("Button: 1"))
        {
            EventSystem.InvokeEvent(EventType.SHOOT);
        }

        if(msg.Contains("Value X: "))
        {
            string valX = msg.Replace("Value X: ", "");
            float valueX = float.Parse(valX);
            Debug.Log(valueX);
            EventSystem<float>.InvokeEvent(EventType.TURRET_X, valueX);
        }

        if (msg.Contains("Value Y: "))
        {
            string valY = msg.Replace("Value Y: ", "");
            float valueY = float.Parse(valY);
            Debug.Log(valueY);
            EventSystem<float>.InvokeEvent(EventType.TURRET_Y, valueY);

        }
    }


    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        if (success)
            Debug.Log("Connection established");
        else
            Debug.Log("Connection attempt failed or disconnection detected");
    }
}
