using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TranslationAnim : UIAnimationBase
{
    public Vector3 startLocalPosition;

    public Vector3 endLocalPosition;

    private Tween InAnimTween;
    private Tween OutAnimTween;




    public override void VirInTransition(Action finish)
    {
        if (InAnimTween!=null) {
            InAnimTween.Kill();
        }

        transform.localPosition = startLocalPosition;
        InAnimTween = transform.DOLocalMove(endLocalPosition, 0.5f);
    }

    public override void VirOutTransition(Action finish)
    {
        if (OutAnimTween != null)
        {
            OutAnimTween.Kill();

        }

        transform.localPosition = endLocalPosition;
        InAnimTween = transform.DOLocalMove(startLocalPosition, 0.5f);
    }

    public override void VirRecover()
    {
        if (InAnimTween != null)
        {
            InAnimTween.Kill();
        }

        if (OutAnimTween!=null) {
            OutAnimTween.Kill();

        }
    }

    
}
