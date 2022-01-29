using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torpedo : MonoBehaviour
{
    private float speed = 1f;
    private float maxSpeed = 100;
    private float lifeTime = 5;
    private float life = 5;
    private float imoTimer = 0.05f;

    public GameObject sharkImpact;

    public AudioClip hit1, hit2;

    private AudioSystem audioSystem;

    private void Update()
    {
        if (speed < maxSpeed) speed += 0.5f * Time.deltaTime;
        transform.position += transform.forward * speed;
        lifeTime -= Time.deltaTime;

        if (lifeTime < 0) Destroy(gameObject);

        imoTimer -= Time.deltaTime;

        audioSystem = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSystem>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (imoTimer >= 0) return;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<IDamagable>()?.TakeDamage(1);
            Instantiate(sharkImpact, transform.position, Quaternion.identity);
            audioSystem.ShootSFX(hit1, transform.position);
            audioSystem.ShootSFX(hit2, transform.position);
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
