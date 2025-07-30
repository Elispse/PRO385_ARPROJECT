using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class QRSpawnSimple : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager trackedImageManager;
    [SerializeField] private GameObject prefabToSpawn;

    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();

    void OnEnable()
    {
        trackedImageManager.trackablesChanged.AddListener(OnTrackedImagesChanged);
    }

    void OnDisable()
    {
        trackedImageManager.trackablesChanged.RemoveListener(OnTrackedImagesChanged);
    }

    void OnTrackedImagesChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            SpawnAtQRCode(trackedImage);
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            string id = trackedImage.trackableId.ToString();
            if (spawnedPrefabs.TryGetValue(id, out var obj))
            {
                // Update the position and rotation as long as it's being tracked
                obj.transform.position = trackedImage.transform.position;
                obj.transform.rotation = trackedImage.transform.rotation;
            }
        }

        foreach (var trackedImage in eventArgs.removed)
        {
            string id = trackedImage.Key.ToString();
            if (spawnedPrefabs.TryGetValue(id, out var obj))
            {
                Destroy(obj);
                spawnedPrefabs.Remove(id);
            }
        }
    }

    void SpawnAtQRCode(ARTrackedImage trackedImage)
    {
        string id = trackedImage.trackableId.ToString();

        if (spawnedPrefabs.ContainsKey(id))
            return;

        GameObject spawned = Instantiate(prefabToSpawn, trackedImage.transform.position, trackedImage.transform.rotation);
        // Optional: Parent to the tracked image so it follows automatically
        spawned.transform.SetParent(trackedImage.transform);
        spawnedPrefabs.Add(id, spawned);
        FindFirstObjectByType<PetUI>().SetPet(spawned.GetComponent<Pet>());
    }
}
