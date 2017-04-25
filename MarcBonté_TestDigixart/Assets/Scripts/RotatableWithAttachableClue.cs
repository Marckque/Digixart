using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatableWithAttachableClue : Rotatable
{
    [Header("Linked clue"), SerializeField]
    private Clue m_LinkedClue;
    private bool m_HasLinkedClue;

    public void AttachClueToRotator()
    {
        m_LinkedClue.transform.SetParent(transform);
        m_HasLinkedClue = true;
    }

    public void DetachClueFromRotator()
    {
        m_LinkedClue.transform.SetParent(transform.root);
        m_HasLinkedClue = false;
    }

    protected override void CheckStatus()
    {
        if (Mathf.Approximately(transform.eulerAngles.y, 180f) && m_HasLinkedClue)
        {
            StateIsNormal = true;
        }
        else
        {
            StateIsNormal = false;   
        }
    }
}