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

    public AudioClip diveSFX, shootSFX;

    public CinemachineDollyCart CDC;

    public GameObject turret, bulletSpawn, projectile;
    private float turretX, turretY;
    private float aimSpeed = 40;
    private float shootCooldown = 0;
    private float offsetX, offsetY;
    
    public RawImage mainUI, sideAUI, sideBUI;

    private bool diving;
    private AudioSystem audioSystem;

    private void Start()
    {
        EventSystem.AddListener(EventType.TOGGLE_DIVE, ToggleDive);
        EventSystem.AddListener(EventType.DIVE_ON, DiveOn);
        EventSystem.AddListener(EventType.DIVE_OFF, DiveOff);
        EventSystem.AddListener(EventType.AIM_LEFT, AimLeft);
        EventSystem.AddListener(EventType.AIM_RIGHT, AimRight);
        EventSystem.AddListener(EventType.AIM_UP, AimUp);
        EventSystem.AddListener(EventType.AIM_DOWN, AimDown);
        EventSystem.AddListener(EventType.SHOOT, Shoot);
        EventSystem.AddListener(EventType.OFFSET_X_P, OffsetXUp);
        EventSystem.AddListener(EventType.OFFSET_X_M, OffsetXDown);
        EventSystem.AddListener(EventType.OFFSET_Y_P, OffsetYUp);
        EventSystem.AddListener(EventType.OFFSET_Y_M, OffsetYDown);

        EventSystem<int>.AddListener(EventType.DAMAGE_PLAYER, GetHit);
        EventSystem<float>.AddListener(EventType.TURRET_Y, TurretY);
        EventSystem<float>.AddListener(EventType.TURRET_X, TurretX);

        audioSystem = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSystem>();
    }

    private void ToggleDive()
    {
        diving = !diving;
    }

    private void DiveOn()
    {
        diving = true;
    }

    private void TurretY(float axisY)
    {
        turretY = axisY;
    }

    private void TurretX(float axisX)
    {
        turretX = axisX;
    }

    private void DiveOff()
    {
        diving = false;
    }

    private void AimLeft()
    {
        turretY -= aimSpeed * Time.deltaTime * 1.5f;
    }

    private void AimRight()
    {
        turretY += aimSpeed * Time.deltaTime * 1.5f;
    }

    private void AimUp()
    {
        turretX -= aimSpeed * Time.deltaTime;
    }

    private void AimDown()
    {
        turretX += aimSpeed * Time.deltaTime;
    }

    private void Shoot()
    {
        if (shootCooldown <= 0)
        {
            Instantiate(projectile, bulletSpawn.transform.position, turret.transform.rotation);
            shootCooldown = shootCooldownReset;
            audioSystem.ShootSFX(shootSFX, bulletSpawn.transform.position);
        }
    }

    private void GetHit(int damage)
    {
        StartCoroutine(HitEffect());
    }

    private IEnumerator HitEffect()
    {
        mainUI.color = Color.red;
        sideAUI.color = Color.red;
        sideBUI.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        mainUI.color = Color.white;
        sideAUI.color = Color.white;
        sideBUI.color = Color.white;
    }

    private void Update()
    {
        turret.transform.localEulerAngles = new Vector3(turretX + offsetX, turretY + offsetY, 0);

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

    public void OffsetXUp()
    {
        offsetX++;
    }

    public void OffsetYUp()
    {
        offsetY++;
    }

    public void OffsetXDown()
    {
        offsetX--;
    }

    public void OffsetYDown()
    {
        offsetY--;
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
        this.music.Stop();
        this.music.clip = music;
        this.music.Play();
    }

    public void UpdateAmbient(AudioClip ambient)
    {
        this.ambient.Stop();
        this.ambient.clip = ambient;
        this.ambient.Play();
    }
}
