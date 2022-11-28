using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UILayerBase<TScreen> : MonoBehaviour where TScreen : UIControllerInterfaces
{
    protected Dictionary<string, TScreen> registeredScreens;
    #region  �ṩ��������д�ķ���

    internal virtual void Initialize()
    {
        registeredScreens = new Dictionary<string, TScreen>();
    }
    /// <summary>
    /// ��ʾһ����Ļ����
    /// </summary>
    /// <param name="screen"></param>
    internal abstract void ShowScreen(TScreen screen);
    /// <summary>
    /// ��ʾһ���ɴ����������
    /// </summary>
    /// <typeparam name="TProps"></typeparam>
    /// <param name="screen"></param>
    /// <param name="properties"></param>
    internal abstract void ShowScreen<TProps>(TScreen screen, TProps properties) where TProps : UIPropertiesInterface;

    /// <summary>
    /// ����һ�����
    /// </summary>
    /// <param name="screen"></param>

    internal abstract void HideScreen(TScreen screen);
    #endregion



    #region  �ṩUIFrame���õĽӿ�

    internal void ShowScreenById(string screenId)
    {
        TScreen ctl;
        if (registeredScreens.TryGetValue(screenId, out ctl))
        {
            ShowScreen(ctl);
        }
        else
        {

            //���û�а���������һ��


            MyDebugTool.LogError("[AUILayerController] Screen ID " + screenId + " not registered to this layer!");
        }
    }

    internal void ShowWindowOrPanelById<T>(string screenId, UIPropertiesInterface properties)
    {
        TScreen ctl;
        if (registeredScreens.TryGetValue(screenId, out ctl))
        {
            ShowScreen(ctl, properties);
        }
        else
        {
            MyDebugTool.LogError("[AUILayerController] Screen ID " + screenId + " not registered!");
        }
    }

    internal void HideWindowOrPanelById(string screenId)
    {
        TScreen ctl;
        if (registeredScreens.TryGetValue(screenId, out ctl))
        {

            HideScreen(ctl);

        }
        else
        {
            MyDebugTool.LogError("[AUILayerController] Could not hide Screen ID " + screenId + " as it is not registered to this layer!");
        }
    }

    internal bool WindowOrPanelAlreadyRegistered(string screenId)
    {
        return registeredScreens.ContainsKey(screenId);
    }

    internal void RegisterWindowOrPanel(string screenId, TScreen controller)
    {
        if (!registeredScreens.ContainsKey(screenId))
        {
            MyDebugTool.Log("RegisterScreen:" + screenId);
            ProcessScreenRegister(screenId, controller);
        }
        else
        {
            MyDebugTool.LogError("[AUILayerController] Screen controller already registered for id: " + screenId);
        }
    }
    internal void UnregisterWindowOrPanel(string screenId, TScreen controller)
    {
        if (registeredScreens.ContainsKey(screenId))
        {
            ProcessScreenUnregister(screenId, controller);
        }
        else
        {
            MyDebugTool.LogError("[AUILayerController] Screen controller not registered for id: " + screenId);
        }
    }

    internal TScreen GetWindowOrPanelByScreenId(string screenId)
    {
        TScreen ctl;
        if (registeredScreens.TryGetValue(screenId, out ctl))
        {

            return ctl;

        }
        else
        {
            MyDebugTool.LogError("[AUILayerController] Could not hide Screen ID " + screenId + " as it is not registered to this layer!");
        }

        return default(TScreen);
    }


    #endregion





    #region  �鷽��

    internal virtual void SetScreenParent(UIControllerInterfaces controller, Transform screenTransform)
    {


    }
    internal virtual void HideAll(bool shouldAnimateWhenHiding = true)
    {
        foreach (var screen in registeredScreens)
        {
            screen.Value.Hide(shouldAnimateWhenHiding);
        }
    }

    protected virtual void ProcessScreenRegister(string screenId, TScreen controller)
    {
        controller.ScreenId = screenId;
        registeredScreens.Add(screenId, controller);
       
    }

    protected virtual void ProcessScreenUnregister(string screenId, TScreen controller)
    {
        registeredScreens.Remove(screenId);
    }

   
    #endregion

}
