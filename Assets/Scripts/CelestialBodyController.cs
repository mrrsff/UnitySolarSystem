using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public enum CelestialBodyType {Star, Planet, Moon}
public class CelestialBodyController : MonoBehaviour
{
    public static List<CelestialBody> celestialBodies = new List<CelestialBody>();
    public List<GameObject> celestialBodyPrefabs = new List<GameObject>();
    public GameObject celestialBodyPrefab, sunPrefab;
    public CelestialBody Sun;
    public float minOrbitRadius = 1f;
    public float maxOrbitRadius = 25f;
    public float minOrbitSpeed = 2f;
    public float maxOrbitSpeed = 10f;
    public float minBodyRadius = 1f;
    public float maxBodyRadius = 2f;
    public float minRotationSpeed = 2f;
    public float maxRotationSpeed = 10f;
    public float minMoonOrbitRadius = 0.1f;
    public float maxMoonOrbitRadius = 2f;
    public float minMoonBodyRadius = 0.05f;
    public float maxMoonBodyRadius = 0.6f;
    public int minPlanets = 6;
    public int maxPlanets = 15;
    public int minMoons = 0;
    public int maxMoons = 3;
    public int maxDepth = 3;
    Transform celestialBodyParent;
    int numPlanets;
    int numMoons;
    void Awake()
    {
        celestialBodyParent = new GameObject("Celestial Bodies").transform;
        numPlanets = Random.Range(minPlanets, maxPlanets);
        numMoons = 0;
        CreateCelestialBody(celestialBodyParent, 0);
    }

    void CreateCelestialBody(Transform parent, int depth)
    {
        if(depth == 0)
        {
            GameObject go = Instantiate(sunPrefab, parent);
            go.name = "Sun";
            CelestialBody cb = go.GetComponent<CelestialBody>();
            cb.orbitRadius = 0;
            cb.orbitSpeed = 0;
            cb.bodyRadius = Random.Range(minBodyRadius, maxBodyRadius) * 5f;
            cb.rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
            cb.orbitAxis = Vector3.zero;
            cb.rotationAxis = Vector3.up;
            cb.SetType(CelestialBodyType.Star);
            cb.SetOrbitBody(null);
            Sun = cb;
            CreateCelestialBody(go.transform, depth + 1);
        }
        else if(depth == 1)
        {
            for (int i = 0; i < numPlanets; i++)
            {
                GameObject go = Instantiate(celestialBodyPrefab);
                CelestialBody cb = go.GetComponent<CelestialBody>();
                cb.SetBody(RandomCelestialBody());
                var parentRadius = parent.GetComponent<CelestialBody>().bodyRadius;
                var parentOrbitSpeed = parent.GetComponent<CelestialBody>().orbitSpeed;
                cb.orbitRadius = Random.Range(minOrbitRadius, maxOrbitRadius) + parentRadius;
                cb.SetRadius(Random.Range(minBodyRadius, maxBodyRadius));
                cb.orbitSpeed = Random.Range(minOrbitSpeed, maxOrbitSpeed) + parentOrbitSpeed;
                cb.rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
                cb.orbitAxis = Random.onUnitSphere;
                cb.rotationAxis = Random.onUnitSphere;
                cb.SetType(CelestialBodyType.Planet);
                cb.SetOrbitBody(parent.GetComponent<CelestialBody>());
                cb.SetName(CelestialBodyNameGenerator.GeneratePlanetName());
                parent.GetComponent<CelestialBody>().AddOrbiter(cb);
                celestialBodies.Add(cb);
                CreateCelestialBody(go.transform, depth + 1);
            }
        }
        else if(depth == 2)
        {
            int numMoons = Random.Range(minMoons, maxMoons);
            this.numMoons += numMoons;
            for (int i = 0; i < numMoons; i++)
            {
                GameObject go = Instantiate(celestialBodyPrefab);
                CelestialBody cb = go.GetComponent<CelestialBody>();
                cb.SetBody(RandomCelestialBody());
                var parentRadius = parent.GetComponent<CelestialBody>().bodyRadius;
                var parentOrbitSpeed = parent.GetComponent<CelestialBody>().orbitSpeed;
                cb.orbitRadius = Random.Range(minMoonOrbitRadius, maxMoonOrbitRadius) + parentRadius;
                cb.SetRadius(Random.Range(minMoonBodyRadius, parentRadius * 0.3f));
                cb.orbitSpeed = Random.Range(minOrbitSpeed, maxOrbitSpeed);
                cb.rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
                cb.orbitAxis = Random.onUnitSphere;
                cb.rotationAxis = Random.onUnitSphere;
                cb.SetType(CelestialBodyType.Moon);
                cb.SetOrbitBody(parent.GetComponent<CelestialBody>());
                cb.SetName(CelestialBodyNameGenerator.GeneratePlanetName());
                parent.GetComponent<CelestialBody>().AddOrbiter(cb);
                celestialBodies.Add(cb);
                CreateCelestialBody(go.transform, depth + 1);
            }
        }
    }

    GameObject RandomCelestialBody()
    {
        return celestialBodyPrefabs[Random.Range(0, celestialBodyPrefabs.Count)];
    }
}
