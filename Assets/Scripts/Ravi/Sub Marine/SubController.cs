using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SubController : MonoBehaviour
{
    public float maxDiveSpeed = 2;
    public float diveSpeedIncrement = 0.1f;
    public float currentDiveSpeed = 0f;
    public float waterResistance = 0.2f;
    public float graceFloat = 2f;
    public float graceFloatReset = 2f;

    public AudioSource cabinSFX;
    public AudioSource localSFX;
    public AudioSource music;
    public AudioSource ambient;

    public AudioClip diveSFX;

    public CinemachineDollyCart CDC;

    public GameObject turret, bulletSpawn;
    float turretX, turretY;
    float aimSpeed = 20;

    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) // TODO toggle move with M
        {
            if (currentDiveSpeed < maxDiveSpeed) currentDiveSpeed += diveSpeedIncrement * Time.deltaTime;
            graceFloat = graceFloatReset;
        }

        if (Input.GetKey(KeyCode.S))
        {
            turretX += aimSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            turretX -= aimSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            turretY -= aimSpeed * Time.deltaTime * 1.5f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            turretY += aimSpeed * Time.deltaTime * 1.5f;
        }
        turret.transform.localEulerAngles = new Vector3(turretX, turretY, 0);

        graceFloat -= Time.deltaTime;
        if (graceFloat < 0 && currentDiveSpeed > 0) currentDiveSpeed -= waterResistance * Time.deltaTime;

        currentDiveSpeed = Mathf.Clamp(currentDiveSpeed, 0, maxDiveSpeed);

        CDC.m_Speed = currentDiveSpeed;
    }

    public void StartDive()
    {
        music.Play();
        ambient.Play();
        cabinSFX.Play();
        localSFX.PlayOneShot(diveSFX);
    }

    public void UpdateSFX(AudioClip clip)
    {
        localSFX.PlayOneShot(clip);
    }

    public void UpdateCabin(AudioClip clip)
    {
        cabinSFX.PlayOneShot(clip);
    }
}
