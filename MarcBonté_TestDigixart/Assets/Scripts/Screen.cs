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

    public override void PlayerInteracts()
    {
        base.PlayerInteracts();
        UpdateScreenStatus();
    }

    private void UpdateScreenStatus()
    {
        m_ScreenIsOn = !m_ScreenIsOn;

        m_LinkedCamera.enabled = m_ScreenIsOn ? m_TurnedOnScreen : m_TurnedOffScreen;
        m_ScreenMeshRenderer.material = m_ScreenIsOn ? m_TurnedOnScreen : m_TurnedOffScreen;
    }
}