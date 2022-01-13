using UnityEngine;

public class ShootProjectile : Action
{
    [SerializeField] private GameObject[] projectiles;
    [SerializeField] private Transform origin;
    [SerializeField] private Transform target;
    [SerializeField] private float fireRate = 0.5f;

    private float nextFire;
    private int index;

    public override bool PerformAction()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            
            var instance = Instantiate(projectiles[index], origin.position, Quaternion.identity);
            var component = instance.GetComponent<Projectile>();
            if (component != null)
            {
                component.target = this.target;
            }

            if (index < projectiles.Length - 1)
            {
                index++;
            }
            else return true;
        }

        return false;
    }

    protected override void Reset()
    {
        nextFire = 0;
        index = 0;
    }
}
