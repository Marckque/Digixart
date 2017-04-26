using UnityEngine;
using System.Collections.Generic;

public class MainManager : MonoBehaviour
{
    private static MainManager m_Instance;
    public static MainManager Instance { get { return m_Instance; } }

    private List<Clue> m_Clues = new List<Clue>();

    private void Awake()
    {
        m_Instance = this;
    }

    public void AddClue(Clue clue)
    {
        if (!m_Clues.Contains(clue))
        {
            m_Clues.Add(clue);
        }
    }
}