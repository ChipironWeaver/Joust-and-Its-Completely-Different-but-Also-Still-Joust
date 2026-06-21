using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<EnemyBehavior> enemies;
    public List<PlayerHealth> players;
    public List<SpawnPoint> spawnPoints;

    private GameObject _playerGo;
    private GameObject _enemiesGo;
    private GameObject _spawnPointGo;
    void Awake()
    {
        Singleton();
        _playerGo = new GameObject
        {
            name = "Player",
            transform =
            {
                parent = transform
            }
        };
        _enemiesGo = new GameObject
        {
            name = "Enemies",
            transform =
            {
                parent = transform
            }
        };
        _spawnPointGo = new GameObject
        {
            name = "SpawnPoint",
            transform =
            {
                parent = transform
            }
        };
    }

    public void RegisterPlayer(PlayerHealth player)
    {
        players.Add(player);
        player.transform.SetParent(_playerGo.transform);
    }

    public void RegisterEnemies(EnemyBehavior enemy)
    {
        enemies.Add(enemy);
        enemy.transform.SetParent(_enemiesGo.transform);
    }

    public void RegisterSpawnPoint(SpawnPoint spawnPoint)
    {
        spawnPoints.Add(spawnPoint);
        spawnPoint.transform.SetParent(_spawnPointGo.transform);
    }
    
    public static LevelManager Instance{ get; private set; }
    void Singleton()
    {
        if (Instance !=null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}
