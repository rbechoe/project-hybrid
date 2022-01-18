using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private int health;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
        
        EventSystem.InvokeEvent(EventType.BOSS_DAMAGED);
    }
}
