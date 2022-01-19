using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour, IDamagable
{
    [SerializeField] public Transform target;
    [SerializeField] protected float speed = 1f;
    [SerializeField] protected float turnSpeed = 1f;
    [Space] [SerializeField] private int health;
    [SerializeField] private int damage = 10;
    [SerializeField] private float damageRange = 1f;

    protected Rigidbody rb;
    private Material material;
    private Color startColor;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.LookAt(target.position);
        material = GetComponent<Renderer>().material;
        startColor = material.GetColor("_EmissionColor");
        OnStart();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, target.position) < damageRange)
        {
            EventSystem<int>.InvokeEvent(EventType.DAMAGE_PLAYER, damage);

            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        StartCoroutine(DamageFlash());

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

    private IEnumerator DamageFlash()
    {
        material.SetColor("_EmissionColor", Color.black);

        yield return new WaitForSeconds(0.1f);
        
        material.SetColor("_EmissionColor", startColor);
    }
}
