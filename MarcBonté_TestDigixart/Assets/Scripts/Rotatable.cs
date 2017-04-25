using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.ThirdPerson;

[SelectionBase]
public class Rotatable : Interactive
{
    private const float ROTATION_LERP_OFFSET = 1f;

    [Header("Rotations"), SerializeField, Range(0f, 40f)]
    protected float m_RotationSpeed = 5f;
    [SerializeField, Range(0, 4), Tooltip("0: 0°          1: 90°          2: 180°          3: 270°          4: 360°")]
    protected int rotationAmount = 1;
    protected float[] rotations = new float[5];
    protected Vector3 nextRotation;
    public bool IsRotating { get; set; }
    public bool m_RotateOnceOnStart;

    protected virtual void Start()
    {
        InitialiseRotations();

        if (m_RotateOnceOnStart)
        {
            InitialiseRotation();
        }
    }

    private void InitialiseRotations()
    {
        for (int i = 0; i < rotations.Length; ++i)
        {
            rotations[i] = Mathf.Round(90f * i);
        }
    }
    
    private void InitialiseRotation()
    {
        IsRotating = true;

        if (nextRotation.y >= 360f)
        {
            transform.eulerAngles = Vector3.zero;
            nextRotation.y = 0f;
        }

        nextRotation += new Vector3(0f, rotations[rotationAmount], 0f);
        
        StartCoroutine(RotateSelf());
    }

    public override void PlayerInteracts()
    {
        base.PlayerInteracts();

        if (!IsRotating)
        {
            InitialiseRotation();
        }
    }

    private IEnumerator RotateSelf()
    {
        HideInteractionFeedback();

        while (IsRotating)
        {
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, nextRotation, m_RotationSpeed * Time.deltaTime);

            if (Mathf.Abs(nextRotation.y - transform.eulerAngles.y) < ROTATION_LERP_OFFSET)
            {
                IsRotating = false;
                transform.eulerAngles = nextRotation;
                m_InteractionGraphics.transform.eulerAngles = transform.eulerAngles;

                yield break;
            }

            yield return new WaitForEndOfFrame();
        }
    }
}