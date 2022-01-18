using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class SubController : MonoBehaviour
{
    public float maxDiveSpeed = 2;
    public float diveSpeedIncrement = 0.1f;
    public float currentDiveSpeed = 0f;
    public float waterResistance = 0.2f;
    public float graceFloat = 2f;
    public float graceFloatReset = 2f;
    public float shootCooldownReset = 1f;
    
    public AudioSource localSFX;
    public AudioSource music;
    public AudioSource ambient;

    public AudioClip diveSFX;

    public CinemachineDollyCart CDC;

    public GameObject turret, bulletSpawn, projectile;
    float turretX, turretY;
    float aimSpeed = 40;
    float shootCooldown = 0;

    public Texture2D blueImg, redImg;
    public Texture2D blueSmallImg, redSmallImg;
    public RawImage mainUI, sideAUI, sideBUI;

    bool diving;

    private void Start()
    {
        EventSystem.AddListener(EventType.TOGGLE_DIVE, ToggleDive);
        EventSystem.AddListener(EventType.DIVE_ON, DiveOn);
        EventSystem.AddListener(EventType.DIVE_OFF, DiveOff);
        EventSystem<int>.AddListener(EventType.TURRET_Y, TurretY);
        EventSystem<int>.AddListener(EventType.TURRET_X, TurretX);
        EventSystem.AddListener(EventType.AIM_LEFT, AimLeft);
        EventSystem.AddListener(EventType.AIM_RIGHT, AimRight);
        EventSystem.AddListener(EventType.AIM_UP, AimUp);
        EventSystem.AddListener(EventType.AIM_DOWN, AimDown);
        EventSystem.AddListener(EventType.SHOOT, Shoot);
        EventSystem<int>.AddListener(EventType.DAMAGE_PLAYER, GetHit);
    }

    void ToggleDive()
    {
        diving = !diving;
    }

    void DiveOn()
    {
        diving = true;
    }

    void TurretY(int axis)
    {
        turretY = axis;
    }

    void TurretX(int axis)
    {
        turretX = axis;
    }

    void DiveOff()
    {
        diving = false;
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
            Instantiate(projectile, bulletSpawn.transform.position, turret.transform.rotation);
            shootCooldown = shootCooldownReset;
        }
    }

    void GetHit(int damage)
    {
        StartCoroutine(HitEffect());
    }

    IEnumerator HitEffect()
    {
        mainUI.texture = redImg;
        sideAUI.texture = redSmallImg;
        sideBUI.texture = redSmallImg;
        yield return new WaitForSeconds(0.5f);
        mainUI.texture = blueImg;
        sideAUI.texture = blueSmallImg;
        sideBUI.texture = blueSmallImg;
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
    }

    public void LocalSFXOneShot(AudioClip clip)
    {
        localSFX.PlayOneShot(clip);
    }

    public void SetDiveSpeed(float speed)
    {
        maxDiveSpeed = speed;
    }

    public void UpdateMusic(AudioClip music)
    {
        this.music.clip = music;
    }

    public void UpdateAmbient(AudioClip ambient)
    {
        this.ambient.clip = ambient;
    }
}
