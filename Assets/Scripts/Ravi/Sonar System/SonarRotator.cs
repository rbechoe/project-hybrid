using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarRotator : MonoBehaviour
{
    void Update()
    {
        transform.localEulerAngles += Vector3.up * 50 * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        // enable objects on hit
        collision.gameObject.GetComponent<BallRemover>().EnableObject();
        Destroy(collision.gameObject.GetComponent<SphereCollider>());
    }
}
