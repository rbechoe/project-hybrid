using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class StartGame : MonoBehaviour
{
    public GameObject[] objsToEnable;
    public GameObject[] toDisable;
    public float pathLength;
    float curPos;

    CinemachineDollyCart CDC;

    bool start, cleaning;

    void Start()
    {
        CDC = gameObject.GetComponent<CinemachineDollyCart>();
        EventSystem.AddListener(EventType.SHOOT, TriggerStart);

        foreach (GameObject gob in objsToEnable)
        {
            gob.SetActive(false);
        }
    }

    void Update()
    {
        if (start)
        {
            curPos += Time.deltaTime;
            CDC.m_Position = curPos;
            if (curPos > pathLength && !cleaning)
            {
                cleaning = true;
                CleanUp();
            }
        }
    }

    void TriggerStart()
    {
        if (!start)
        {
            start = true;
        }
    }

    void CleanUp()
    {
        foreach (GameObject gob in objsToEnable)
        {
            gob.SetActive(true);
        }
        foreach (GameObject gob in toDisable)
        {
            gob.SetActive(false);
        }

        EventSystem.RemoveListener(EventType.SHOOT, TriggerStart);
        EventSystem.InvokeEvent(EventType.GAME_START);
        Destroy(gameObject);
    }
}
