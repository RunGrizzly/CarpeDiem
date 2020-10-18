using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Fulfilment
{

    public Trait affectedTrait;
    public int fulfilAmount;

    public Fulfilment(Trait _affectedTrait, int _fulfilAmount)
    {

        affectedTrait = _affectedTrait;
        fulfilAmount = _fulfilAmount;

    }

}