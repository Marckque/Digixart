using UnityEngine;
using System.Collections.Generic;

public class Character : MonoBehaviour
{
    private List<Interactive> m_Interactives = new List<Interactive>();
    public Interactive CurrentInteractive { get; private set; }

    [SerializeField, Range(0f, 3f)]
    private float m_ForwardMultiplier = 1.25f;
    private bool m_HasReleasedInteractButton = true;

    protected void Update()
    {
        GetClosestInteractiveAmongInteractives();

        if (m_HasReleasedInteractButton && (Input.GetAxisRaw("A_1") > 0f) || Input.GetKeyDown(KeyCode.A))
        {
            m_HasReleasedInteractButton = false;
            Interact();
        }

        if (Input.GetAxisRaw("A_1") == 0)//(Mathf.Approximately(Input.GetAxisRaw("A_1"), 0f))
        {
            m_HasReleasedInteractButton = true;
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

                Vector3 newInteractiveTransformPosition = interactive.transform.position;
                newInteractiveTransformPosition.y = 0f;
                Vector3 newTransformPosition = transform.position;
                newTransformPosition.y = 0;

                float distance = ((newInteractiveTransformPosition - newTransformPosition) - (transform.forward * m_ForwardMultiplier)).sqrMagnitude;

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

        SimpleTeleporter teleporter = other.GetComponent<SimpleTeleporter>();
        if (teleporter)
        {
            transform.position = teleporter.ArrivalPoint.position;
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