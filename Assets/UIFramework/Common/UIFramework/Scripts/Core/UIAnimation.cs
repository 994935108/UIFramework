using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface UIAnimation
{
    public void InTransition(Action finish);

    public void OutTransition(Action finish);



    public void Recover();

   
}
