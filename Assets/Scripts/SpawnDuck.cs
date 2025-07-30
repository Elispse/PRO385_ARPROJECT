using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;

public class SpawnDuck : MonoBehaviour
{
    [SerializeField]
    private GameObject duckPrefab;
    [SerializeField]
    private GameObject spawnPrefab;
    [SerializeField]
    private NavMeshSurface navMeshSurface;

    private void Start()
    {
        StartCoroutine(SetupGame());
    }

    private IEnumerator SetupGame()
    {
        yield return new WaitForSeconds(0.1f);
        navMeshSurface.BuildNavMesh();
        yield return new WaitForEndOfFrame();
        GameObject pet = Instantiate(duckPrefab, spawnPrefab.transform.position, spawnPrefab.transform.rotation);
        FindFirstObjectByType<PetUI>().SetPet(pet.GetComponent<Pet>());
    }
}
