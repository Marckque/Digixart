using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTeleporter : MonoBehaviour
{
    [SerializeField]
    private Transform m_ArrivalPoint;
    public Transform ArrivalPoint { get { return m_ArrivalPoint; } }
}