using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBaby : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioSystem audioSystem;

    public bool isUsed;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSystem = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSystem>();
    }

    private void Update()
    {
        if (isUsed && !audioSource.isPlaying)
        {
            audioSystem.ReturnSFX(audioSource);
            isUsed = false;
        }
    }
}
