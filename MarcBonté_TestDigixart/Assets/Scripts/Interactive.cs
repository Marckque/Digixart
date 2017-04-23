using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    [Header("Interactive")]
    public MeshRenderer m_InteractionGraphics;

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
}