using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    public Button oneX , twoX, threeX;
    private void Start() {
        oneX.onClick.AddListener(() => Time.timeScale = 1f);
        twoX.onClick.AddListener(() => Time.timeScale = 2f);
        threeX.onClick.AddListener(() => Time.timeScale = 3f);
    }

    private void Update()
    {
        // Add screenshot functionality
        if(Input.GetKeyDown(KeyCode.F12))
        {
            ScreenShotUtility.TakeScreenshot();
        }
    }
}
