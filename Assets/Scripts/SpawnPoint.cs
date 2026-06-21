using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    void Start()
    {
        LevelManager.Instance.RegisterSpawnPoint(this);
    }

    public void Respawn()
    {
        Debug.Log("Respawn");
    }
}
