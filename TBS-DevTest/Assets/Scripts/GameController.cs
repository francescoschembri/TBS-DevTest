﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject[] hazards;
    [SerializeField] Vector3 spawnValues;
    [SerializeField] int hazardCount;
    [SerializeField] float spawnWait;
    [SerializeField] float startWait;
    [SerializeField] float waveWait;

    [SerializeField] Text scoreText;
    [SerializeField] Text restartText;
    [SerializeField] Text gameOverText;

    [SerializeField] KeyCode pauseKey = KeyCode.P;
    [SerializeField] KeyCode restartKey = KeyCode.R;
    [SerializeField] KeyCode increaseSFXVolumeKey = KeyCode.O;
    [SerializeField] KeyCode decreaseSFXVolumeKey = KeyCode.I;
    [SerializeField] float amountSFXVolume = 0.1f;
    [SerializeField] KeyCode increaseMusicVolumeKey = KeyCode.K;
    [SerializeField] KeyCode decreaseMusicVolumeKey = KeyCode.L;
    [SerializeField] float amountMusicVolume = 0.1f;
    [SerializeField] KeyCode muteKey = KeyCode.M;

    private bool gameOver;
    private bool restart;
    private int score;
    private bool paused;
    private bool muted = false;
    private float defaultTimeScale;
    private float SFXMultiplier = 1f;
    private AudioSource audioSource;

    private DangerVFX dangerVFX;
    private DangerSFX dangerSFX;

    public float GetSFXMultiplier() { return SFXMultiplier; }

    void Start()
    {
        dangerVFX = FindObjectOfType<DangerVFX>();
        dangerSFX = GetComponentInChildren<DangerSFX>();
        audioSource = GetComponent<AudioSource>();
        defaultTimeScale = Time.timeScale;
        paused = false;
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        //Audio keyboard controls
        if (Input.GetKeyDown(muteKey))
        {
            if (muted)
            {
                muted = false;
                AudioListener.volume = 1;
            }
            else
            {
                muted = true;
                AudioListener.volume = 0;
            }
        }
        else if (Input.GetKeyDown(increaseSFXVolumeKey))
        {
            if (SFXMultiplier > Mathf.Epsilon)
                SFXMultiplier -= amountSFXVolume;
        }
        else if (Input.GetKeyDown(decreaseSFXVolumeKey))
        {
            if (SFXMultiplier < 1)
                SFXMultiplier += amountSFXVolume;
        }
        else if (Input.GetKeyDown(decreaseMusicVolumeKey))
        {
            if (audioSource.volume > Mathf.Epsilon)
                audioSource.volume -= amountMusicVolume;
        }
        else if (Input.GetKeyDown(increaseMusicVolumeKey))
        {
            if (audioSource.volume < 1)
                audioSource.volume += amountMusicVolume;
        }

        //Pause-Unpause game
        if (Input.GetKeyDown(pauseKey))
        {
            if (paused)
            {
                paused = false;
                Time.timeScale = defaultTimeScale;
            }
            else
            {
                paused = true;
                Time.timeScale = 0;
            }
        }

        //Restart Game when Game Over occurs
        if (restart)
        {
            if (Input.GetKeyDown(restartKey))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }

    public void DangerAlertOn()
    {
        dangerVFX.enabled = true;
        dangerSFX.enabled = true;
    }

    public void DangerAlertOff()
    {
        dangerVFX.enabled = false;
        dangerSFX.enabled = false;
    }
}