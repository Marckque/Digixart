using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
        /*
        if (m_DeactivateOnStart.Length > 0)
        {
            foreach (GameObject go in m_DeactivateOnStart)
            {
                go.SetActive(false);
            }
        }
        */
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

    protected void ActivateFirstSetOfEntities()
    {
        if (m_FirstSetOfEntities.Length > 0)
        {
            foreach (GameObject go in m_FirstSetOfEntities)
            {
                go.SetActive(true);
            }
        }
    }

    protected void ActivateSecondSetOfEntities()
    {
        if (m_SecondSetOfEntities.Length > 0)
        {
            foreach (GameObject go in m_SecondSetOfEntities)
            {
                go.SetActive(true);
            }
        }
    }

    protected void DeactivateFirstSetOfEntities()
    {
        if (m_FirstSetOfEntities.Length > 0)
        {
            foreach (GameObject go in m_FirstSetOfEntities)
            {
                go.SetActive(false);
            }
        }
    }

    protected void DeactivateSecondSetOfEntities()
    {
        if (m_SecondSetOfEntities.Length > 0)
        {
            foreach (GameObject go in m_SecondSetOfEntities)
            {
                go.SetActive(false);
            }
        }
    }

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