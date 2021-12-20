using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubController : MonoBehaviour
{
    float diveSpeed = 2;

    public AudioSource cabinSFX;
    public AudioSource localSFX;
    public AudioSource music;
    public AudioSource ambient;

    public AudioClip diveSFX;

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            transform.position += Vector3.down * Time.deltaTime * diveSpeed;
        }
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
