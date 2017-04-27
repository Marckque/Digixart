using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Interactive : MonoBehaviour
{
    [Header("Interactive"), SerializeField]
    protected MeshRenderer m_InteractionGraphics;
    [SerializeField]
    protected GameObject[] m_FirstSetOfEntities;
    [SerializeField]
    protected GameObject[] m_SecondSetOfEntities;
    [SerializeField]
    protected GameObject[] m_DeactivateOnStart;

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
        if (m_FirstSetOfEntities.Length > 0)
        {
            foreach (GameObject go in m_FirstSetOfEntities)
            {
                go.SetActive(true);
            }
        }
    }

    protected void ActivateGlitchEntities()
    {
        if (m_SecondSetOfEntities.Length > 0)
        {
            foreach (GameObject go in m_SecondSetOfEntities)
            {
                go.SetActive(true);
            }
        }
    }

    protected void DeactivateNormalEntities()
    {
        if (m_FirstSetOfEntities.Length > 0)
        {
            foreach (GameObject go in m_FirstSetOfEntities)
            {
                Interactive i = go.GetComponentInChildren<Interactive>();
                if (i)
                {
                    i.DeactivateNormalEntities();
                }

                go.SetActive(false);
            }
        }
    }

    protected void DeactivateGlitchEntities()
    {
        if (m_SecondSetOfEntities.Length > 0)
        {
            foreach (GameObject go in m_SecondSetOfEntities)
            {
                Interactive i = go.GetComponentInChildren<Interactive>();
                if (i)
                {
                    i.DeactivateGlitchEntities();
                }

                go.SetActive(false);
            }
        }
    }

    protected void UseNormal()
    {
        ActivateNormalEntities();
        DeactivateGlitchEntities();
    }

    protected void UseGlitch()
    {
        DeactivateNormalEntities();
        ActivateGlitchEntities();
    }

    // Debug purposes
    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        if (m_FirstSetOfEntities.Length > 0)
        {
            foreach (GameObject go in m_FirstSetOfEntities)
            {
                
                Gizmos.DrawLine(transform.position, go.transform.position + Vector3.up * 0.25f);
            }
        }

        Gizmos.color = Color.white;
        if (m_SecondSetOfEntities.Length > 0)
        {
            foreach (GameObject go in m_SecondSetOfEntities)
            {
                
                Gizmos.DrawLine(transform.position, go.transform.position);
            }
        }

        Gizmos.color = Color.red;
        if (m_DeactivateOnStart.Length > 0)
        {
            foreach (GameObject go in m_DeactivateOnStart)
            {
                
                Gizmos.DrawLine(transform.position, go.transform.position - Vector3.up * 0.25f);
            }
        }
    }
}