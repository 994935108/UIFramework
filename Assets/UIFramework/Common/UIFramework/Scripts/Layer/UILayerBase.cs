using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UILayerBase<Twp> : MonoBehaviour where Twp : UIWindowAndPanelBaseInterfaces
{
    protected Dictionary<string, Twp> registeredScreens;
    #region  提供给子类重写的方法

    internal virtual void Initialize()
    {
        registeredScreens = new Dictionary<string, Twp>();
    }
    
    internal abstract void ShowWindowOrPanel(Twp windowOrPanel);
  
    internal abstract void ShowWidowOrPanel<TProps>(Twp screen, TProps properties) where TProps : UIPropertiesInterface;

    

    internal abstract void HideWindowOrPanel(Twp windowOrPanel);
    #endregion



    #region  提供UIManager调用的接口

    internal void ShowWindowOrPanelById(string windowOrPanelId)
    {
        Twp ctl;
        if (registeredScreens.TryGetValue(windowOrPanelId, out ctl))
        {
            ShowWindowOrPanel(ctl);
        }
        else
        {

            //如果没有包含，创建一个


            MyDebugTool.LogError(" Screen ID " + windowOrPanelId + " not registered to this layer!");
        }
    }

    internal void ShowWindowOrPanelById<T>(string windowOrPanelId, UIPropertiesInterface properties)
    {
        Twp ctl;
        if (registeredScreens.TryGetValue(windowOrPanelId, out ctl))
        {
            ShowWidowOrPanel(ctl, properties);
        }
        else
        {
            MyDebugTool.LogError("WindowOrPanel  " + windowOrPanelId + " not registered!");
        }
    }

    internal void HideWindowOrPanelById(string windowOrPanelId)
    {
        Twp ctl;
        if (registeredScreens.TryGetValue(windowOrPanelId, out ctl))
        {

            HideWindowOrPanel(ctl);

        }
        else
        {
            MyDebugTool.LogError(" Could not hide windowOrPanelId " + windowOrPanelId + " as it is not registered to this layer!");
        }
    }

    internal bool WindowOrPanelAlreadyRegistered(string windowOrPanelId)
    {
        return registeredScreens.ContainsKey(windowOrPanelId);
    }

    internal void RegisterWindowOrPanel(string windowOrPanelId, Twp controller)
    {
        if (!registeredScreens.ContainsKey(windowOrPanelId))
        {
            MyDebugTool.Log("RegisterScreen:" + windowOrPanelId);
            ProcessScreenRegister(windowOrPanelId, controller);
        }
        else
        {
            MyDebugTool.LogError("controller already registered for id: " + windowOrPanelId);
        }
    }
    internal void UnregisterWindowOrPanel(string windowOrPanelId, Twp controller)
    {
        if (registeredScreens.ContainsKey(windowOrPanelId))
        {
            ProcessScreenUnregister(windowOrPanelId, controller);
        }
        else
        {
            MyDebugTool.LogError("[AUILayerController] Screen controller not registered for id: " + windowOrPanelId);
        }
    }

    internal Twp GetWindowOrPanelByScreenId(string windowOrPanelId)
    {
        Twp ctl;
        if (registeredScreens.TryGetValue(windowOrPanelId, out ctl))
        {

            return ctl;

        }
        else
        {
            MyDebugTool.LogError(" Could not hide Screen ID " + windowOrPanelId + " as it is not registered to this layer!");
        }

        return default(Twp);
    }


    #endregion





    #region  虚方法

    internal virtual void SetScreenParent(UIWindowAndPanelBaseInterfaces controller, Transform screenTransform)
    {


    }
    internal virtual void HideAll(bool shouldAnimateWhenHiding = true)
    {
        foreach (var screen in registeredScreens)
        {
            screen.Value.Hide(shouldAnimateWhenHiding);
        }
    }

    protected virtual void ProcessScreenRegister(string screenId, Twp controller)
    {
        controller.ScreenId = screenId;
        registeredScreens.Add(screenId, controller);
       
    }

    protected virtual void ProcessScreenUnregister(string screenId, Twp controller)
    {
        registeredScreens.Remove(screenId);
    }

   
    #endregion

}
