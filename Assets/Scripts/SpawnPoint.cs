using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    void Start()
    {
        LevelManager.Instance.RegisterSpawnPoint(this);
    }

    public void Respawn()
    {
        //Respawn Animation
    }
}
