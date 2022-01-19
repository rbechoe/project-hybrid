using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour, IDamagable
{
    public float health = 100;
    public AudioClip hitSFX;
    public GameObject subMarine;
    AudioSystem audioSystem;

    void Start()
    {
        EventSystem<int>.AddListener(EventType.DAMAGE_PLAYER, TakeDamage);
        audioSystem = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSystem>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        audioSystem.ShootSFX(hitSFX, subMarine.transform.position);
    }
}
