using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimationBase : MonoBehaviour, UIAnimationInterface
{
    public void ShowTransitionAnim(Action finish)
    {
        VirInTransition(finish);
    }

    public void HideTransitionAnim(Action finish)
    {
        VirOutTransition(finish);
    }

    public void Recover()
    {
        VirRecover();
    }

    public virtual void VirInTransition(Action finish) { 
    
    }

    public virtual void VirOutTransition(Action finish)
    {

    }

    public virtual void VirRecover( )
    {

    }

}
