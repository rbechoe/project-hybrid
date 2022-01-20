using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private int health;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            EventSystem<int>.InvokeEvent(EventType.SCORE_UP, 5000);
            EventSystem.InvokeEvent(EventType.GAME_END);
            Destroy(this.gameObject);
        }
        
        EventSystem.InvokeEvent(EventType.BOSS_DAMAGED);
    }
}
