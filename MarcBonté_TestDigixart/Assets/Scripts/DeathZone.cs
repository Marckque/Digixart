using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : Interactive
{
    public override void PlayerInteracts()
    {
        base.PlayerInteracts();
        SceneManager.LoadScene(0);
    }
}