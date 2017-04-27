using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.ThirdPerson;

[SelectionBase]
public class GlitchRotatable : Rotatable
{
    [Header("Glitch"), SerializeField, Range(0, 4), Tooltip("0: 0°          1: 90°          2: 180°          3: 270°          4: 360°")]
    private int m_GlitchRotation;

    [Header("Entity state"), SerializeField]
    private GameObject m_NormalState;
    [SerializeField]
    private GameObject m_GlitchState;

    protected override void Start()
    {
        base.Start();
        CheckStatus();
    }

    protected void OnEnable()
    {
        if (Time.time > 0.5f) CheckStatus();
    }


    public override void PlayerInteracts()
    {
        base.PlayerInteracts();
        CheckStatus();
    }

    protected void CheckStatus()
    {
        if (Mathf.Approximately(nextRotation.y, m_Rotations[m_GlitchRotation]))
        {
            SwitchThisEntityToGlitchState();
            UseGlitchEntities();
        }
        else
        {
            SwitchThisEntityToNormalState();
            UseNormalEntities();
        }
    }

    protected void SwitchThisEntityToGlitchState()
    {
        m_NormalState.SetActive(false);
        m_GlitchState.SetActive(true);
    }

    protected void SwitchThisEntityToNormalState()
    {
        m_NormalState.SetActive(true);
        m_GlitchState.SetActive(false);
    }
}