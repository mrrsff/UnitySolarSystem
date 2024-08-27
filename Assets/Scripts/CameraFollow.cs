using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f, zoomSpeed = 10f;
    public Vector3 offset;
    float minZoom = 2f, maxZoom = 25f;
    public static event System.Action<Transform> OnTargetChanged;
    private void Start() {
        SetTarget(GameObject.Find("Sun").transform);
    }
    private void LateUpdate()
    {
        DetectTarget();
        ListenMouse();
        FollowTarget();
    }

    private void ListenMouse()
    {
        var mouseWheel = Input.GetAxis("Mouse ScrollWheel");
        if (mouseWheel > 0f)
        {
            var direction = transform.position - target.position;
            var newOffset = offset - (direction.normalized * zoomSpeed * Time.deltaTime);
            if(newOffset.magnitude > minZoom) offset = newOffset;
        }
        else if (mouseWheel < 0f)
        {
            var direction = transform.position - target.position;
            offset = Vector3.ClampMagnitude(offset + (direction.normalized * zoomSpeed * Time.deltaTime), maxZoom);
        }

        if(Input.GetMouseButton(1))
        {
            var mouseDelta = new Vector2(Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));
            offset = Quaternion.AngleAxis(mouseDelta.x, Vector3.up) * offset;
            offset = Quaternion.AngleAxis(mouseDelta.y, transform.right) * offset;
        }
    }

    private void FollowTarget()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
            var targetRotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, smoothSpeed);
        }
    }

    private void DetectTarget()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 10000f))
            {
                if (hit.transform.CompareTag("CelestialBody"))
                {
                    SetTarget(hit.transform);
                }
            }
        }
    }
    public void SetTarget(Transform tr)
    {
        target = tr;
        minZoom = target.GetComponent<CelestialBody>().bodyRadius * 3f;
        maxZoom = target.GetComponent<CelestialBody>().bodyRadius * 10f;
        offset = (transform.position - target.position).normalized * minZoom;
        OnTargetChanged?.Invoke(target);
    }
}
