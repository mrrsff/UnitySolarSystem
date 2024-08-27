using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float speed;
    public CelestialBody target;
    public GameObject explosionPrefab, asteroidPrefab;

    public void SetTarget(CelestialBody body)
    {
        target = body;
        StartCoroutine(Move());
    }
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    IEnumerator Move()
    {
        while (true)
        {
            if(target == null) 
            {
                Explode();
                yield break;
            }
            transform.position = Vector3.Lerp(transform.position, target.transform.position, speed * Time.deltaTime / Vector3.Distance(transform.position, target.transform.position));
            yield return null;
        }
    }
    public void SetBody(GameObject body)
    {
        if (transform.Find("Sphere") != null) Destroy(transform.Find("Sphere").gameObject);
        asteroidPrefab = body;
        Instantiate(asteroidPrefab, transform);
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("CelestialBody"))
        {
            Explode();
        }
    }
    void Explode()
    {
        var go = Instantiate(explosionPrefab, transform.position, transform.rotation);
        go.transform.localScale = transform.localScale;
        Destroy(go, 2f);
        Destroy(gameObject);
    }
}
