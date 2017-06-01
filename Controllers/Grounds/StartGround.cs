using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class StartGround : GroundCtr
{
    public override void Act(PlayerCtr player, Action _End)
    {
        _End();
    }
}
