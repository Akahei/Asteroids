using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Asteroids")]
    public int StartAsteroidNum = 2;
    public int AsteroidsIncremment = 1;
    public Asteroid AsteroidPrefab;

    public int Score {get; private set;}

    int nextRoundAsteroidsNum;
    List<Asteroid> asteroidsList = new List<Asteroid>();

    static public GameManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        RestartGame();
    }

    void RestartGame()
    {
        nextRoundAsteroidsNum = StartAsteroidNum;
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
            var asteroid = PoolManager.PMInstance.GetInstance(AsteroidPrefab);
            asteroid.Init(LevelBox.Instance.GetRandomPointOnEdge(), Random.Range(0f, 360f), asteroid.GetRandomSpeed());
        }
    }

    public void RegisterAsteroid(Asteroid asteroid)
    {
        asteroidsList.Add(asteroid);
    }

    public void OnDestructibleDestroyed(Destructible destructible)
    {
        Score += destructible.ScorePoints;
        var asteroid = destructible.GetComponent<Asteroid>();
        if (asteroid != null)
        {
            asteroidsList.Remove(asteroid);
            if (asteroidsList.Count == 0) StartRound();
        }
    }
}
