using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RunBtn : ButtonBase
{
    public override void ButtonDown()
    {
        Managers.Input.isRun = true;
    }

    public override void ButtonUp()
    {
        Managers.Input.isRun = false;
    }


}
