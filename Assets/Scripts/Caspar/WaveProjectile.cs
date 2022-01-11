using UnityEngine;

public class WaveProjectile : Projectile
{
    [SerializeField] private float waveAmplitude = 10f;
    [SerializeField] private float waveFrequency = 1f;

    [SerializeField] private float easingDistance = 5f;

    private Transform child;

    protected override void Start()
    {
        base.Start();

        child = transform.GetChild(0);
        
        transform.forward = (target.position - transform.position).normalized;
    }

    private void FixedUpdate()
    {
        var distance = Vector3.Distance(transform.position, target.position);
        var easing = distance < easingDistance ? Mathf.InverseLerp(0, easingDistance, distance) : 1;
        
        var dir = (target.position - transform.position).normalized;
        
        rb.velocity = dir * speed;
        child.localPosition = Vector3.right * Mathf.Sin(Time.time * waveFrequency) * waveAmplitude * easing;
    }
}
