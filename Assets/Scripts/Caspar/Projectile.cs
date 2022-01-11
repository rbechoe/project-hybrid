using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector] public Transform target;
    [SerializeField] protected float speed = 1f;
    [SerializeField] protected float turnSpeed = 1f;

    protected Rigidbody rb;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
}
