using System.Collections.Generic;
using UnityEngine;

public class EnemyScanner : MonoBehaviour
{
    public GameObject threat, nothreat, radarObj;
    public Transform spawn;

    private List<GameObject> enemies = new List<GameObject>();
    private List<float> timers = new List<float>();

    public float scanRangeMultiplier = 1;
    private float graceTimer = 2;

    private void Update()
    {
        for (int i = 0; i < timers.Count; i++)
        {
            timers[i] -= Time.deltaTime;
            if (timers[i] <= 0)
            {
                timers.RemoveAt(i);
                enemies.RemoveAt(i);
            }
        }

        transform.parent.localEulerAngles += Vector3.up * (100 * Time.deltaTime);
        Vector3 spriteRotation = Vector3.back * transform.parent.localEulerAngles.y;
        radarObj.transform.localEulerAngles = spriteRotation;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !enemies.Contains(collision.gameObject))
        {
            GameObject enemy = collision.gameObject;
            timers.Add(graceTimer);
            enemies.Add(collision.gameObject);

            float size = enemy.GetComponent<EnemyInfo>().size;
            float x = 0f;
            float z = 0f;

            if (enemy.transform.position.x > transform.parent.position.x)
            {
                x = (enemy.transform.position.x - transform.parent.position.x) * 0.5f + size * 0.5f;
            }
            else
            {
                x -= (transform.parent.position.x - enemy.transform.position.x) * 0.5f - size * 0.5f;
            }

            if (enemy.transform.position.z > transform.parent.position.z)
            {
                z = (enemy.transform.position.z - transform.parent.position.z) * 0.5f + size * 0.5f;
            }
            else
            {
                z -= (transform.parent.position.z - enemy.transform.position.z) * 0.5f - size * 0.5f;
            }

            x *= scanRangeMultiplier;
            z *= scanRangeMultiplier;

            // spawn gob based on size and relative position
            Vector3 spawnPos = new Vector3(10000 + x, 0, 10000 + z);
            GameObject gob = Instantiate(enemy.GetComponent<EnemyInfo>().dead ? nothreat : threat, spawnPos, Quaternion.identity);
            gob.transform.localScale = Vector3.one * size;
        }
    }
}
