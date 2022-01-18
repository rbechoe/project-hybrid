using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    private GameObject boss;
    
    private void Start()
    {
        boss = FindObjectOfType<Boss>().gameObject;
        boss.SetActive(false);
    }

    private void OnEnable()
    {
        EventSystem.AddListener(EventType.START_BOSS, StartBoss);
    }

    private void OnDisable()
    {
        EventSystem.RemoveListener(EventType.START_BOSS, StartBoss);
    }

    private void StartBoss()
    {
        boss.SetActive(true);
    }
}
