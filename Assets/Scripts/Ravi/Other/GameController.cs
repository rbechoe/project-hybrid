using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour, IDamagable
{
    public int lives = 100;
    public int score = 0;
    public AudioClip hitSFX;
    public GameObject subMarine, theEnd;
    AudioSystem audioSystem;
    public TextMeshProUGUI livesTxt, scoresTxt;

    float graceTime = 1;
    bool scoreIncrement;

    void Start()
    {
        EventSystem.AddListener(EventType.LIVE_UP, LifeUp);
        EventSystem.AddListener(EventType.LIVE_DOWN, LifeDown);
        EventSystem.AddListener(EventType.GAME_START, GameStart);
        EventSystem.AddListener(EventType.GAME_END, GameEnd);
        EventSystem<int>.AddListener(EventType.DAMAGE_PLAYER, TakeDamage);
        EventSystem<int>.AddListener(EventType.SCORE_UP, ScoreIncrement);
        theEnd.SetActive(false);
        audioSystem = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSystem>();
    }

    void Update()
    {
        livesTxt.text = "Lives: " + lives;
        scoresTxt.text = "Score: " + score;

        if (scoreIncrement)
        {
            graceTime -= Time.deltaTime;
            if (graceTime <= 0)
            {
                graceTime = 1;
                score++;
            }
        }
    }

    public void GameStart()
    {
        scoreIncrement = true;
    }

    public void GameEnd()
    {
        scoreIncrement = false;
        theEnd.SetActive(true);
    }

    public void TakeDamage(int damage)
    {
        lives -= damage;
        audioSystem.ShootSFX(hitSFX, subMarine.transform.position);
    }

    public void ScoreIncrement(int amount)
    {
        score += amount;
    }

    public void LifeDown()
    {
        lives--;
    }

    public void LifeUp()
    {
        lives++;
    }
}
