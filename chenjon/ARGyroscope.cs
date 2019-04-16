using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARGyroscope : MonoBehaviour
{
    //Gyroscope
    private Gyroscope gs;
    private Quaternion rotation; //rotation of the device and gyroscope
    //Device Camera
    private GameObject cameraContainer; //used to position camera with virtual objects
    private WebCamTexture deviceCamera;
    public RawImage background;
    public AspectRatioFitter fit;
    private bool startAR = false; //check when device meets requirements

    private void Start()
    {
        //Check if device has gyroscope
        if (!SystemInfo.supportsGyroscope)
        {
            Debug.Log("Device has no Gyroscope.");
            return; //end
        }

        //Check if device has back camera
        for(int i = 0; i < WebCamTexture.devices.Length; i++) //loop through device's cameras
        {
            if (!WebCamTexture.devices[i].isFrontFacing) //camera is back facing
            {
                deviceCamera = new WebCamTexture(WebCamTexture.devices[i].name, Screen.width, Screen.height);
                break; //found correct camera
            }
        }
        //else no back facing camera found
        if(deviceCamera == null) //there is no back facing camera
        {
            Debug.Log("Device has no back facing camera.");
            return;
        }

        //Device has both gyroscope and a back camera
        cameraContainer = new GameObject("Camera Container");
        cameraContainer.transform.position = transform.position;
        transform.SetParent(cameraContainer.transform);

        gs = Input.gyro;
        gs.enabled = true; //enable device's gyroscope
        //position camera with virtual objects
        cameraContainer.transform.rotation = Quaternion.Euler(90f, 0, 0);
        rotation = new Quaternion(0, 0, 1, 0);

        deviceCamera.Play(); //start using camera
        background.texture = deviceCamera; //background is whatever camera is seeing

        startAR = true;
    }

    private void Update()
    {
        if (startAR)
        {
            //update camera resolution
            float ratio = (float)deviceCamera.width / (float)deviceCamera.height;
            fit.aspectRatio = ratio;

            //float scaleY = cam.videoVerticallyMirrored //-1, else 1
            float scaleY = deviceCamera.videoVerticallyMirrored ? -1.0f : 1.0f;
            background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);
            int orientation = -deviceCamera.videoRotationAngle;
            background.rectTransform.localEulerAngles = new Vector3(0, 0, orientation);

            //update gyroscope on position
            transform.localRotation = gs.attitude * rotation;
        }
    }

}
