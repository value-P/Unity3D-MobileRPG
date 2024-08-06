using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBtn : ButtonBase
{
    public override void ButtonDown()
    {
        Managers.Input.onAttack = true;
    }

    public override void ButtonUp()
    {
        Managers.Input.onAttack = false;
    }
}
