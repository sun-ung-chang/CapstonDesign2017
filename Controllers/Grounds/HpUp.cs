using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpUp : GroundCtr
{

    public override void Act(PlayerCtr player, Action _End)
    {
        
        player.GetDamaged(- 100);
        player.SetUI(true);
        _End();
    }
}
