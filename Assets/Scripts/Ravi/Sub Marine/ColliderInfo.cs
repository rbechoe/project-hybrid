using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderInfo : MonoBehaviour
{
    public TriggerTypes types = TriggerTypes.None;
    
    public AudioClip oneTimeClip;
    public AudioClip newAmbience;
    public AudioClip newMusic;
    public float maxDiveSpeed = 16;
    public GameObject removeSonarDot;

    private void OnCollisionEnter(Collision collision)
    {
        if ((types & TriggerTypes.None) != 0)
        {
            Debug.Log(gameObject.name + " has ColliderInfo without actions!");
            return;
        }

        if (!collision.transform.CompareTag("SubMarine")) return;

        SubController sub = collision.gameObject.GetComponent<SubController>();
        print("Fired actions");

        if ((types & TriggerTypes.Music) != 0)
        {
            sub.UpdateMusic(newMusic);
        }
        if ((types & TriggerTypes.Ambience) != 0)
        {
            sub.UpdateAmbient(newAmbience);
        }
        if ((types & TriggerTypes.Speed) != 0)
        {
            sub.SetDiveSpeed(maxDiveSpeed);
        }
        if ((types & TriggerTypes.SFX) != 0)
        {
            sub.LocalSFXOneShot(oneTimeClip);
        }
        if ((types & TriggerTypes.StartDive) != 0)
        {
            sub.StartDive();
        }
        if ((types & TriggerTypes.StartBoss) != 0)
        {
            EventSystem.InvokeEvent(EventType.START_BOSS);
            Destroy(removeSonarDot); // removes directional light
        }
        if ((types & TriggerTypes.BossRadar) != 0)
        {
            Destroy(removeSonarDot); // removes big boss dot
        }
        Destroy(gameObject);
    }
}

[Flags]
public enum TriggerTypes
{
    None        = 0,
    Music       = 1,
    Ambience    = 2,
    Speed       = 4,
    SFX         = 8,
    StartDive   = 16,
    StartBoss   = 32,
    BossRadar   = 64,
}