using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject particlePrefab;
    private Animator _animator;        
    void Start()
    {
        _animator = GetComponent<Animator>();
        LevelManager.Instance.RegisterSpawnPoint(this);
    }

    public void Respawn()
    {
        _animator.SetTrigger("Respawn");
        Instantiate(particlePrefab, transform.position, Quaternion.identity);
    }
}
