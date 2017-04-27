using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Clue : Interactive
{
    [Header("UI"), SerializeField]
    private GameObject UI;

    private bool m_CurrentMode = true;
    private bool m_Interacting;

    public override void PlayerInteracts()
    {
        base.PlayerInteracts();

        m_Interacting = true;
        m_CurrentMode = !m_CurrentMode;
        CheckStatus();
    }

    protected override void CheckStatus()
    {
        base.CheckStatus();

        if (m_CurrentMode)
        {
            UseNormalEntities();            
        }
        else
        {
            UseGlitchEntities();
            
        }

        if (m_Interacting)
        {
            StartCoroutine(ActivateUI());
        }
        else
        {
            DeactivateUI();
        }

        m_Interacting = false;
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