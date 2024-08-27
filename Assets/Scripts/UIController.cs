using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIController : MonoBehaviour
{
    public TextMeshProUGUI planetNameText;
    public Button nextPlanetButton, previousPlanetButton;
    public CameraFollow cameraFollow;
    private void Start() {
        cameraFollow = FindObjectOfType<CameraFollow>();
        CameraFollow.OnTargetChanged += OnTargetChanged;
        nextPlanetButton.onClick.AddListener(NextPlanet);
        previousPlanetButton.onClick.AddListener(PreviousPlanet);
    }
    private void OnTargetChanged(Transform target)
    {
        planetNameText.text = target.name;
    }
    public void NextPlanet()
    {
        var currentPlanet = cameraFollow.target;
        var curIndex = CelestialBodyController.celestialBodies.IndexOf(currentPlanet.GetComponent<CelestialBody>());
        var nextIndex = curIndex + 1;
        if(nextIndex >= CelestialBodyController.celestialBodies.Count) nextIndex = 0;
        cameraFollow.SetTarget(CelestialBodyController.celestialBodies[nextIndex].transform);
    }
    public void PreviousPlanet()
    {
        var currentPlanet = cameraFollow.target;
        var curIndex = CelestialBodyController.celestialBodies.IndexOf(currentPlanet.GetComponent<CelestialBody>());
        var nextIndex = curIndex - 1;
        if(nextIndex < 0) nextIndex = CelestialBodyController.celestialBodies.Count - 1;
        cameraFollow.SetTarget(CelestialBodyController.celestialBodies[nextIndex].transform);
    }

}
