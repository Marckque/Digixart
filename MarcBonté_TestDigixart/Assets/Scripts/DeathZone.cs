using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    protected void OnTriggerEnter(Collider other)
    {
        Character character = other.GetComponent<Character>();

        if (character)
        {
            SceneManager.LoadScene(0);
        }
    }
}