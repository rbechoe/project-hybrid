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

    public GameObject myShark;

    // Start is called before the first frame update
    void Start()
    {
        CDC = gameObject.GetComponent<CinemachineDollyCart>();

        if (myShark == null) enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, myShark.transform.position) < 1)
        {
            CDC.m_Speed = moveSpeed;
        }
        else
        {
            CDC.m_Speed = 0;
        }
    }
}
