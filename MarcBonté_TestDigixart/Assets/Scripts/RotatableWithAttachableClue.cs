using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatableWithAttachableClue : Rotatable
{
    [Header("Linked clue"), SerializeField]
    private Transform m_LinkedClue;

    public void AttachClueToRotator()
    {
        m_LinkedClue.transform.SetParent(transform);
    }

    public void DetachClueFromRotator()
    {
        if (!IsLinkedToDeathZone)
        {
            m_LinkedClue.transform.SetParent(transform.root);
        }
    }
}