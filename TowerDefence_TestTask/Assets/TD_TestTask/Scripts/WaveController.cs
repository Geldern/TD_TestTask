using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class WaveController : MonoBehaviour
{
    public WaveData[] Waves;
    private WaveData CurrentWave;
    private float CurrentWaveDuration;
    public TextMeshProUGUI SpawnText;

    private float SpawnCountdown = 1.5f;
    private float spawnTimer;
    private bool waveOn;
    [HideInInspector]
    public int WaveNum;

    public GameObject Enemy;
    public float PauseBetweenWaves = 3f;
    private float waveCountdown;
    private GameObject[] EnemiesAlive;
    public GameManager gameManager;
    public Transform Spawn;
    void Start()
    {
        WaveNum = -1;
        CurrentWave = Waves[WaveNum];
    }
    
    void Update()
    {
        EnemiesAlive = GameObject.FindGameObjectsWithTag("Enemy"); 
        CurrentWaveDuration -= Time.deltaTime;

        if (CurrentWaveDuration <= 0)
        {
            waveOn = false;
            waveCountdown -= Time.deltaTime;
        }
        if (waveCountdown <= 0 && EnemiesAlive.Length == 0)
        {
            NextWave();
        }
        spawnTimer -= Time.deltaTime;

        if (waveOn && spawnTimer <= 0)
        {
            spawnTimer = SpawnCountdown;
            Instantiate(Enemy, Spawn.position, Spawn.rotation);
        }

    }

    void NextWave()
    {
        if (WaveNum < Waves.Length)
        {
            WaveNum++;
            waveOn = true;
            waveCountdown = PauseBetweenWaves;
            CurrentWave = Waves[WaveNum];
            CurrentWaveDuration = CurrentWave.Duration;
            SpawnText.text = "Wave " + (WaveNum + 1) + "/" + Waves.Length;
        }
        else gameManager.WinGame();
    }
    
}
