using UnityEngine;

public class BossDamageable : MonoBehaviour, IDamagable
{
    [SerializeField] private float damageMultiplier = 1;
    private BossHealth bossHealth;

    private void Start()
    {
        bossHealth = FindObjectOfType<BossHealth>();
    }
    public void TakeDamage(int damage)
    {
        var trueDamage = Mathf.RoundToInt(damage * damageMultiplier);
        bossHealth.TakeDamage(trueDamage);
    }
}
