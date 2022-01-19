using UnityEngine;

public class HomingProjectile : Projectile
{
    private void LateUpdate()
    {
        var targetDir = target.position - transform.position;

        var step = Time.deltaTime * turnSpeed;
        var newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);

        // Move forward
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}