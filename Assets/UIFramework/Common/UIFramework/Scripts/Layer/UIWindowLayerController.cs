using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWindowLayerController : UILayerBase<IWindowController>
{
    private Stack<IWindowController> openWindowStack;

    public IWindowController CurrentWindow { get; internal set; }


    internal override void Initialize()
    {
        base.Initialize();

        openWindowStack = new Stack<IWindowController>();

    }



    internal override void ShowScreen<TProps>(IWindowController screen, TProps properties)
    {
        if (openWindowStack.Contains(screen))
        {
            HideAllTopWindow(screen);

            if (!screen.IsVisible)
            {
                screen.Show(properties);
            }
        }
        else
        {
            if (CurrentWindow != null && CurrentWindow.IsHideOnOpenForegroundWindow)
            {
                MyDebugTool.LogError("current==" + CurrentWindow.ScreenId);

                CurrentWindow.Hide();
                if (CurrentWindow.HasOutAnim)
                {
                    CurrentWindow.OutTransitionFinish(() => {
                        CurrentWindow.Show();
                    });
                }

            }
            else
            {
                screen.Show(properties);
            }
            openWindowStack.Push(screen);

        }
        CurrentWindow = screen;
    }
    internal override void ShowScreen(IWindowController screen)
    {

        if (openWindowStack.Contains(screen))
        {
            HideAllTopWindow(screen);

            if (!screen.IsVisible)
            {
                screen.Show();
            }
        }
        else
        {
            if (CurrentWindow != null && CurrentWindow.IsHideOnOpenForegroundWindow)
            {
                MyDebugTool.LogError("current==" + CurrentWindow.ScreenId);

                CurrentWindow.Hide();

                if (CurrentWindow.HasOutAnim) {
                    CurrentWindow.OutTransitionFinish(() => {
                        CurrentWindow.Show();
                    });
                }
                

            }
            else
            {
                screen.Show();
            }
            openWindowStack.Push(screen);

        }
        CurrentWindow = screen;
    }

    

   

    private void HideAllTopWindow(IWindowController screen)
    {
        while (openWindowStack.Count > 0)
        {
            IWindowController windowController = openWindowStack.Peek();
          


            if (windowController != screen)
            {
                if (windowController.IsVisible)
                {
                    //Debug.LogError("");
                    windowController.Hide(false);
                }
                openWindowStack.Pop();
            }
            else {
                break;
            }

        }

    }


    internal override void HideScreen(IWindowController screen)
    {
        MyDebugTool.Log("打开面板的数量" + openWindowStack.Count);

        if (openWindowStack.Contains(screen))
        {
            if (openWindowStack.Count > 0)
            {
                //判断当前打开的是否是顶层Window
                if (openWindowStack.Peek() == screen)
                {
                    IWindowController windowController = openWindowStack.Pop();
                    windowController.Hide();

                    if (windowController.HasOutAnim)
                    {

                        windowController.OutTransitionFinish(() =>
                        {
                            if (openWindowStack.Count > 0)
                            {
                                ///关闭当前窗口的时候判断前一个窗口的情况
                                IWindowController preWindow = openWindowStack.Peek();
                                preWindow.Redisplay();
                                CurrentWindow = preWindow;
                            }
                            else
                            {
                                MyDebugTool.LogError("列表已经空了");
                            }

                        });
                      

                    }
                    else {

                        if (openWindowStack.Count > 0)
                        {
                            ///关闭当前窗口的时候判断前一个窗口的情况
                            IWindowController preWindow = openWindowStack.Peek();
                            preWindow.Redisplay();
                            CurrentWindow = preWindow;
                        }
                        else
                        {
                            MyDebugTool.LogError("列表已经空了");
                        }
                    }

                }
                else
                {
                    MyDebugTool.LogError("当前面板不是顶层面板,不允许关闭");
                }
            }

        }
        else
        {
            MyDebugTool.LogError("没有这个面板");
        }

    }


    internal override void HideAll(bool shouldAnimateWhenHiding = true)
    {
        while (openWindowStack.Count > 0)
        {
            if (openWindowStack.Peek().IsVisible)
            {
                openWindowStack.Pop().Hide();

            }
            else
            {
                openWindowStack.Pop();
            }
        }

    }

    internal override void SetScreenParent(UIControllerInterfaces controller, Transform screenTransform)
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
