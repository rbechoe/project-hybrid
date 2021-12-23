using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarRotator : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // enable objects on hit
        collision.gameObject.GetComponent<BallRemover>().EnableObject();
        Destroy(collision.gameObject.GetComponent<SphereCollider>());
    }
}
