using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Task
{
    //Keep a list of fulfilments that the current task has amassed.
    public List<Fulfilment> fulfilments = new List<Fulfilment>();
}