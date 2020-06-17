using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    public Ship PlayerShipPrefab;
    public float RespawnTime = 2;
    public int Lifes = 5;

    [Header("Asteroids")]
    public int StartAsteroidNum = 2;
    public int AsteroidsIncremment = 1;
    public Asteroid AsteroidPrefab;
    public float TimeBeetweenWaveSpawn = 2;

    [Header("Ufo")]
    public Ufo UfoPrefab;
    public float UfoMinCooldoww = 20;
    public float UfoMaxCooldown = 40;
    [Tooltip("Минимальное растояние от верхней/нижней границы в процентах при спауне UFO")]
    public float UfoMinDistanceFromEdge = 0.2f;

    public int Score {get; private set;} = 0;
    public int CurrentPlayerLifes {get; private set;}
    public Ship PlayerShip {get; private set;}
    public bool GameStarted {get; private set;}

    public UnityAction<int> OnScoreChanged;
    public UnityAction<int> OnLifesChanged;
    public UnityAction OnGameStarted;
    public UnityAction OnGameOver;

    int nextWaveAsteroidsNum;
    List<Asteroid> asteroidsList = new List<Asteroid>();

    Ufo ufo = null;
    float nextUfoSpawnTime = float.MaxValue;
    float nextWaveTime = float.MaxValue;
    float playerRespawnTime = float.MaxValue;

    static public GameManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (!GameStarted) return;

        if (PlayerShip == null && Time.time >= playerRespawnTime)
        {
            SpawnPlayer();
        }

        if (ufo == null && Time.time >= nextUfoSpawnTime)
        {
            ufo = Instantiate(UfoPrefab);
            ufo.transform.position = LevelBox.Instance.GetRandomPointOnLeftRightEdge(UfoMinDistanceFromEdge);
            ufo.transform.rotation = Quaternion.Euler(0, 0, ufo.transform.position.x > 0 ? -90 : 90);
            ScheduleNextUfo();
        }

        if (asteroidsList.Count == 0 && Time.time >= nextWaveTime)
        {
            StartWave();
        }
    }

    public void NewGame()
    {
        GameStarted = false;
        foreach(var resetable in FindObjectsOfType<MonoBehaviour>().OfType<IResetable>())
        {
            resetable.ResetObject();
        }
        GameStarted = true;
        SetScore(0);
        SetPlayerLifes(Lifes);
        nextWaveAsteroidsNum = StartAsteroidNum;
        SpawnPlayer();
        ScheduleNextUfo();
        StartWave();
        if (OnGameStarted != null) OnGameStarted.Invoke();
    }

    void StartWave()
    {
        SpawnAsteroids(nextWaveAsteroidsNum);
        nextWaveAsteroidsNum += AsteroidsIncremment;
    }

    void SpawnPlayer()
    {
        PlayerShip = Instantiate(PlayerShipPrefab);
    }

    void SpawnAsteroids(int num)
    {
        asteroidsList.Clear();
        for (int i = 0; i < num; i++)
        {
            var asteroid = PoolManager.Instance.GetInstance(AsteroidPrefab);
            (var spawnPoint, var direction) = LevelBox.Instance.GetRandomPointOnEdge();
            var rotation = Quaternion.LookRotation(Vector3.forward, direction).eulerAngles.z;
            rotation += Random.Range(-90, 90);
            asteroid.Init(spawnPoint, rotation, asteroid.GetRandomSpeed());
        }
    }

    void GameOver()
    {
        GameStarted = false;
        if (OnGameOver != null) OnGameOver.Invoke();
    }

    void SetScore(int value)
    {
        Score = value;
        if (OnScoreChanged != null) OnScoreChanged.Invoke(Score);
    }

    void SetPlayerLifes(int value)
    {
        CurrentPlayerLifes = value;
        if (OnLifesChanged != null) OnLifesChanged.Invoke(CurrentPlayerLifes);
    }

    void ScheduleNextUfo() => nextUfoSpawnTime = Time.time + Random.Range(UfoMinCooldoww, UfoMaxCooldown);
    void ScheduleNextWave() => nextWaveTime = Time.time + TimeBeetweenWaveSpawn;

    public void RegisterAsteroid(Asteroid asteroid)
    {
        asteroidsList.Add(asteroid);
    }

    public void OnDestructibleExplode(Destructible destructible, GameObject instigator)
    {
        if (!GameStarted) return;

        var scoreObject = destructible.GetComponent<ScoreObject>();
        if (scoreObject && scoreObject.ScorePoints > 0 && instigator.GetComponent<PlayerController>() != null)
        {
            SetScore(Score + scoreObject.ScorePoints);
        }
        
        if (PlayerShip && PlayerShip == destructible)
        {
            PlayerShip = null;
            SetPlayerLifes(CurrentPlayerLifes - 1);
            if (CurrentPlayerLifes == 0)
            {
                GameOver();
            }
            else
            {
                playerRespawnTime = Time.time + RespawnTime;
            }

        }
        else if (ufo && ufo == destructible)
        {
            ufo = null;
            ScheduleNextUfo();
        }
        else
        {
            var asteroid = destructible as Asteroid;
            if (asteroid != null)
            {
                asteroidsList.Remove(asteroid);
                if (asteroidsList.Count == 0) ScheduleNextWave();
            }
        }
    }
}
