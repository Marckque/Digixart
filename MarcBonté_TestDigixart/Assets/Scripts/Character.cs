using UnityEngine;
using System.Collections.Generic;

public class Character : MonoBehaviour
{
    private List<Interactive> m_Interactives = new List<Interactive>();
    public Interactive CurrentInteractive { get; private set; }
    protected void Update()
    {
        GetClosestInteractiveAmongInteractives();

        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    private void Interact()
    {
        if (CurrentInteractive)
        {
            CurrentInteractive.PlayerInteracts();
        }
    }

    public void AddInteractive(Interactive a_Interactive)
    {
        if (!m_Interactives.Contains(a_Interactive))
        {
            m_Interactives.Add(a_Interactive);
        }
    }

    public void RemoveInteractive(Interactive a_Interactive)
    {
        if (m_Interactives.Contains(a_Interactive))
        {
            if (a_Interactive == CurrentInteractive)
            {
                CurrentInteractive.HideInteractionFeedback();
                CurrentInteractive = null;
            }

            m_Interactives.Remove(a_Interactive);
        }
    }

    private void GetClosestInteractiveAmongInteractives()
    {
        if (m_Interactives.Count > 0)
        {
            float closestInteractive = float.MaxValue;

            foreach (Interactive interactive in m_Interactives)
            {
                interactive.HideInteractionFeedback();
                float distance = (interactive.transform.position - transform.position).sqrMagnitude;

                if (distance < closestInteractive)
                {
                    closestInteractive = distance;
                    CurrentInteractive = interactive;
                }
            }

            Rotatable r = CurrentInteractive as Rotatable;
            if (r)
            {
                if (!r.IsRotating)
                {
                    CurrentInteractive.DisplayInteractionFeedback();
                }
            }
            else
            {
                CurrentInteractive.DisplayInteractionFeedback();
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (CurrentInteractive)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, CurrentInteractive.transform.position);
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        Interactive interactive = other.GetComponent<Interactive>();

        if (interactive)
        {
            AddInteractive(interactive);
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        Interactive interactive = other.GetComponent<Interactive>();

        if (interactive)
        {
            RemoveInteractive(interactive);
        }
    }
}