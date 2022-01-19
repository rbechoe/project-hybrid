using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torpedo : MonoBehaviour
{
    float speed = 1f;
    float maxSpeed = 100;
    float lifeTime = 5;
    float life = 5;
    float imoTimer = 0.05f;

    public GameObject sharkImpact;

    void Update()
    {
        if (speed < maxSpeed) speed += 0.5f * Time.deltaTime;
        transform.position += transform.forward * speed;
        lifeTime -= Time.deltaTime;

        if (lifeTime < 0) Destroy(gameObject);

        imoTimer -= Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (imoTimer >= 0) return;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<IDamagable>()?.TakeDamage(1);
            Instantiate(sharkImpact, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            life--;
            collision.gameObject.GetComponent<IDamagable>()?.TakeDamage(1);
            Instantiate(sharkImpact, transform.position, Quaternion.identity);

            if (life <= 0) Destroy(gameObject);
        }
    }
}
