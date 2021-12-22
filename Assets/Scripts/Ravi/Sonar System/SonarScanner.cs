using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarScanner : MonoBehaviour
{
    public GameObject ball, scanner;
    public Transform spawn;

    public LayerMask hitLayers;

    float scanTimer = 0;

    void Start()
    {
        
    }

    void Update()
    {
        scanTimer -= Time.deltaTime;

        if (scanTimer < 0)
        {
            StartCoroutine(ScanLogic());
            scanTimer = 5;
        }
    }

    IEnumerator ScanLogic()
    {
        List<GameObject> hits = new List<GameObject>();

        int step = 0;
        while (step < 50)
        {
            scanner.transform.localScale = Vector3.one * step * 2;

            Collider[] enemies = Physics.OverlapSphere(transform.position, 2 * step, hitLayers);

            foreach (Collider enemy in enemies)
            {
                if (enemy.CompareTag("Enemy") && !hits.Contains(enemy.gameObject))
                {
                    // Logic: https://gyazo.com/26f23a1369e45a2106c830e53105f53c
                    float x = 0;
                    float y = 0;
                    float z = 0;
                    if (enemy.transform.position.x > transform.position.x)
                    {
                        x = (enemy.transform.position.x - transform.position.x) / 2f;
                    }
                    else
                    {
                        x -= (transform.position.x - enemy.transform.position.x) / 2f;
                    }
                    if (enemy.transform.position.z > transform.position.z)
                    {
                        z = (enemy.transform.position.z - transform.position.z) / 2f;
                    }
                    else
                    {
                        z -= (transform.position.z - enemy.transform.position.z) / 2f;
                    }

                    // spawn gob based on size and relative position
                    GameObject gob = Instantiate(ball, spawn.position, Quaternion.identity);
                    gob.transform.parent = spawn;
                    gob.transform.localPosition = new Vector3(x, y, z);
                    gob.transform.localScale = Vector3.one * enemy.GetComponent<EnemyInfo>().size;

                    hits.Add(enemy.gameObject);
                }
            }

            // 10 steps per second
            step++;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForEndOfFrame();
    }
}
