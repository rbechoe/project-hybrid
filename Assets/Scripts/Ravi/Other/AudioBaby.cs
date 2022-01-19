using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBaby : MonoBehaviour
{
    AudioSource source;
    AudioSystem system;

    public bool isUsed;

    private void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        system = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSystem>();
    }

    void Update()
    {
        if (isUsed && !source.isPlaying)
        {
            system.ReturnSFX(source);
            isUsed = false;
        }
    }
}
