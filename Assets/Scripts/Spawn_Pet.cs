using UnityEngine;

public class Spawn_Pet : MonoBehaviour
{
    [SerializeField] GameObject petPrefab;
    private bool petSpawned = false;
    public void SpawnPet()
    {
        petSpawned = true;
        if (!petSpawned)
        {

        }
    }
}
