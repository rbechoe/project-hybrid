using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour, IDamagable
{
    // TODO SINGLETON
    public float health = 100;

    // Start is called before the first frame update
    void Start()
    {
        EventSystem<int>.AddListener(EventType.DAMAGE_PLAYER, TakeDamage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        // UPDATE UI
        // PUSH SFX
        // other attack related logic
    }
}
