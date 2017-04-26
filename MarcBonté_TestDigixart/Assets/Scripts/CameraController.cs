using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Target"), SerializeField]
    private Transform m_PositionTarget;
    [SerializeField]
    private Transform m_RotationTarget;

    [Header("Settings"), SerializeField, Range(0f, 10f)]
    private float m_PositionSpeed = 1f;
    [SerializeField, Range(0f, 10f)]
    private float m_RotationSpeed = 1;

    [SerializeField]
    private Vector3 m_CameraOffset;

    [SerializeField, Range(-1f, 1f)]
    private float m_DotAmount;
    [SerializeField, Range(1f, 5f)]
    private float m_SpeedMultiplier = 1f;

    public float m_FinalCameraOffsetZ;

    protected void Update()
    {
        UpdateCameraPosition();
        UpdateCameraRotation();
	}

    protected void OnValidate()
    {
        if (m_PositionTarget)
        {
            transform.position = m_PositionTarget.position + m_CameraOffset;
        }
    }

    private void UpdateCameraPosition()
    {
        float dot = Vector3.Dot(m_PositionTarget.forward.normalized, transform.forward.normalized);
        float multiplier = dot > m_DotAmount ? 1f : m_SpeedMultiplier;
        Vector3 finalPosition = m_PositionTarget.position + m_CameraOffset;
        finalPosition.z = m_CameraOffset.z;

        transform.position = Vector3.Lerp(transform.position, finalPosition, (m_PositionSpeed * multiplier) * Time.deltaTime);
    }

    private void UpdateCameraRotation()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(m_PositionTarget.position - transform.position), m_RotationSpeed * Time.deltaTime);
    }
}
