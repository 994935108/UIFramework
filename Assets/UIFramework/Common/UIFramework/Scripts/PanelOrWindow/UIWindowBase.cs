using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWindowBase: UIScenePanel<UIWindowProperties>, IWindowController 
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
}


public class UIWindowBase<Tprops> : UIScenePanel<Tprops>, IWindowController where Tprops : UIPropertiesInterface
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
}
