using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArduinoTurret : MonoBehaviour, ITurret
{
    private SerialController serialController;
    public SerialController SerialController
    {
        get
        {
            if (serialController == null)
            {
                serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
            }
            return serialController;
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

    public void GetFloatValue(string strToRemove, float returnValue, EventType eType)
    {
        string message = SerialController.ReadSerialMessage();
        if (message.Contains(strToRemove))
        {
            string val = message.Replace(strToRemove, "");
            returnValue = float.Parse(val);
            EventSystem<float>.InvokeEvent(eType, returnValue);

        }
    }

}