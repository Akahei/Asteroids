using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Asteroids")]
    public int StartAsteroidNum = 2;
    public int AsteroidsIncremment = 1;
    public Asteroid AsteroidPrefab;

    static public GameManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        SpawnAsteroid(StartAsteroidNum);
    }

    void SpawnAsteroid(int num)
    {
        for (int i = 0; i < num; i++)
        {
            var asteroid = PoolManager.PMInstance.GetInstance(AsteroidPrefab);
            asteroid.Init(LevelBox.Instance.GetRandomPointOnEdge(), Random.Range(0f, 360f), asteroid.GetRandomSpeed());
        }
    }
}
