using UnityEngine;

public class ShootProjectile : Action
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform origin, target;

    public override bool PerformAction()
    {
        var instance = Instantiate(projectile, origin.position, origin.rotation);
        var component = instance.GetComponent<Projectile>();
        if (component != null)
        {
            component.target = this.target;
        }

        return true;
    }

    protected override void Reset()
    {
        
    }
}
