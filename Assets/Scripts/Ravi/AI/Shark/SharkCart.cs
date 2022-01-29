using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SharkCart : MonoBehaviour
{
    private CinemachineDollyCart CDC;
    private float moveSpeed = 5;

    public GameObject myShark;

    private void Start()
    {
        CDC = gameObject.GetComponent<CinemachineDollyCart>();

        if (myShark == null) enabled = false;
    }

    private void Update()
    {
        moveSpeed = Mathf.Clamp(moveSpeed, 0, int.MaxValue);
        CDC.m_Speed = moveSpeed;

        bool nearby = (Vector3.Distance(transform.position, myShark.transform.position) < 10);
        moveSpeed = (nearby) ? moveSpeed + 2 : moveSpeed - 2;
    }
}
