using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    [HideInInspector] public Transform target;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float turnSpeed = 1f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

        Vector3 targetDir = target.transform.position - transform.position;
        var step = 0.2f * Time.deltaTime * speed * 2;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);

        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
