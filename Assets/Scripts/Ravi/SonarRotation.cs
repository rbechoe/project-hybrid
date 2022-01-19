using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarRotation : MonoBehaviour
{
    void Start()
    {
        EventSystem.AddListener(EventType.START_BOSS, Rotate);   
    }

    public void Rotate()
    {
        transform.localEulerAngles = new Vector3(90, 270, 0);
        EventSystem.RemoveListener(EventType.START_BOSS, Rotate);
        Destroy(gameObject);
    }
}
