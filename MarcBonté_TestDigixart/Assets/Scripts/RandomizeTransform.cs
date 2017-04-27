using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeTransform : MonoBehaviour
{
    private const float MINIMUM_SCALE = 1.6f;
    private const float MAXIMUM_SCALE = 2.4f;

    [SerializeField]
    private bool m_RandomizeRotation;
    [SerializeField]
    private bool m_RandomizeScale;

    protected void OnValidate()
    {
        if (m_RandomizeRotation)
        {
            m_RandomizeRotation = false;

            Vector3 newRotation = Vector3.zero;
            newRotation.y = Random.Range(0f, 360f);

            transform.localEulerAngles = newRotation;
        }

        if (m_RandomizeScale)
        {
            m_RandomizeScale = false;

            float x = Random.Range(MINIMUM_SCALE, MAXIMUM_SCALE);
            float y = Random.Range(MINIMUM_SCALE, MAXIMUM_SCALE);
            float z = Random.Range(MINIMUM_SCALE, MAXIMUM_SCALE);

            transform.localScale = new Vector3(x, y, z);
        }
    }
}