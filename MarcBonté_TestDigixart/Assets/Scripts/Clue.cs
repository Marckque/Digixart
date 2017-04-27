﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Clue : Interactive
{
    [SerializeField]
    private GameObject UI;

    private bool m_CurrentMode = true;

    public override void PlayerInteracts()
    {
        base.PlayerInteracts();

        m_CurrentMode = !m_CurrentMode;
        CheckSet();
    }

    private void CheckSet()
    {
        if (m_CurrentMode)
        {
            UseNormal();
            DeactivateUI();
        }
        else
        {
            UseGlitch();
            StartCoroutine(ActivateUI());
        }
    }

    private IEnumerator ActivateUI()
    {
        if (UI && Time.time > 0.5f)
        {
            UI.SetActive(true);
            yield return new WaitForSeconds(5f);
            UI.SetActive(false);
        }
        else
        {
            yield break;
        }
    }

    private void DeactivateUI()
    {
        if (UI)
        {
            StopAllCoroutines();
            UI.SetActive(false);
        }
    }
}