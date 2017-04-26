using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Clue : Interactive
{
    [SerializeField]
    private GameObject UI;

    private bool m_CurrentMode = true;

    protected void Start()
    {
        CheckSet();
    }

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
            FirstSet();
            DeactivateUI();
        }
        else
        {
            SecondSet();
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

    private void FirstSet()
    {
        ActivateFirstSetOfEntities();
        DeactivateSecondSetOfEntities();
    }

    private void SecondSet()
    {
        DeactivateFirstSetOfEntities();
        ActivateSecondSetOfEntities();
    }
}