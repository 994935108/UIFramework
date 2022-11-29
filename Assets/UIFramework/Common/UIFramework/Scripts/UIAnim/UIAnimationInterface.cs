using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface UIAnimationInterface
{
    public void ShowTransitionAnim(Action finish);


    public void HideTransitionAnim(Action finish);



    public void Recover();

   
}
