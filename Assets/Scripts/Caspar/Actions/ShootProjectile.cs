using UnityEngine;

public class ShootProjectile : Action
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform origin;
    [SerializeField] private Transform target;

    public override bool PerformAction()
    {
        var instance = Instantiate(projectile, origin.position, Quaternion.identity);
        var component = instance.GetComponent<Projectile>();
        if (component != null)
        {
            component.target = this.target;
        }

        return false;
    }

    protected override void Reset()
    {
        
    }
}
