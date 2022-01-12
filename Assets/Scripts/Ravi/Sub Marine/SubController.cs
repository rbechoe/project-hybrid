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
    public float shootCooldownReset = 1f;

    public AudioSource cabinSFX;
    public AudioSource localSFX;
    public AudioSource music;
    public AudioSource ambient;

    public AudioClip diveSFX;

    public CinemachineDollyCart CDC;

    public GameObject turret, bulletSpawn, projectile;
    float turretX, turretY;
    float aimSpeed = 20;
    float shootCooldown = 0;

    bool diving;

    private void Start()
    {
        EventSystem.AddListener(EventType.TOGGLE_DIVE, ToggleDive);
        EventSystem.AddListener(EventType.AIM_LEFT, AimLeft);
        EventSystem.AddListener(EventType.AIM_RIGHT, AimRight);
        EventSystem.AddListener(EventType.AIM_UP, AimUp);
        EventSystem.AddListener(EventType.AIM_DOWN, AimDown);
        EventSystem.AddListener(EventType.SHOOT, Shoot);
    }

    void ToggleDive()
    {
        diving = !diving;
    }

    void AimLeft()
    {
        turretY -= aimSpeed * Time.deltaTime * 1.5f;
    }

    void AimRight()
    {
        turretY += aimSpeed * Time.deltaTime * 1.5f;
    }

    void AimUp()
    {
        turretX -= aimSpeed * Time.deltaTime;
    }

    void AimDown()
    {
        turretX += aimSpeed * Time.deltaTime;
    }

    void Shoot()
    {
        if (shootCooldown <= 0)
        {
            // TODO shoot projectile
            shootCooldown = shootCooldownReset;
        }
    }

    void Update()
    {
        turret.transform.localEulerAngles = new Vector3(turretX, turretY, 0);

        if (diving)
        {
            if (currentDiveSpeed < maxDiveSpeed) currentDiveSpeed += diveSpeedIncrement * Time.deltaTime;
            graceFloat = graceFloatReset;
        }

        graceFloat -= Time.deltaTime;
        if (graceFloat < 0 && currentDiveSpeed > 0) currentDiveSpeed -= waterResistance * Time.deltaTime;

        currentDiveSpeed = Mathf.Clamp(currentDiveSpeed, 0, maxDiveSpeed);

        CDC.m_Speed = currentDiveSpeed;

        if (shootCooldown > 0) shootCooldown -= Time.deltaTime;
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
