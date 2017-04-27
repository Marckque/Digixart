using UnityEngine;
using System.Collections.Generic;

public class Character : MonoBehaviour
{
    private List<Interactive> m_Interactives = new List<Interactive>();
    public Interactive CurrentInteractive { get; private set; }

    [SerializeField, Range(0f, 3f)]
    private float m_ForwardMultiplier = 1.25f;
    private bool m_HasReleasedInteractButton = true;

    [SerializeField]
    private AudioClip[] m_InteractionAudioClips;
    private AudioSource m_AudioSource;
    private int m_InteractionSoundIndex;

    protected void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    protected void Update()
    {
        GetClosestInteractiveAmongInteractives();

        if (m_HasReleasedInteractButton && (Input.GetAxisRaw("A_1") > 0f) || Input.GetKeyDown(KeyCode.A))
        {
            m_HasReleasedInteractButton = false;
            Interact();
        }

        if (Input.GetAxisRaw("A_1") == 0)
        {
            m_HasReleasedInteractButton = true;
        }
    }

    // Manage interactions with interactive objects
    private void Interact()
    {
        if (CurrentInteractive)
        {
            SoundManagement();
            CurrentInteractive.PlayerInteracts();
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

    // Sound
    private void SoundManagement()
    {
        if (m_InteractionSoundIndex < m_InteractionAudioClips.Length - 1)
        {
            m_InteractionSoundIndex++;
        }
        else
        {
            m_InteractionSoundIndex = 0;
        }

        m_AudioSource.clip = m_InteractionAudioClips[m_InteractionSoundIndex];

        // As I only made one sound for it, I change the pitch. But, it would be nicer to have proper clips. Else, the array is pretty useless.
        int multiplier = m_InteractionSoundIndex + 1;
        m_AudioSource.pitch = multiplier * 0.33f; 

        if (!m_AudioSource.isPlaying)
        {
            m_AudioSource.Play();
        }
    }

    // Debug purposes
    private void OnDrawGizmos()
    {
        if (CurrentInteractive)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, CurrentInteractive.transform.position);
        }
    }
}