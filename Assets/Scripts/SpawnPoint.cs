using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    void OnEnable()
    {
        LevelManager.Instance.RegisterSpawnPoint(this);
    }

    public void Respawn()
    {
        Debug.Log("Respawn");
    }
}
