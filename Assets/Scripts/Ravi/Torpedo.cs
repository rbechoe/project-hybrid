using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torpedo : MonoBehaviour
{
    float speed = 1f;
    float maxSpeed = 20;
    float lifeTime = 10;

    void Start()
    {
        
    }

    void Update()
    {
        if (speed < maxSpeed) speed += 0.1f * Time.deltaTime;
        transform.position += transform.forward * speed;
        lifeTime -= Time.deltaTime;

        if (lifeTime < 0) Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // on hit enemy do damage and stuffles uwu xo
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<IDamagable>()?.TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
