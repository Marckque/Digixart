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

	protected void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + cameraOffset, speed * Time.deltaTime);
	}
}
