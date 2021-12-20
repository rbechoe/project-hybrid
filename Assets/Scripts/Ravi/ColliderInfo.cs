using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderInfo : MonoBehaviour
{
    public int hitType;
    public AudioClip clip;

    private void OnCollisionEnter(Collision collision)
    {
        switch(hitType)
        {
            case 0:
                collision.gameObject.GetComponent<SubController>()?.StartDive();
                break;

            case 1:
            case 2:
                collision.gameObject.GetComponent<SubController>()?.UpdateCabin(clip);
                break;
        }
        Destroy(gameObject);
    }
}
