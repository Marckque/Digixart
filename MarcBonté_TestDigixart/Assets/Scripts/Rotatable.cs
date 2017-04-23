using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.ThirdPerson;

[SelectionBase]
public class Rotatable : Interactive
{
    private const float ROTATION_LERP_OFFSET = 1f;

    [SerializeField, Range(0f, 40f)]
    private float m_RotationSpeed = 10f;
    [SerializeField, Range(0, 4), Tooltip("0: 0°          1: 90°          2: 180°          3: 270°          4: 360°")]
    private int rotationAmount;
    private float[] rotations = new float[5];
    private Vector3 nextRotation;
    public bool IsRotating { get; private set; }
    public GameObject isSelected;

    protected void Start()
    {
        InitialiseRotations();
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

    public override void ActivateInteraction()
    {
        base.ActivateInteraction();

        if (!IsRotating)
        {
            InitialiseRotation();

        }
    }

    private IEnumerator RotateSelf()
    {
        HideInteraction();

        while (IsRotating)
        {
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, nextRotation, m_RotationSpeed * Time.deltaTime);

            if (Mathf.Abs(nextRotation.y - transform.eulerAngles.y) < ROTATION_LERP_OFFSET)
            {
                IsRotating = false;
                transform.eulerAngles = nextRotation;
                DisplayInteraction();

                yield break;
            }

            yield return new WaitForEndOfFrame();
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        Character character = other.GetComponent<Character>();
        
        if (character)
        {
            character.AddInteractive(this);
        } 
    }

    protected void OnTriggerExit(Collider other)
    {
        Character character = other.GetComponent<Character>();

        if (character)
        {
            character.RemoveInteractive(this);
        }
    }
}