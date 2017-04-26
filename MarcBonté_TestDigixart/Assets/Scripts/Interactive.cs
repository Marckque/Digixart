using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    [Header("Interactive"), SerializeField]
    protected MeshRenderer m_InteractionGraphics;
    [SerializeField]
    protected GameObject[] m_ActivatesEntities;

    protected void Awake()
    {
        m_InteractionGraphics.enabled = false;
    }

    public virtual void PlayerInteracts()
    {
    }

    public void DisplayInteractionFeedback()
    {
        m_InteractionGraphics.enabled = true;
    }

    public void DeactivateInteraction()
    {
        HideInteractionFeedback();
    }

    public void HideInteractionFeedback()
    {
        m_InteractionGraphics.enabled = false;
    }

    protected void ActivateEntities()
    {
        if (m_ActivatesEntities.Length > 0)
        {
            foreach (GameObject go in m_ActivatesEntities)
            {
                go.SetActive(true);
            }
        }
    }

    protected void DeactivateEntities()
    {
        if (m_ActivatesEntities.Length > 0)
        {
            foreach (GameObject go in m_ActivatesEntities)
            {
                go.SetActive(false);
            }
        }
    }
}