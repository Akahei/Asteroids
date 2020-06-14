using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Asteroids")]
    public int StartAsteroidNum = 2;
    public int AsteroidsIncremment = 1;
    public Asteroid AsteroidPrefab;

    [Header("Ufo")]
    public Ufo UfoPrefab;
    public float UfoMinCooldoww = 20;
    public float UfoMaxCooldown = 40;
    [Tooltip("Минимальное растояние UFO от верхней и нижней границ при спауне в процентах")]
    public float UfoMinDistanceFromEdge = 0.2f;

    public int Score {get; private set;}
    public Ship PlayerShip {get; private set;}

    int nextRoundAsteroidsNum;
    List<Asteroid> asteroidsList = new List<Asteroid>();

    Ufo ufo = null;
    float NextUfoSpawnTime;

    static public GameManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (ufo == null && Time.time >= NextUfoSpawnTime)
        {
            ufo = Instantiate(UfoPrefab);
            ufo.transform.position = LevelBox.Instance.GetRandomPointOnLeftRightEdge(UfoMinDistanceFromEdge);
            ufo.transform.rotation = Quaternion.Euler(0, 0, Random.value > 0.5f ? -90 : 90);
            ScheduleNextUfo();
        }
    }

    void Start()
    {
        PlayerShip = GameObject.FindObjectOfType<Ship>();
        RestartGame();
    }

    void RestartGame()
    {
        nextRoundAsteroidsNum = StartAsteroidNum;
        ScheduleNextUfo();
        StartRound();
    }

    void StartRound()
    {
        SpawnAsteroids(nextRoundAsteroidsNum);
        nextRoundAsteroidsNum += AsteroidsIncremment;
    }

    void SpawnAsteroids(int num)
    {
        for (int i = 0; i < num; i++)
        {
            var asteroid = PoolManager.Instance.GetInstance(AsteroidPrefab);
            asteroid.Init(LevelBox.Instance.GetRandomPointOnEdge(), Random.Range(0f, 360f), asteroid.GetRandomSpeed());
        }
    }

    void ScheduleNextUfo() => NextUfoSpawnTime = Time.time + Random.Range(UfoMinCooldoww, UfoMaxCooldown);

    public void RegisterAsteroid(Asteroid asteroid)
    {
        asteroidsList.Add(asteroid);
    }

    public void OnDestructibleDestroyed(Destructible destructible)
    {
        Score += destructible.ScorePoints;
        if (ufo && ufo.gameObject == destructible.gameObject)
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
