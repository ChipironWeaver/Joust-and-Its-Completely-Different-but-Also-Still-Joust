using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using NaughtyAttributes;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private bool _autoStartSpawning = true;
    [SerializeField] private float _spawnCooldown;
    [SerializeField] private float _waveCooldown;
    [SerializeField] private int _maxWave;
    
    [SerializeField,CurveRange(0, 0, 1, 1)] private AnimationCurve _enemyCountCurve;
    [SerializeField] private Vector2 _enemyCountRange;
    
    [SerializeField] private List<EnemyWeight> _enemyWeights;
    
    [SerializeField] private BackgroundMaterialChanger _backgroundMaterialChanger;
        
    public bool canSpawn = true;
    [ReadOnly] public int currentWave;
    [ReadOnly] public int enemiesToSpawn;

    private SpecialInstantiate _spInstantiate;

    private void OnEnable()
    {
        Singleton();
        Actions.EnemyDeath += TryWaves;
    }

    private void OnDisable()
    {
        Actions.EnemyDeath -= TryWaves;
    }

    private void Start()
    {
        _spInstantiate = GetComponent<SpecialInstantiate>();
        if (_autoStartSpawning) StartWave();
    }

    private void TryWaves()
    {
        if (enemiesToSpawn == 0 && LevelManager.Instance.enemies.Count == 0)
        {
            StartWave();
        }
    }
    private void StartWave()
    {
        _backgroundMaterialChanger.ApplyColorsOnMaterial(Random.Range(1,_backgroundMaterialChanger.backgroundColors.Count));
        currentWave ++;
        if (currentWave > _maxWave)
        {
            Actions.Win?.Invoke();
            return;
        }
        Actions.WaveStart?.Invoke();
        enemiesToSpawn = ChipironUtility.RandomRound(ChipironUtility.EvaluateVector2( _enemyCountRange,  _enemyCountCurve.Evaluate(currentWave / (float)_maxWave)));
        Invoke(nameof(SpawnEnemy), _waveCooldown);
    }

    private void SpawnEnemy()
    {
        if (!canSpawn) return;
        if (enemiesToSpawn <= 0) return;
        _spInstantiate.DoInstantiatePrefab(ChoseEnemy());
        enemiesToSpawn--;
        Invoke(nameof(SpawnEnemy), _spawnCooldown);
    }

    private GameObject ChoseEnemy()
    {
        GameObject choice = null;
        List<float> weights = new List<float>();
        float totalWeight = 0;
        foreach (EnemyWeight weight in _enemyWeights)
        {
            weights.Add(ChipironUtility.EvaluateVector2(weight.weightRangePerWave,weight.weightCurve.Evaluate(currentWave / (float)_maxWave)));
            totalWeight += weights.Last();
        }
        float targetWeight = Random.Range(0, totalWeight);
        for (int i = 0; i < weights.Count; i++)
        {
            targetWeight -= weights[i];
            choice = _enemyWeights[i].prefab;
            if (targetWeight <= 0) break;
        }
        return choice;
    }
    
    
    public static WaveManager Instance{ get; private set; }
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

[Serializable]
public class EnemyWeight
{
    [CurveRange(0, 0, 1, 1)] public AnimationCurve weightCurve;
    public Vector2 weightRangePerWave;
    public GameObject prefab;
}
