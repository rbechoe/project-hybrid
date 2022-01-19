using UnityEngine;

public class WaveProjectile : Projectile
{
    [SerializeField] protected float waveAmplitude = 10f;
    [SerializeField] protected float waveFrequency = 1f;

    [SerializeField] protected float easingDistance = 5f;

    protected float distance;
    protected float easing;
    protected Vector3 dir;

    private Vector3 force;

    protected override void OnStart()
    {
        transform.forward = (target.position - transform.position).normalized;
    }

    private void Update()
    {
        distance = Vector3.Distance(transform.position, target.position);
        easing = distance < easingDistance ? Mathf.InverseLerp(0, easingDistance, distance) : 1;
        
        dir = (target.position - transform.position).normalized;

        rb.position += CalculateForce() * Time.deltaTime;
    }
    
    private void LateUpdate()
    {
        var rot = turnSpeed * Time.deltaTime;
        transform.Rotate(rot, rot, rot);
    }

    protected virtual Vector3 CalculateForce()
    {
        return Vector3.zero;
    }
}
