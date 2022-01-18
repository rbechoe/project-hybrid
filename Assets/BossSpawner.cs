using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
        EventSystem.AddListener(EventType.START_BOSS, StartBoss);
    }

    private void StartBoss()
    {
        gameObject.SetActive(true);
    }
}
