using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public float spawnRate;
    public GameObject asteroidPrefab;
    public BoxCollider spawnArea;
    void Start()
    {
        StartCoroutine(SpawnAsteroid());   
    }

    IEnumerator SpawnAsteroid()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            SpawnAsteroid(ChooseTarget());
        }
    }
    CelestialBody ChooseTarget()
    {
        if(CelestialBodyController.celestialBodies.Count == 0) return null;
        return CelestialBodyController.celestialBodies[Random.Range(0, CelestialBodyController.celestialBodies.Count)];
    }
    void SpawnAsteroid(CelestialBody target)
    {
        if(target == null) return;
        GameObject go = Instantiate(asteroidPrefab);
        go.transform.position = new Vector3(Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x), Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y), Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z));
        Asteroid asteroid = go.GetComponent<Asteroid>();
        asteroid.SetTarget(target);
        asteroid.SetSpeed(target.orbitSpeed * 5);
    }
}
