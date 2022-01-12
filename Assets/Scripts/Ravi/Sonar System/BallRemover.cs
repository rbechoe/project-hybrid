using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRemover : MonoBehaviour
{
    bool fading = false;
    float aliveTimer;
    float aliveReset = 1;

    Material mat;

    private void Start()
    {
        mat = gameObject.GetComponent<MeshRenderer>().material;
        aliveTimer = aliveReset;
    }

    void Update()
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
                aliveTimer = aliveReset;
            }
        }
    }

    public void EnableObject()
    {
        enabled = true;
        gameObject.GetComponent<MeshRenderer>().enabled = true;
    }
}
