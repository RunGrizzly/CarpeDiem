using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewIronButton : WorldButton
{
    public override void DoButtonAction()
    {
        eventManager.newIron.Invoke();
    }
}