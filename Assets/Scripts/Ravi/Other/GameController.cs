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

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
