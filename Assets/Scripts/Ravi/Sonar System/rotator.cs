using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotator : MonoBehaviour
{
    public float speed = 50;

    void Update()
    {
        transform.localEulerAngles += Vector3.right * speed * Time.deltaTime;
    }
}
