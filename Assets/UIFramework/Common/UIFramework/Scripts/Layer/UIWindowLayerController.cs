using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct WindowHistoryEntry
{
    public readonly IWindowController Screen;
    public readonly UIWindowPropertiesInterface Properties;

    public WindowHistoryEntry(IWindowController screen, UIWindowPropertiesInterface properties)
    {
        Screen = screen;
        Properties = properties;
    }


}

public class UIWindowLayerController : UILayerBase<IWindowController>
{
    private Stack<IWindowController> openWindowStack;

    public IWindowController CurrentWindow { get; internal set; }


    internal override void Initialize()
    {
        base.Initialize();

        openWindowStack =new Stack<IWindowController>();
      
    }

   

    internal override void ShowWindowOrPanel<TProps>(IWindowController screen, TProps properties)
    {
        if (openWindowStack.Contains(screen))
        {
            while (openWindowStack.Count > 0)
            {
                IWindowController windowController = openWindowStack.Pop();

                if (windowController == screen)
                {

                    if (openWindowStack.Count > 0)
                    {
                        IWindowController pre = openWindowStack.Peek();
                        if (pre.IsHideOnOpenForegroundWindow && pre.IsVisible)
                        {
                            pre.Hide();
                        }

                    }
                    if (!windowController.IsVisible)
                    {
                        windowController.Show(properties);

                    }
                    break;

                }
                else
                {
                    if (windowController.IsVisible)
                    {
                        windowController.Hide();

                    }
                }

            }
        }
        else
        {

            if (CurrentWindow != null && CurrentWindow.IsHideOnOpenForegroundWindow) {
                CurrentWindow.Hide();
            }
            screen.Show(properties);
            openWindowStack.Push(screen);

        }
        CurrentWindow = screen;
    }
    internal override void ShowWindowOrPanel(IWindowController screen)
    {
        if (openWindowStack.Contains(screen))
        {
            while (openWindowStack.Count > 0)
            {
                IWindowController windowController = openWindowStack.Pop();

                if (windowController == screen)
                {

                    if (openWindowStack.Count > 0)
                    {
                        IWindowController pre = openWindowStack.Peek();
                        if (pre.IsHideOnOpenForegroundWindow && pre.IsVisible)
                        {
                            pre.Hide();
                        }

                    }
                    if (!windowController.IsVisible)
                    {
                        windowController.Show();

                    }
                    break;

                }
                else
                {
                    if (windowController.IsVisible)
                    {
                        windowController.Hide();

                    }
                }

            }
        }
        else {
            if (CurrentWindow!=null&&CurrentWindow.IsHideOnOpenForegroundWindow)
            {
                CurrentWindow.Hide();
            }
            screen.Show();
            openWindowStack.Push(screen);

        }
        CurrentWindow = screen;
    }


    internal override void HideWindowOrPanel(IWindowController screen)
    {
        MyDebugTool.Log("打开面板的数量"+openWindowStack.Count);
        if (openWindowStack.Count > 0)
        {
            if (openWindowStack.Peek() == screen&&screen.IsVisible)
            {
                openWindowStack.Pop().Hide();
                if (openWindowStack.Count > 0)
                {
                    IWindowController preWindow = openWindowStack.Peek();
                    if (preWindow.IsHideOnOpenForegroundWindow && !preWindow.IsVisible)
                    {
                        preWindow.Show();
                        openWindowStack.Pop();
                    }
                    else
                    {
                        MyDebugTool.LogError("当前面板是打开的无需再次打开");
                    }
                }
                else {
                    MyDebugTool.LogError("列表已经空了");
                }

               
            }
            else {
                MyDebugTool.LogError("当前面板不是顶层面板,不允许关闭");
            }
        }
        else {
            MyDebugTool.LogError("没有这个面板");
        }
    }




    internal override void HideAll(bool shouldAnimateWhenHiding = true)
    {
        while (openWindowStack.Count>0)
        {
            if (openWindowStack.Peek().IsVisible)
            {
                openWindowStack.Pop().Hide();

            }
            else {
                openWindowStack.Pop();
            }
        }

    }

    internal override void SetPanelOrWindowParent(UIControllerInterfaces controller, Transform screenTransform)
    {
        var ctl = controller as IWindowController;
        if (ctl != null)
        {

            switch (ctl.uilayer)
            {
                case UILayer.BackgroundLayer:
                    break;
                case UILayer.MiddleLayer:
                    break;
                case UILayer.Foreground:
                    break;
                case UILayer.Popup:
                    break;
                case UILayer.Toast:
                    break;
                default:
                    break;
            }
            Transform parent = transform.Find(ctl.uiType.ToString() + "/" + ctl.uilayer);

            screenTransform.SetParent(parent, false);

        }


    }




}
