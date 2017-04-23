using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    public MeshRenderer m_InteractionGraphics;

    public virtual void ActivateInteraction()
    {
        
    }

    public void DisplayInteraction()
    {
        m_InteractionGraphics.enabled = true;
    }

    public void DeactivateInteraction()
    {
        HideInteraction();
    }

    public void HideInteraction()
    {
        m_InteractionGraphics.enabled = false;
    }
}