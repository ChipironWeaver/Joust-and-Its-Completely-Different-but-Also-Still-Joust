using NaughtyAttributes;
using UnityEngine;

public class SpecialInstantiate : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private bool _instantiateOnPositon;
    [SerializeField, ShowIf("_instantiateOnPositon")] private Vector3 _positonOffset;
    [SerializeField, HideIf("_instantiateOnPositon")] private Vector3 _spawnPositon;
    [SerializeField] private bool _yeetTheInstantiateOnSpawn;
    [SerializeField, ShowIf("_yeetTheInstantiateOnSpawn")] private float _yeetForce;
    [SerializeField, ShowIf("_yeetTheInstantiateOnSpawn")] private bool _isSetLaunchDirection;
    [SerializeField, ShowIf("_isSetLaunchDirection")] private Vector2 _launchDirection;
    [SerializeField] private bool _registerInEnemies;
    [SerializeField, ShowIf("_registerInEnemies")] private bool _spawnAsEnemy;
    
    public void DoInstantiate()
    {
        if (_prefab == null) return;
        {DoInstantiatePrefab(_prefab);}
    }

    public void DoInstantiatePrefab(GameObject instantiate)
    {
        GameObject prefab = Instantiate(instantiate);
        prefab.transform.position = _instantiateOnPositon ? transform.position + _positonOffset : _spawnPositon ;
        if (_yeetTheInstantiateOnSpawn)
        {
            Vector2 direction = _isSetLaunchDirection ? new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) : _launchDirection;
            direction.Normalize();
            prefab.GetComponent<Rigidbody2D>().linearVelocity = direction * _yeetForce;
        }
        if (_registerInEnemies)
        {
            LevelManager.Instance.RegisterEnemies(prefab.GetComponent<EnemyBehavior>());
            if (_spawnAsEnemy)
            {
                LevelManager.Instance.Spawn(prefab);
            }
        }
    }
}
