using UnityEngine;

public class Projectile : MonoBehaviour, IDamagable
{
    [HideInInspector] public Transform target;
    [SerializeField] protected float speed = 1f;
    [SerializeField] protected float turnSpeed = 1f;
    [Space] [SerializeField] private int health;

    protected Rigidbody rb;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
