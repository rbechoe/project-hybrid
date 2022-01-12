using UnityEngine;

public class BossHealth : MonoBehaviour, IDamagable
{
    [SerializeField] private int health;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}