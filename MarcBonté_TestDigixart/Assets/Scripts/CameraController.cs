using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Target"), SerializeField]
    private Transform positionTarget;
    [SerializeField]
    private Transform rotationTarget;

    [Header("Settings"), SerializeField, Range(0f, 10f)]
    private float positionSpeed = 1f;
    [SerializeField, Range(0f, 10f)]
    private float rotationSpeed = 1;

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
        UpdateCameraRotation();
	}

    protected void OnValidate()
    {
        if (positionTarget)
        {
            transform.position = positionTarget.position + cameraOffset;
        }
    }

    private void UpdateCameraPosition()
    {
        float dot = Vector3.Dot(positionTarget.forward.normalized, transform.forward.normalized);
        float multiplier = dot > dotAmount ? 1f : speedMultiplier;
        Vector3 finalPosition = positionTarget.position + cameraOffset;
        finalPosition.z = cameraOffset.z;

        transform.position = Vector3.Lerp(transform.position, finalPosition, (positionSpeed * multiplier) * Time.deltaTime);
    }

    private void UpdateCameraRotation()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(positionTarget.position - transform.position), rotationSpeed * Time.deltaTime);
    }
}
