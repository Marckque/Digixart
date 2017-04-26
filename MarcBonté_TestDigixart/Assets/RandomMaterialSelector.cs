using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMaterialSelector : MonoBehaviour
{
    [SerializeField]
    private bool m_GiveNewMaterial;
    [SerializeField]
    private Material[] m_Materials;

    private void OnValidate()
    {
        if (m_GiveNewMaterial)
        {
            m_GiveNewMaterial = false;

            int randomMaterial = Random.Range(0, m_Materials.Length - 1);
            GetComponent<MeshRenderer>().material = m_Materials[randomMaterial];
        }
    }
}