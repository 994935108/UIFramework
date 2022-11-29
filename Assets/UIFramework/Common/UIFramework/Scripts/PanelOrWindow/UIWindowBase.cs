using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWindowBase : UIWindowAndPanelBasePanel<UIWindowProperties>, IWindowBaseInterface
{
    [Tooltip("��ǰ�洰�ڵ�ʱ���Ƿ�����")]
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
        [Tooltip("��ǰ�洰�ڵ�ʱ���Ƿ�����")]
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
    
