using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

   

    public UIAnimationBase inAnim;
    public UIAnimationBase outAnim;


    private bool isVisible;


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
        if (outAnim != null)
        {
            outAnim.OutTransition(() =>
            {
                VirOutTransitionFinishedEvent();

                VirHide();
                isVisible = false;
                gameObject.SetActive(false);
            });
        }
        else {
            VirHide();

            isVisible = false;
            gameObject.SetActive(false);
        }
      

    }
    public void Show(UIPropertiesInterface props = null)
    {
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

        if (inAnim != null)
        {
            inAnim.InTransition(()=> {
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

   
}
