using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Interactive : MonoBehaviour
{
    [Header("Interactive"), SerializeField]
    protected MeshRenderer m_InteractionGraphics;
    [SerializeField]
    protected GameObject[] m_NormalEntities;
    [SerializeField]
    protected GameObject[] m_GlitchEntities;

    protected void Awake()
    {
        m_InteractionGraphics.enabled = false;
    }

    public virtual void PlayerInteracts()
    {
    }

    // Visuals feedbacks for interaction
    public void DisplayInteractionFeedback()
    {
        m_InteractionGraphics.enabled = true;
    }

    public void HideInteractionFeedback()
    {
        m_InteractionGraphics.enabled = false;
    }

    // Activates/deactivates set of entities
    protected void ActivateNormalEntities()
    {
        if (m_NormalEntities.Length > 0)
        {
            foreach (GameObject go in m_NormalEntities)
            {
                go.SetActive(true);
            }
        }
    }

    protected void ActivateGlitchEntities()
    {
        if (m_GlitchEntities.Length > 0)
        {
            foreach (GameObject go in m_GlitchEntities)
            {
                go.SetActive(true);
            }
        }
    }

    protected void DeactivateNormalEntities()
    {
        if (m_NormalEntities.Length > 0)
        {
            foreach (GameObject go in m_NormalEntities)
            {
                Interactive i = go.GetComponentInChildren<Interactive>();
                if (i)
                {
                    i.DeactivateNormalEntities();
                    i.transform.rotation = Quaternion.identity;
                }

                go.SetActive(false);
            }
        }
    }

    protected void DeactivateGlitchEntities()
    {
        if (m_GlitchEntities.Length > 0)
        {
            foreach (GameObject go in m_GlitchEntities)
            {
                Interactive i = go.GetComponentInChildren<Interactive>();
                if (i)
                {
                    i.DeactivateGlitchEntities();
                    i.transform.rotation = Quaternion.identity;
                }

                go.SetActive(false);
            }
        }
    }

    protected void UseNormalEntities()
    {
        ActivateGlitchEntities();
        DeactivateNormalEntities();
    }

    protected void UseGlitchEntities()
    {
        DeactivateGlitchEntities();
        ActivateNormalEntities();
    }

    // Debug purposes
    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        if (m_GlitchEntities.Length > 0)
        {
            foreach (GameObject go in m_GlitchEntities)
            {
                
                Gizmos.DrawLine(transform.position, go.transform.position + Vector3.up * 0.25f);
            }
        }

        Gizmos.color = Color.white;
        if (m_NormalEntities.Length > 0)
        {
            foreach (GameObject go in m_NormalEntities)
            {
                
                Gizmos.DrawLine(transform.position, go.transform.position);
            }
        }
    }
}