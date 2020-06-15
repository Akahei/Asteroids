using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    public Ship PlayerShipPrefab;
    public float RespawnTime = 2;

    [Header("Asteroids")]
    public int StartAsteroidNum = 2;
    public int AsteroidsIncremment = 1;
    public Asteroid AsteroidPrefab;

    [Header("Ufo")]
    public Ufo UfoPrefab;
    public float UfoMinCooldoww = 20;
    public float UfoMaxCooldown = 40;
    [Tooltip("Минимальное растояние от верхней/нижней границы в процентах при спауне UFO")]
    public float UfoMinDistanceFromEdge = 0.2f;

    public int Score {get; private set;}
    public Ship PlayerShip {get; private set;}

    int nextRoundAsteroidsNum;
    List<Asteroid> asteroidsList = new List<Asteroid>();

    Ufo ufo = null;
    float nextUfoSpawnTime;

    float playerRespawnTime;

    static public GameManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
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
    }

    void Start()
    {
        RestartGame();
    }

    void RestartGame()
    {
        nextRoundAsteroidsNum = StartAsteroidNum;
        SpawnPlayer();
        ScheduleNextUfo();
        StartRound();
    }

    void StartRound()
    {
        SpawnAsteroids(nextRoundAsteroidsNum);
        nextRoundAsteroidsNum += AsteroidsIncremment;
    }

    void SpawnPlayer()
    {
        PlayerShip = Instantiate(PlayerShipPrefab);
    }

    void SpawnAsteroids(int num)
    {
        for (int i = 0; i < num; i++)
        {
            var asteroid = PoolManager.Instance.GetInstance(AsteroidPrefab);
            asteroid.Init(LevelBox.Instance.GetRandomPointOnEdge(), Random.Range(0f, 360f), asteroid.GetRandomSpeed());
        }
    }

    void ScheduleNextUfo() => nextUfoSpawnTime = Time.time + Random.Range(UfoMinCooldoww, UfoMaxCooldown);

    public void RegisterAsteroid(Asteroid asteroid)
    {
        asteroidsList.Add(asteroid);
    }

    public void OnDestructibleDestroyed(Destructible destructible)
    {
        Score += destructible.ScorePoints;
        if (PlayerShip && PlayerShip.gameObject == destructible.gameObject)
        {
            PlayerShip = null;
            playerRespawnTime = Time.time + RespawnTime;

        }
        else if (ufo && ufo.gameObject == destructible.gameObject)
        {
            ufo = null;
            ScheduleNextUfo();
        }
        else
        {
            var asteroid = destructible.GetComponent<Asteroid>();
            if (asteroid != null)
            {
                asteroidsList.Remove(asteroid);
                if (asteroidsList.Count == 0) StartRound();
            }
        }
    }
}
