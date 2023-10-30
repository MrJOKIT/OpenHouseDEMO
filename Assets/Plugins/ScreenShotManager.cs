using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScreenShotManager : MonoBehaviour
{
    public GameObject screenShotPanel;
    public GameObject capturePanel;
    public GameObject saveImagePanel;
    public string gameName;

    private byte[] currentTexture;

    private string currentFilePath;

    public RawImage showImg;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator TakeScreenShot()
    {
        capturePanel.SetActive(false);
        yield return new WaitForEndOfFrame();
        Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenShot.ReadPixels(new Rect(0,0,Screen.width,Screen.height),0,0);
        screenShot.Apply();

        currentFilePath = Path.Combine(Application.temporaryCachePath, "temp_img.png");
        currentTexture = screenShot.EncodeToPNG();
        
        File.WriteAllBytes(currentFilePath,currentTexture);
        
        ShowImage();
        capturePanel.SetActive(true);
        Destroy(screenShot);
    }

    public void Capture()
    {
        StartCoroutine(TakeScreenShot());
    }

    public string ScreenShotName()
    {
        return string.Format("{0}_{1}.png", gameName, System.DateTime.Now.ToString("yyyy-M-dd_HH-mm-ss"));
    }

    public void ShowImage()
    {
        Texture2D tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        tex.LoadImage(currentTexture);
        showImg.material.mainTexture = tex;
        screenShotPanel.SetActive(true);
    }

    public void SaveToGallery()
    {
        NativeGallery.Permission permission =
            NativeGallery.SaveImageToGallery(currentFilePath, gameName, ScreenShotName(),
                (success, path) =>
                {
                    Debug.Log("Media save result: " + success + " " + path);
                    if (success)
                    {
                        saveImagePanel.SetActive(true);
#if UNITY_EDITOR
                        string editorFilePath = Path.Combine(Application.persistentDataPath, ScreenShotName());
                        File.WriteAllBytes(editorFilePath,currentTexture);
#endif
                    }
                });
        Debug.Log("Permission result: " + permission);
    }

    public void ShareImage()
    {
        new NativeShare().AddFile(currentFilePath).SetSubject("Share screenshot from this Game").SetText("Hello world!")
            .SetCallback((result,ShareTarget) => Debug.Log("Share result: " + ", Selected app: " + ShareTarget )).Share();
    }
}
