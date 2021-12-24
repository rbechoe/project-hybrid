using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarScanner : MonoBehaviour
{
    public GameObject ball, scannerObj, radarObj;
    public Transform spawn;

    public LayerMask hitLayers;

    float scanTimer = 0;
    public float scanReset = 2f;
    public float scanRangeMultiplier = 1f;

    void Update()
    {
        scanTimer -= Time.deltaTime;

        if (scanTimer < 0)
        {
            scannerObj.transform.localScale = Vector3.one;
            StartCoroutine(ScanLogic());
            scanTimer = scanReset;
        }
    }

    IEnumerator ScanLogic()
    {
        List<GameObject> hits = new List<GameObject>();

        int step = 0;
        while (step < (scanReset * 20))
        {
            Collider[] enemies = Physics.OverlapSphere(transform.position, 2 * step * scanRangeMultiplier, hitLayers);

            foreach (Collider enemy in enemies)
            {
                if (enemy.CompareTag("Enemy") && !hits.Contains(enemy.gameObject))
                {
                    float size = enemy.GetComponent<EnemyInfo>().size;
                    // Logic: https://gyazo.com/26f23a1369e45a2106c830e53105f53c
                    float x = 0;
                    float y = 0;
                    float z = 0;
                    if (enemy.transform.position.x > transform.position.x)
                    {
                        x = (enemy.transform.position.x - transform.position.x) / 2f + size / 2f;
                    }
                    else
                    {
                        x -= (transform.position.x - enemy.transform.position.x) / 2f - size / 2f;
                    }
                    if (enemy.transform.position.z > transform.position.z)
                    {
                        z = (enemy.transform.position.z - transform.position.z) / 2f + size / 2f;
                    }
                    else
                    {
                        z -= (transform.position.z - enemy.transform.position.z) / 2f - size / 2f;
                    }

                    // spawn gob based on size and relative position
                    GameObject gob = Instantiate(ball, spawn.position, Quaternion.identity);
                    gob.transform.parent = spawn;
                    gob.transform.localPosition = new Vector3(x / scanRangeMultiplier, y, z / scanRangeMultiplier);
                    gob.transform.localScale = Vector3.one * size;

                    hits.Add(enemy.gameObject);
                }
            }

            // 20 steps per second
            scannerObj.transform.localScale = Vector3.one * 2 * step;
            radarObj.transform.localScale = new Vector3(1, 0, 1) * 2 * step;
            step++;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForEndOfFrame();
    }
}
