using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIScenePanel<TProps> : MonoBehaviour, UIControllerInterfaces where TProps : UIPropertiesInterface
{
    public UIType UItype;
    public UILayer UILayer;


    public string ScreenId { get { return gameObject.name; } set { } }

    public UILayer uilayer { get { return SetUILayer(); } }
    public UIType uiType { get { return SetUIType(); } }

    public virtual UILayer SetUILayer() {
        return UILayer;


    }
    public virtual UIType SetUIType()
    {
        return UItype;


    }

    public bool IsVisible { get {
            return isVisible;
        }  }

    public bool HasInAnim
    {
        get
        {
            return inAnim!=null;
        }
    }

    public bool HasOutAnim
    {
        get
        {
            return outAnim!=null;
        }
    }

    Action InTransitionFinishCallback;
    Action OutTransitionFinishCallback;

    public UIAnimationBase inAnim;
    public UIAnimationBase outAnim;


    private  bool isVisible;

   protected bool isStopaAnim;

    protected void RecoverAnim() {

        if (inAnim!=null) { 
        
        inAnim.Recover();
        }

        if (outAnim!=null) { 
        outAnim.Recover();

        }

    }

    public void Init()
    {
        VirInit();
    }

   

    public virtual void  VirInTransitionFinishedEvent() { 
    
    }


    public virtual void VirOutTransitionFinishedEvent()
    {

    }

    public void Hide(bool anim = true)
    {
        if (outAnim != null&& !isStopaAnim&& anim)
        {
            outAnim.OutTransition(() =>
            {

                if (!isStopaAnim) {

                    OutTransitionFinishCallback.Invoke();
                    VirOutTransitionFinishedEvent();

                    VirHide();
                    isVisible = false;
                    gameObject.SetActive(false);

                    Debug.LogError("Òþ²ØÃæ°å" + this.name);
                }
            });
        }
        else {
            

            VirHide();

            isVisible = false;
            gameObject.SetActive(false);
            
        }
      

    }
    public void Show(UIPropertiesInterface props = null, bool anim = true)
    {
            isStopaAnim = false;
           isVisible = true;

           gameObject.SetActive(true);


        if (props != null)
        {
            if (props is TProps)
            {
                SetProperties((TProps)props);
            }
           
        }
        
        VirShow();
      

        if (inAnim != null && anim)
        {
            

            inAnim.InTransition(() =>
            {

                InTransitionFinishCallback.Invoke();
                VirInTransitionFinishedEvent();
            });
        }
        
    }

   
    public virtual void VirShow() { 
    
    }
    public virtual void VirHide()
    {

    }
   
    protected virtual void SetProperties(TProps props)
    {

    }
    protected virtual void VirInit()
    {

    }

    public void InTransitionFinish(Action finish)
    {
        InTransitionFinishCallback = finish;
    }

    public void OutTransitionFinish(Action finish)
    {
        OutTransitionFinishCallback = finish;


    }
}
