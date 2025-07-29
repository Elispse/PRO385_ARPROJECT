using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;
using Unity.AI.Navigation;

public class QRAnchorRaycastSpawner : MonoBehaviour
{
    [SerializeField] ARTrackedImageManager trackedImageManager;
    [SerializeField] ARRaycastManager raycastManager;
    [SerializeField] ARAnchorManager anchorManager;
    [SerializeField] ARPlaneManager planeManager;
    [SerializeField] Camera arCamera;

    private Dictionary<string, GameObject> spawnedObjects = new Dictionary<string, GameObject>();

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

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
            SpawnAnchorAtQRCenter(trackedImage);
        }
    }

    void SpawnAnchorAtQRCenter(ARTrackedImage trackedImage)
    {
        string id = trackedImage.trackableId.ToString();

        if (spawnedObjects.ContainsKey(id))
            return;

        // Convert the QR code's world position to screen position
        Vector3 worldPos = trackedImage.transform.position;
        Vector2 screenPos = arCamera.WorldToScreenPoint(worldPos);

        // Raycast from screen position into AR world planes
        if (raycastManager.Raycast(screenPos, hits, TrackableType.Planes))
        {
            Pose hitPose = hits[0].pose;
            var trackableId = hits[0].trackableId;
            var plane = planeManager.GetPlane(trackableId);

            if (plane == null)
            {
                Debug.LogWarning("Plane not found for the hit.");
                return;
            }

            ARAnchor anchor = anchorManager.AttachAnchor(plane, hitPose);
            if (anchor == null)
            {
                Debug.LogWarning("Failed to create anchor.");
                return;
            }
        }
        else
        {
            Debug.LogWarning("Raycast from QR center did not hit any planes.");
        }

    }
}
