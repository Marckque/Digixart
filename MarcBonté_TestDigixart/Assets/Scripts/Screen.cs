using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen : Interactive
{
    [Header("Screen"), SerializeField]
    private MeshRenderer m_ScreenMeshRenderer;
    [SerializeField]
    private Material m_TurnedOnScreen;
    [SerializeField]
    private Material m_TurnedOffScreen;
    private bool m_ScreenIsOn;

    [Header("Camera"), SerializeField]
    private Camera m_LinkedCamera;

    [Header("Linked rotatable"), SerializeField]
    private RotatableWithAttachableClue m_LinkedRotatableWithAttachableClue;

    public override void PlayerInteracts()
    {
        base.PlayerInteracts();
        UpdateScreenStatus();
        
        if (m_ScreenIsOn)
        {
            m_LinkedRotatableWithAttachableClue.AttachClueToRotator();
        }         
        else
        {
            m_LinkedRotatableWithAttachableClue.DetachClueFromRotator();
        }
    }

    private void UpdateScreenStatus()
    {
        m_ScreenIsOn = !m_ScreenIsOn;

        m_LinkedCamera.enabled = m_ScreenIsOn ? m_TurnedOnScreen : m_TurnedOffScreen;
        m_ScreenMeshRenderer.material = m_ScreenIsOn ? m_TurnedOnScreen : m_TurnedOffScreen;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, m_LinkedRotatableWithAttachableClue.transform.position);
    }
}