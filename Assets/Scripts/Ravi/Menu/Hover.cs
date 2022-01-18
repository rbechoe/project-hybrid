using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    public float minimum = -1.0f;
    public float maximum = 1.0f;
    public float speed = 0.25f;

    private float t = 0;
    private float localY;

    void Start()
    {
        localY = transform.localPosition.y;
    }

    void Update()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, localY + Mathf.Lerp(minimum, maximum, t), transform.localPosition.z);
        
        t += Time.deltaTime * speed;

        if (t > 1.0f)
        {
            float temp = maximum;
            maximum = minimum;
            minimum = temp;
            t = 0.0f;
        }
    }
}
