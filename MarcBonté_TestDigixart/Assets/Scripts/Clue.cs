using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Clue : Interactive
{
    public override void PlayerInteracts()
    {
        base.PlayerInteracts();
        print("You got a clue my friend.");
    }
}