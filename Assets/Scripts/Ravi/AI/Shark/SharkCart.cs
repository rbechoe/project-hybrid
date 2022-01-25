using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SharkCart : MonoBehaviour
{
    CinemachineDollyCart CDC;

    [HideInInspector]
    float moveSpeed = 5;

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
        moveSpeed = Mathf.Clamp(moveSpeed, 0, int.MaxValue);
        CDC.m_Speed = moveSpeed;

        bool nearby = (Vector3.Distance(transform.position, myShark.transform.position) < 10);
        moveSpeed = (nearby) ? moveSpeed + 2 : moveSpeed - 2;
    }
}
