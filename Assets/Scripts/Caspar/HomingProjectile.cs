using UnityEngine;

public class HomingProjectile : Projectile
{
    private void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;

        var targetDelta = target.position - transform.position;
        var angleDiff = Vector3.Angle(transform.forward, targetDelta);
        var cross = Vector3.Cross(transform.forward, targetDelta);

        rb.AddTorque(cross * angleDiff * turnSpeed);
    }
}
