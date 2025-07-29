using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloLab.ARFoundationQRTracking;
using Unity.AI.Navigation; // Ensure this using directive is present at the top


public class QRTrackers : MonoBehaviour
{
    [SerializeField]
    private string targetQRText;
    [SerializeField]
    private NavMeshSurface navMeshSurface;
    [SerializeField]
    private GameObject duck;

    private bool isDuckSpawned = false;

    private ARFoundationQRTracker qrTracker;

    private void Awake()
    {
        qrTracker = FindObjectOfType<ARFoundationQRTracker>();

        if (qrTracker == null)
        {
            Debug.LogError($"{nameof(ARFoundationQRTracker)} not found in scene.");
        }
        else
        {
            qrTracker.OnTrackedQRImagesChanged += QRTracker_OnTrackedQRImagesChanged;
        }

        HideObject();
    }

    private void OnDestroy()
    {
        if (qrTracker != null)
        {
            qrTracker.OnTrackedQRImagesChanged -= QRTracker_OnTrackedQRImagesChanged;
        }
    }

    private void QRTracker_OnTrackedQRImagesChanged(HoloLab.ARFoundationQRTracking.ARTrackedQRImagesChangedEventArgs eventArgs)
    {
        foreach (var addedQR in eventArgs.Added)
        {
            if (IsTarget(addedQR))
            {
                if (addedQR.TrackingReliable)
                {
                    ShowObject(addedQR);
                }
                return;
            }
        }

        foreach (var updatedQR in eventArgs.Updated)
        {
            if (IsTarget(updatedQR))
            {
                if (updatedQR.TrackingReliable)
                {
                    ShowObject(updatedQR);
                }
                return;
            }
        }

        foreach (var removedQR in eventArgs.Removed)
        {
            if (IsTarget(removedQR))
            {
                HideObject();
                return;
            }
        }
    }

    private void ShowObject(HoloLab.ARFoundationQRTracking.ARTrackedQRImage image)
    {
        var imageTransform = image.transform;

        transform.SetPositionAndRotation(imageTransform.position + new Vector3(0, -1f, 0), imageTransform.rotation);
        gameObject.SetActive(true);
        if (!isDuckSpawned) Instantiate(duck, imageTransform.position + new Vector3(0, -0.8f, 0), imageTransform.rotation);
        isDuckSpawned = true;
        StartCoroutine(RebuildNavMeshNextFrame());
    }

    private IEnumerator RebuildNavMeshNextFrame()
    {
        yield return new WaitForSeconds(0.2f); // Or: yield return null;
        navMeshSurface.BuildNavMesh();
    }


    private void HideObject()
    {
        gameObject.SetActive(false);
    }

    private bool IsTarget(HoloLab.ARFoundationQRTracking.ARTrackedQRImage image)
    {
        if (string.IsNullOrEmpty(targetQRText))
        {
            return true;
        }

        return image.Text == targetQRText;
    }
}