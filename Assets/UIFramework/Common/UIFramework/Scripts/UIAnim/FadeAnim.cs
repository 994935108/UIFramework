using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class FadeAnim : UIAnimationBase
{
    public CanvasGroup canvasGroup;

    private Tween InTween;
    private Tween OutTween;

    private void Awake()
    {
        if (canvasGroup==null) {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }


    public override void VirInTransition(Action finish)
    {
        if (canvasGroup==null) {
            return;
        }
        if (InTween!=null) {
            InTween.Kill();
        }

        canvasGroup.alpha = 0;

        
        InTween = canvasGroup.DOFade(1,0.5f).SetEase(Ease.Linear).OnComplete(()=> {
           
            finish.Invoke();

        });
        
    }

    public override void VirOutTransition(Action finish)
    {
        if (canvasGroup == null)
        {
            return;
        }
        if (OutTween != null)
        {
            OutTween.Kill();
        }

        canvasGroup.alpha = 1;
       
        OutTween = canvasGroup.DOFade(0, 0.5f).SetEase(Ease.Linear).OnComplete(() => {
           
            finish.Invoke();
        });
    }

    public override void VirRecover()
    {
        if (canvasGroup == null)
        {
            return;
        }
        if (OutTween != null)
        {
            OutTween.Kill();
        }

        if (InTween != null)
        {
            InTween.Kill();
        }
        canvasGroup.alpha = 1;
       
    }

}
