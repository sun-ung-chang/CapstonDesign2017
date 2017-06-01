using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Ship : GroundCtr
{
    public override void Act(PlayerCtr player, Action _End)
    {
        PlayerCtr Enemy = GameManager.FindPlayer(player.Next_Player_Id);

        Enemy.GetDamaged(200);
        Enemy.SetUI(false);
        _End();
    }
}
