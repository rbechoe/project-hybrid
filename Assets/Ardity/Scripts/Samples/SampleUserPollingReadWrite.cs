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

public class SampleUserPollingReadWrite : ITurret
{
    public SerialController serialController;
    public SerialController SerialController
    {
        get 
        { 
            if(serialController == null)
            {
                serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
            }
            return serialController;
        }
    }

    public void GetFloatValue(string strToRemove, float returnValue, EventType eType)
    {
        string message = SerialController.ReadSerialMessage();
        if (message.Contains(strToRemove))
        {
            string val = message.Replace(strToRemove, "");
            returnValue = float.Parse(val);
            EventSystem<float>.InvokeEvent(eType,returnValue);
        }
    }

    public void GetBoolValue(string strToRemove, int returnValue, int returnValue2, EventType etype, EventType eType2)
    {
        string message = SerialController.ReadSerialMessage();
        if (message.Contains(strToRemove))
        {
            string valSwitch = message.Replace("Switch: ", "");
            int valueSwitch = int.Parse(valSwitch);
            Debug.Log("Switch: " + valueSwitch);
            if (valueSwitch == 1)
            {
                EventSystem<int>.InvokeEvent(etype, returnValue);
            }
            if (valueSwitch == 0)
            {
                EventSystem<int>.InvokeEvent(eType2, returnValue2);
            }
        }
    }
}