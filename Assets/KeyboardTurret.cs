using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardTurret : MonoBehaviour, ITurret
{
    public void GetBoolValue(string strToRemove, int returnValue, int returnValue2, EventType eType, EventType eType2)
    {
        switch (eType)
        {
            case EventType.TOGGLE_DIVE:
                EventSystem<bool>.InvokeEvent(eType, Input.GetKeyDown(KeyCode.G));
                break;
            case EventType.SHOOT:
                EventSystem<bool>.InvokeEvent(eType, Input.GetMouseButton(0));
                break;
      
            case EventType.DIVE_ON:
                EventSystem<bool>.InvokeEvent(eType, Input.GetKeyDown(KeyCode.T));
                break;
            case EventType.DIVE_OFF:
                EventSystem<bool>.InvokeEvent(eType, Input.GetKeyDown(KeyCode.Y));
                break;
        }          
    }

    public void GetFloatValue(string strToRemove, float returnValue, EventType eType)
    {
        switch (eType)
        {
            case EventType.TURRET_X:
                EventSystem<float>.InvokeEvent(eType, returnValue);
                break;
            case EventType.TURRET_Y:
                EventSystem<float>.InvokeEvent(eType, returnValue);
                break;                
        }
    }
}
