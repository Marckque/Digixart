using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.ThirdPerson;

[SelectionBase]
public class GlitchRotatable : Rotatable
{
    [Header("Glitch"), SerializeField, Range(0, 4), Tooltip("0: 0°          1: 90°          2: 180°          3: 270°          4: 360°")]
    private int brokenRotationAmount;

    [Header("Entity state"), SerializeField]
    private GameObject m_NormalState;
    [SerializeField]
    private GameObject m_GlitchState;

    public bool StateIsNormal { get; set; }

    public override void PlayerInteracts()
    {
        CheckStatus();
        base.PlayerInteracts();
        CheckStatus();
    }

    protected void CheckStatus()
    {
        if (Mathf.Approximately(nextRotation.y, rotations[brokenRotationAmount]))
        {
            StateIsNormal = true;
        }
        else
        {
            StateIsNormal = false;
        }

        if (StateIsNormal)
        {
            m_NormalState.SetActive(false);
            m_GlitchState.SetActive(true);
        }
        else
        {
            m_NormalState.SetActive(true);
            m_GlitchState.SetActive(false);
        }
    }
}