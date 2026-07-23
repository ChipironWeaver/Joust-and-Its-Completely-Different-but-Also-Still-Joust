using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private Animator _animator;        
    void Start()
    {
        _animator = GetComponent<Animator>();
        LevelManager.Instance.RegisterSpawnPoint(this);
    }

    public void Respawn()
    {
        _animator.SetTrigger("Respawn");
    }
}
