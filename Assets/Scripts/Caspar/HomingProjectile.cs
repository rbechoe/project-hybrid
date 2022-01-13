using UnityEngine;

public class HomingProjectile : Projectile
{
    [SerializeField] private float randomOffset = 0.2f;
    
    protected override void OnStart()
    {
        transform.forward = (transform.forward + Random.onUnitSphere * randomOffset).normalized;
    }
    
    private void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;

        var targetDelta = target.position - transform.position;
        var diff = Vector3.Angle(transform.forward, targetDelta);
        var cross = Vector3.Cross(transform.forward, targetDelta);

        rb.AddTorque(cross * (diff * turnSpeed));
    }
}
