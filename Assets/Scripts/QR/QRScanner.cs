using UnityEngine;
using ZXing;

public class QR : MonoBehaviour
{
    [SerializeField]
    private string lastResult;

    private WebCamTexture camTexture;
    private Color32[] cameraColorData;
    private int width, height;
    private Rect screenRect;


    private IBarcodeReader barcodeReader = new BarcodeReader
    {
        AutoRotate = false,
        Options = new ZXing.Common.DecodingOptions
        { 
            TryHarder = false
        }
    };

    private Result result;

    private void Start()
    {
        SetupWebcamTexture();
        PlayWebcamTexture();

        lastResult = "http://www.google.com";

        cameraColorData = new Color32[width * height];
        screenRect = new Rect(0, 0, Screen.width, Screen.height);
    }

    private void OnEnable()
    {
        PlayWebcamTexture();
    }

    private void OnDisable()
    {
        if (camTexture != null)
        {
            camTexture.Pause();
        }
    }

    private void Update()
    {
        // decoding from camera image
        camTexture.GetPixels32(cameraColorData);
        result = barcodeReader.Decode(cameraColorData, width, height);
        if (result != null)
        {
            lastResult = result.Text + " " + result.BarcodeFormat;
            Debug.Log(lastResult);
        }
    }

    private void OnGUI()
    {
        GUI.DrawTexture(screenRect, camTexture, ScaleMode.ScaleToFit);
        GUI.TextField(new Rect(10, 10, 256, 25), lastResult);
    }

    private void OnDestroy()
    {
        camTexture.Stop();
    }

    private void SetupWebcamTexture()
    {
        camTexture = new WebCamTexture();
        camTexture.requestedHeight = Screen.height;
        camTexture.requestedWidth = Screen.width;
    }

    private void PlayWebcamTexture()
    {
        if (camTexture != null)
        {
            camTexture.Play();
            width = camTexture.width;
            height = camTexture.height;
        }
    }
}
