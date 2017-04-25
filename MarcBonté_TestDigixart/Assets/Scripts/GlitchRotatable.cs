using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.ThirdPerson;

[SelectionBase]
public class GlitchRotatable : Rotatable
{
    [Header("Glitch"), SerializeField, Range(0, 4), Tooltip("0: 0°          1: 90°          2: 180°          3: 270°          4: 360°")]
    private int brokenRotationAmount;
    [SerializeField]
    private GameObject m_ActivatesEntity;

    [Header("Entity state"), SerializeField]
    private GameObject m_NormalState;
    [SerializeField]
    private GameObject m_GlitchState;

    protected override void Start()
    {
        base.Start();

        if (m_ActivatesEntity)
        {
            m_ActivatesEntity.SetActive(false);
        }
    }

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
            SwitchToGlitchState();
        }
        else
        {
            SwitchToNormalState();
        }
    }

    private void SwitchToGlitchState()
    {
        m_NormalState.SetActive(false);
        m_GlitchState.SetActive(true);

        if (m_ActivatesEntity)
        {
            m_ActivatesEntity.SetActive(true);
        }        
    }

    private void SwitchToNormalState()
    {
        m_NormalState.SetActive(true);
        m_GlitchState.SetActive(false);

        if (m_ActivatesEntity)
        {
            m_ActivatesEntity.SetActive(false);
        }
    }
}