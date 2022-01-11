using UnityEngine;

public class ShootProjectile : Action
{
    [SerializeField] private GameObject[] projectiles;
    [SerializeField] private Transform origin;
    [SerializeField] private Transform target;

    public override bool PerformAction()
    {
        foreach (var projectile in projectiles)
        {
            var instance = Instantiate(projectile, origin.position, Quaternion.identity);
            var component = instance.GetComponent<Projectile>();
            if (component != null)
            {
                component.target = this.target;
            }
        }

        return true;
    }

    protected override void Reset()
    {

    }
}
