using UnityEngine;

public class Projectile : MonoBehaviour, IDamagable
{
    [HideInInspector] public Transform target;
    [SerializeField] protected float speed = 1f;
    [SerializeField] protected float turnSpeed = 1f;
    [Space] [SerializeField] private int health;
    [SerializeField] private int damage = 10;
    [SerializeField] private float damageRange = 1f;

    protected Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        OnStart();
    }

    protected virtual void Update()
    {
        if (Vector3.Distance(transform.position, target.position) < damageRange)
        {
            var component = GetComponent<GameController>();

            if (component != null)
            {
                component.TakeDamage(damage);
            }
            
            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(this.gameObject);
    }
    
    protected virtual void OnStart()
    {
        
    }
}
