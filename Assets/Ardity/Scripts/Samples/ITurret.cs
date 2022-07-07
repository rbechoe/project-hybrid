using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITurret 
{
    public void GetFloatValue(string strToRemove, float returnValue, EventType eType);

    public void GetBoolValue(string strToRemove, int returnValue, int returnValue2, EventType etype, EventType eType2);
}
