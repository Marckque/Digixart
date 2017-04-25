using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.ThirdPerson;

[SelectionBase]
public class Rotatable : Interactive
{
    private const float ROTATION_LERP_OFFSET = 1f;

    [Header("Rotations"), SerializeField, Range(0f, 40f)]
    private float m_RotationSpeed = 5f;
    [SerializeField, Range(0, 4), Tooltip("0: 0°          1: 90°          2: 180°          3: 270°          4: 360°")]
    private int rotationAmount = 1;
    private float[] rotations = new float[5];
    private Vector3 nextRotation;
    public bool IsRotating { get; set; }
    public bool m_HasBeenRotatedOnce;

    [Header("Broken"), SerializeField]
    private bool m_CanBeBroken;
    public bool StateIsNormal { get; set; }
    [SerializeField, Range(0, 4), Tooltip("0: 0°          1: 90°          2: 180°          3: 270°          4: 360°")]
    private int brokenRotationAmount = 2;

    [Header("Boxes"), SerializeField]
    private GameObject m_NormalBox;
    [SerializeField]
    private GameObject m_BrokenBox;

    protected void Start()
    {
        InitialiseRotations();

        if (m_HasBeenRotatedOnce)
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

        if (m_CanBeBroken)
        {
            CheckStatus();
        }

        while (IsRotating)
        {
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, nextRotation, m_RotationSpeed * Time.deltaTime);

            if (Mathf.Abs(nextRotation.y - transform.eulerAngles.y) < ROTATION_LERP_OFFSET)
            {
                IsRotating = false;
                transform.eulerAngles = nextRotation;

                yield break;
            }

            yield return new WaitForEndOfFrame();
        }
    }

    protected virtual void CheckStatus()
    {
        if (Mathf.Approximately(nextRotation.y, 180f))
        {
            StateIsNormal = true;
        }
        else
        {
            StateIsNormal = false;
        }

        if (StateIsNormal)
        {
            m_NormalBox.SetActive(false);
            m_BrokenBox.SetActive(true);
        }
        else
        {
            m_NormalBox.SetActive(true);
            m_BrokenBox.SetActive(false);
        }
    }
}