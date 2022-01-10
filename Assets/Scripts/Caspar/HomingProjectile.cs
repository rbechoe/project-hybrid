using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    [HideInInspector] public Transform target;
    [SerializeField] private float speed;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    private void FixedUpdate()
    {
        rb.AddForce(transform.forward * speed);
    }
    
    private void LateUpdate()
    {
        if (target != null)
        {
            transform.LookAt(target);
        }
    }
}
