using UnityEngine;

public class ProjectileHealth : MonoBehaviour, IDamagable
{
    [SerializeField] private int health = 100;

    public void TakeDamage(int damage)
    {
        throw new System.NotImplementedException();
    }
}
