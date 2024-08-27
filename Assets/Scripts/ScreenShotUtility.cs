using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScreenShotUtility
{
    public static void TakeScreenshot(string fileName = "Screenshot") // Default file name is 'Screenshot
    {
        // Add timestamp to file name
        string date = System.DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss");
        string folderPath = Application.dataPath + "/Screenshots/";
        if (!System.IO.Directory.Exists(folderPath))
        {
            System.IO.Directory.CreateDirectory(folderPath);
        }
        string path = folderPath + fileName + "_" + date + ".png";
        ScreenCapture.CaptureScreenshot(path, 1);
        Debug.Log("Screenshot saved to: " + path);
    }
}
