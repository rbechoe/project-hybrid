using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    private GameObject boss;

    private Vector3 originalPosition;
    
    private void Start()
    {
        boss = FindObjectOfType<Boss>().gameObject;
        originalPosition = boss.transform.position;
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
        boss.transform.position = originalPosition;
    }
}
