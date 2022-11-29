using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWindowBase : UIWindowAndPanelBasePanel<UIWindowProperties>, IWindowBaseInterface
{
    [Tooltip("打开前面窗口的时候是否隐藏")]
    public bool isHideOnOpenForegroundWindow = true;


    public bool IsHideOnOpenForegroundWindow
    {
        get
        {
            return isHideOnOpenForegroundWindow;
        }
    }

   

    public void Redisplay(UIPropertiesInterface props = null)
    {
        if (!IsVisible)
        {
            Show();
        }
        Redisplay();
    }

    public virtual void Redisplay()
    {
        
    }
}


    public class UIWindowBase<Tprops> : UIWindowAndPanelBasePanel<Tprops>, IWindowBaseInterface where Tprops : UIPropertiesInterface
    {
        [Tooltip("打开前面窗口的时候是否隐藏")]
        public bool isHideOnOpenForegroundWindow = true;


        public bool IsHideOnOpenForegroundWindow
        {
            get
            {
                return isHideOnOpenForegroundWindow;
            }
        }

    public void Redisplay(UIPropertiesInterface props = null)
    {
       
        if (!IsVisible)
        {
            Show();
            
        }
        
        ReShow();
    }

    public virtual void ReShow()
    {

    }
}
    
