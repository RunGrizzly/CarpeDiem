using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewClothesButton : WorldButton
{
    public override void DoButtonAction()
    {
        eventManager.newClothes.Invoke();
    }
}