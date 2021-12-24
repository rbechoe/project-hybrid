using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SharkCart : MonoBehaviour
{
    CinemachineDollyCart CDC;

    [HideInInspector]
    public float moveSpeed;
    [HideInInspector]
    public bool moving;

    // Start is called before the first frame update
    void Start()
    {
        CDC = gameObject.GetComponent<CinemachineDollyCart>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            CDC.m_Speed = moveSpeed;
        }
        else
        {
            CDC.m_Speed = 0;
        }
    }
}
