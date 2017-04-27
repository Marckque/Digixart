using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Variables
    [Header("Target"), SerializeField]
    private Transform m_PositionTarget;

    [Header("Settings"), SerializeField, Range(0f, 10f)]
    private float m_PositionSpeed = 1f;
    [SerializeField]
    private Vector3 m_CameraOffset;
    [SerializeField, Range(-1f, 1f)]
    private float m_DotAmount;
    [SerializeField, Range(1f, 5f)]
    private float m_SpeedMultiplier = 1f;
    #endregion Variables

    protected void Update()
    {
        UpdateCameraPosition();
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
        // Makes the camera go a little faster if the player looks (world) backwards
        float dot = Vector3.Dot(m_PositionTarget.forward.normalized, transform.forward.normalized);
        float multiplier = dot > m_DotAmount ? 1f : m_SpeedMultiplier;
        Vector3 finalPosition = m_PositionTarget.position + m_CameraOffset;
        finalPosition.z = m_CameraOffset.z;

        transform.position = Vector3.Lerp(transform.position, finalPosition, (m_PositionSpeed * multiplier) * Time.deltaTime);
    }
}
