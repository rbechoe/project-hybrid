using UnityEngine;

public class ShootProjectile : BossAction
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform origin;
    [SerializeField] private Transform target;
    [SerializeField] private int count = 1;

    public override bool PerformAction()
    {
        Debug.Log("Ik heb nu een cool projectielbarfje gelegd!!!");

        for (var i = 0; i < count; i++)
        {
            var dir = (target.position - origin.position).normalized;
            var instance = Instantiate(projectile, origin.position, Quaternion.identity);
            var component = instance.GetComponent<HomingProjectile>();
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
