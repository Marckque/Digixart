using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Target"), SerializeField]
    private Transform target;

    [Header("Settings"), SerializeField, Range(0f, 10f)]
    private float speed = 1f;
    [SerializeField]
    private Vector3 cameraOffset;

    [SerializeField, Range(-1f, 1f)]
    private float dotAmount;
    [SerializeField, Range(1f, 5f)]
    private float speedMultiplier = 1f;


    public float finalCameraOffsetZ;

    protected void Update()
    {
        UpdateCameraPosition();
	}

    protected void OnValidate()
    {
        if (target)
        {
            transform.position = target.position + cameraOffset;
        }
    }

    private void UpdateCameraPosition()
    {
        float dot = Vector3.Dot(target.forward.normalized, transform.forward.normalized);
        float multiplier = dot > dotAmount ? 1f : speedMultiplier;

        transform.position = Vector3.Lerp(transform.position, target.position + cameraOffset, (speed * multiplier) * Time.deltaTime);
    }
}
