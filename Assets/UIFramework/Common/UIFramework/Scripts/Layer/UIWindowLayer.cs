using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWindowLayer : UILayerBase<IWindowBaseInterface>
{
    private Stack<IWindowBaseInterface> openWindowStack;

    public IWindowBaseInterface CurrentWindow { get; internal set; }


    internal override void Initialize()
    {
        base.Initialize();

        openWindowStack = new Stack<IWindowBaseInterface>();

    }



    internal override void ShowWidowOrPanel<TProps>(IWindowBaseInterface screen, TProps properties)
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
                    CurrentWindow.ShowTransitionAnimFinish(() => {
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
    internal override void ShowWindowOrPanel(IWindowBaseInterface screen)
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
                    CurrentWindow.ShowTransitionAnimFinish(() => {
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

    

   

    private void HideAllTopWindow(IWindowBaseInterface screen)
    {
        while (openWindowStack.Count > 0)
        {
            IWindowBaseInterface windowController = openWindowStack.Peek();
          


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


    internal override void HideWindowOrPanel(IWindowBaseInterface screen)
    {
        MyDebugTool.Log("??????????????" + openWindowStack.Count);

        if (openWindowStack.Contains(screen))
        {
            if (openWindowStack.Count > 0)
            {
                //????????????????????????Window
                if (openWindowStack.Peek() == screen)
                {
                    IWindowBaseInterface windowController = openWindowStack.Pop();
                    windowController.Hide();

                    if (windowController.HasOutAnim)
                    {

                        windowController.ShowTransitionAnimFinish(() =>
                        {
                            if (openWindowStack.Count > 0)
                            {
                                ///??????????????????????????????????????
                                IWindowBaseInterface preWindow = openWindowStack.Peek();
                                preWindow.Redisplay();
                                CurrentWindow = preWindow;
                            }
                            else
                            {
                                MyDebugTool.LogError("????????????");
                            }

                        });
                      

                    }
                    else {

                        if (openWindowStack.Count > 0)
                        {
                            ///??????????????????????????????????????
                            IWindowBaseInterface preWindow = openWindowStack.Peek();
                            preWindow.Redisplay();
                            CurrentWindow = preWindow;
                        }
                        else
                        {
                            MyDebugTool.LogError("????????????");
                        }
                    }

                }
                else
                {
                    MyDebugTool.LogError("????????????????????,??????????");
                }
            }

        }
        else
        {
            MyDebugTool.LogError("????????????");
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

    internal override void SetScreenParent(UIWindowAndPanelBaseInterfaces controller, Transform screenTransform)
    {
        var ctl = controller as IWindowBaseInterface;
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
