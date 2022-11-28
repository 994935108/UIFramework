using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimationBase : MonoBehaviour, UIAnimation
{
    public void InTransition(Action finish)
    {
        VirInTransition(finish);
    }

    public void OutTransition(Action finish)
    {
        VirOutTransition(finish);
    }

    public virtual void VirInTransition(Action finish) { 
    
    }

    public virtual void VirOutTransition(Action finish)
    {

    }
}