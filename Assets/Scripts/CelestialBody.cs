using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CelestialBody : MonoBehaviour
{
    public string bodyName;
    public CelestialBodyType type;
    public CelestialBody orbitBody;
    public GameObject bodyPrefab;
    public float orbitRadius;
    public float orbitSpeed;
    public Vector3 orbitAxis;
    public float bodyRadius;
    public float rotationSpeed;
    public Vector3 rotationAxis;
    List<CelestialBody> orbiters = new List<CelestialBody>();
    Transform orbitersParent;
    public SphereCollider bodyCollider;
    /*LineRenderer CreateLineRenderer()
    {
        GameObject lineObject = new GameObject("Orbit Line");
        lineObject.transform.SetParent(transform);
        LineRenderer lr = lineObject.AddComponent<LineRenderer>();
        lr.loop = true;
        lr.positionCount = 360;
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.useWorldSpace = false;
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.material.color = Color.white;
        return lr;
    }*/
    IEnumerator Orbit()
    {
        while (true)
        {
            transform.RotateAround(orbitBody.transform.position, orbitAxis, orbitSpeed * Time.deltaTime);
            yield return null;
        }
    }
    IEnumerator Rotate()
    {
        while (true)
        {
            transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }
    public void SetName(string str)
    {
        bodyName = str;
        gameObject.name = str;
    }
    public void SetBody(GameObject body)
    {
        if(transform.Find("Sphere") != null) Destroy(transform.Find("Sphere").gameObject);
        bodyPrefab = body;
        Instantiate(bodyPrefab, transform);
    }
    public void SetOrbitBody(CelestialBody body)
    {
        orbitersParent = new GameObject("Orbiters").transform;
        orbitersParent.SetParent(transform);
        orbiters = new List<CelestialBody>();
        
        StartCoroutine(Rotate());
        transform.localScale = Vector3.one * bodyRadius;
        transform.rotation = Quaternion.identity;
        if(body == null)
        {
            transform.position = Vector3.zero;
            orbitBody = null;
            return;
        }
        orbitBody = body;
        transform.position = Random.onUnitSphere * orbitRadius + orbitBody.transform.position;
        StartCoroutine(Orbit());
    }
    public void AddOrbiter(CelestialBody cb)
    {
        orbiters.Add(cb);
        cb.orbitBody = this;
        cb.gameObject.transform.SetParent(orbitersParent);
    }
    public void SetType(CelestialBodyType type)
    {
        this.type = type;
    }
    public void SetRadius(float radius)
    {
        bodyRadius = radius;
        bodyCollider.radius *= 1.2f;
    }
}
