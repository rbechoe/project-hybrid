using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRemover : MonoBehaviour
{
    bool enabled = false;
    bool fading = false;
    float aliveTimer = 2;

    Material mat;

    private void Start()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        mat = gameObject.GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        if (enabled)
        {
            aliveTimer -= Time.deltaTime;

            if (fading)
            {
                aliveTimer -= Time.deltaTime;
                mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, mat.color.a - 1 * Time.deltaTime);

                if (aliveTimer <= 0)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                if (aliveTimer <= 0)
                {
                    fading = true;
                    aliveTimer = 2;
                }
            }
        }
    }

    public void EnableObject()
    {
        enabled = true;
        gameObject.GetComponent<MeshRenderer>().enabled = true;
    }
}
