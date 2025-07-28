using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using ZXing;

using Unity.XR.CoreUtils;

public class QR : MonoBehaviour
{
    [SerializeField]
    private string lastResult;
    [SerializeField]
    private ARSession session;
    [SerializeField]
    private XROrigin sessionOrigin;
    [SerializeField]
    private ARCameraManager cameraManager;
    [SerializeField]
    private VoidEvent spawnPet;

    private Texture2D cameraImageTexture;

    private IBarcodeReader barcodeReader = new BarcodeReader
    {
        AutoRotate = false,
        Options = new ZXing.Common.DecodingOptions
        { 
            TryHarder = false
        }
    };

    private Result result;

    private void OnEnable()
    {
        cameraManager.frameReceived += OnCameraFrameReceived;
    }

    private void OnDisable()
    {
        cameraManager.frameReceived -= OnCameraFrameReceived;
    }

    private void OnCameraFrameReceived(ARCameraFrameEventArgs eventArgs)
    {
        if (!cameraManager.TryAcquireLatestCpuImage(out XRCpuImage cpuImage))
        {
            Debug.LogWarning("Failed to acquire latest CPU image.");
            return;
        }

        var conversionParams = new XRCpuImage.ConversionParams
        {
            inputRect = new RectInt(0, 0, cpuImage.width, cpuImage.height),

            outputDimensions = new Vector2Int(cpuImage.width / 2, cpuImage.height / 2),

            outputFormat = TextureFormat.RGBA32,

            transformation = XRCpuImage.Transformation.MirrorY
        };

        int size = cpuImage.GetConvertedDataSize(conversionParams);

        var buffer = new NativeArray<byte>(size, Allocator.Temp);

        cpuImage.Convert(conversionParams, buffer);

        cpuImage.Dispose();

        cameraImageTexture = new Texture2D(
            conversionParams.outputDimensions.x,
            conversionParams.outputDimensions.y,
            conversionParams.outputFormat,
            false);

        cameraImageTexture.LoadRawTextureData(buffer);
        cameraImageTexture.Apply();

        buffer.Dispose();

        result = barcodeReader.Decode(cameraImageTexture.GetPixels32(),
            cameraImageTexture.width,
            cameraImageTexture.height);

        if (result != null)
        {
            lastResult = result.Text + " " + result.BarcodeFormat;
            print(lastResult);

            if (result.Text == "https://Spawn")
            {
                spawnPet.RaiseEvent();
            }
        }
    }
}