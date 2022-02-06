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

public class SampleMessageListener : MonoBehaviour
{
    // Invoked when a line of data is received 
    public void OnMessageArrived(string msg)
    {
        Debug.Log("Message arrived: " + msg);

        if(msg.Contains("Button: 1"))
        {
            EventSystem.InvokeEvent(EventType.SHOOT);           //Invoke the Shoot Event 
        }

        if(msg.Contains("Value X: "))
        {
            string valX = msg.Replace("Value X: ", "");         //"Empty" the string so only the value is being processed
            float valueX = float.Parse(valX);                   //Make the received string into a float
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

        if(msg.Contains("Switch: "))
        {
            string valSwitch = msg.Replace("Switch: ", "");
            float valueSwitch = float.Parse(valSwitch);
            Debug.Log(valueSwitch);
            EventSystem<float>.InvokeEvent(EventType.TOGGLE_DIVE, valueSwitch);
        }
    }

    //Check if the connection is established or disconnected.
    public void OnConnectionEvent(bool success)
    {
        if (success)
            Debug.Log("Connection established");
        else
            Debug.Log("Connection attempt failed or disconnection detected");
    }
}
