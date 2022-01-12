using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScanner : MonoBehaviour
{

    public GameObject threat, nothreat, radarObj;
    public Transform spawn;

    List<GameObject> enemies = new List<GameObject>();
    List<float> timers = new List<float>();

    public float scanRangeMultiplier = 1;
    float graceTimer = 2;

    void Update()
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

        transform.parent.localEulerAngles += Vector3.up * 100 * Time.deltaTime;
        radarObj.transform.localEulerAngles = transform.parent.localEulerAngles;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !enemies.Contains(collision.gameObject))
        {
            GameObject enemy = collision.gameObject;
            timers.Add(graceTimer);
            enemies.Add(collision.gameObject);

            float size = enemy.GetComponent<EnemyInfo>().size;
            float x = 0;
            float y = 0;
            float z = 0;
            if (enemy.transform.position.x > transform.parent.position.x)
            {
                x = (enemy.transform.position.x - transform.parent.position.x) / 2f + size / 2f;
            }
            else
            {
                x -= (transform.parent.position.x - enemy.transform.position.x) / 2f - size / 2f;
            }
            if (enemy.transform.position.z > transform.parent.position.z)
            {
                z = (enemy.transform.position.z - transform.parent.position.z) / 2f + size / 2f;
            }
            else
            {
                z -= (transform.parent.position.z - enemy.transform.position.z) / 2f - size / 2f;
            }

            // spawn gob based on size and relative position
            Vector3 spawnPos = new Vector3(10000 + x, y, 10000 + z);
            GameObject gob = null;
            if (enemy.GetComponent<EnemyInfo>().dead)
                gob = Instantiate(nothreat, spawnPos, Quaternion.identity);
            else
                gob = Instantiate(threat, spawnPos, Quaternion.identity);
            gob.transform.localScale = Vector3.one * size;
        }
    }
}
