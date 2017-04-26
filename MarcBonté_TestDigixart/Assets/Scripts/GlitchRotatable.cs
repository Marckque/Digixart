using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.ThirdPerson;

[SelectionBase]
public class GlitchRotatable : Rotatable
{
    [Header("Glitch"), SerializeField, Range(0, 4), Tooltip("0: 0°          1: 90°          2: 180°          3: 270°          4: 360°")]
    private int m_GlitchRotation;
    [SerializeField]
    private int m_GlitchIndex;

    [Header("Entity state"), SerializeField]
    private GameObject m_NormalState;
    [SerializeField]
    private GameObject m_GlitchState;

    protected override void Start()
    {
        base.Start();
        DeactivateSecondSetOfEntities();
    }

    public override void PlayerInteracts()
    {
        CheckStatus();
        base.PlayerInteracts();
        CheckStatus();
    }

    protected void CheckStatus()
    {
        if (Mathf.Approximately(nextRotation.y, m_Rotations[m_GlitchRotation]))
        {
            SwitchToGlitchState();
        }
        else
        {
            SwitchToNormalState();
        }
    }

    protected void SwitchToGlitchState()
    {
        m_NormalState.SetActive(false);
        m_GlitchState.SetActive(true);

        ActivateFirstSetOfEntities();
        DeactivateSecondSetOfEntities();
    }

    protected void SwitchToNormalState()
    {
        m_NormalState.SetActive(true);
        m_GlitchState.SetActive(false);

        DeactivateFirstSetOfEntities();
        ActivateSecondSetOfEntities();
    }
}