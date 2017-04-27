using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[SelectionBase]
public class Rotatable : Interactive
{
    #region Variables
    private const float ROTATION_LERP_OFFSET = 1f;

    [Header("Rotations"), SerializeField, Range(0f, 40f)]
    protected float m_RotationSpeed = 5f;
    [SerializeField, Range(0, 4), Tooltip("0: 0°          1: 90°          2: 180°          3: 270°          4: 360°")]
    protected int rotationAmount = 1;
    [SerializeField]
    protected bool m_RotateOnceOnStart;

    protected float[] m_Rotations = new float[5];
    protected Vector3 nextRotation;
    public bool IsRotating { get; set; }
    #endregion Variables

    // Start
    protected virtual void Start()
    {
        InitialiseRotationsArray();

        if (m_RotateOnceOnStart)
        {
            PrepareCurrentRotation();
        }
    }

    private void InitialiseRotationsArray()
    {
        for (int i = 0; i < m_Rotations.Length; ++i)
        {
            m_Rotations[i] = Mathf.Round(90f * i);
        }
    }

    // Override of parent function (required)
    public override void PlayerInteracts()
    {
        base.PlayerInteracts();

        if (!IsRotating)
        {
            PrepareCurrentRotation();
        }
    }

    // Rotate the rotatable
    private void PrepareCurrentRotation()
    {
        IsRotating = true;

        if (nextRotation.y >= 360f)
        {
            transform.eulerAngles = Vector3.zero;
            nextRotation.y = 0f;
        }

        nextRotation += new Vector3(0f, m_Rotations[rotationAmount], 0f);

        StartCoroutine(RotateSelf());
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