using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWindowBase: UIScenePanel<UIWindowProperties>, IWindowController 
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
}


public class UIWindowBase<Tprops> : UIScenePanel<Tprops>, IWindowController where Tprops : UIPropertiesInterface
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
}
