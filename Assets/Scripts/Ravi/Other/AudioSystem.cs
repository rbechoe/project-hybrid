using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    public List<AudioSource> activeSources = new List<AudioSource>();
    public List<AudioSource> inactiveSources = new List<AudioSource>();

    private void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<AudioSource>())
            {
                inactiveSources.Add(child.GetComponent<AudioSource>());
            }
        }
    }

    public void ShootSFX(AudioClip sfx, Vector3 position)
    {
        if (inactiveSources.Count <= 0) return;

        AudioSource source = inactiveSources[0];
        inactiveSources.Remove(source);
        activeSources.Add(source);

        source.gameObject.transform.position = position;
        source.PlayOneShot(sfx);
        source.GetComponent<AudioBaby>().isUsed = true;
    }

    public void ReturnSFX(AudioSource source)
    {
        activeSources.Remove(source);
        inactiveSources.Add(source);
        source.gameObject.transform.position = Vector3.zero;
    }
}
