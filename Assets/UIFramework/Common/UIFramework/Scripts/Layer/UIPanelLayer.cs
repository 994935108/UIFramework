using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelLayer : UILayerBase<IPanelBaseInterface>
{

    /// <summary>
    /// …Ë÷√√Ê∞ÂµƒParent
    /// </summary>
    /// <param name="controller"></param>
    /// <param name="screenTransform"></param>
    internal override void SetScreenParent(UIWindowAndPanelBaseInterfaces controller, Transform screenTransform)
    {

        var ctl = controller as IPanelBaseInterface;
        if (ctl != null)
        {

            ctl.uiType.ToString();
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
        else
        {
            base.SetScreenParent(controller, screenTransform);
        }
    }
  

    internal override void ShowWidowOrPanel<TProps>(IPanelBaseInterface screen, TProps properties)
    {
        screen.Show(properties);
    }



    internal override void HideWindowOrPanel(IPanelBaseInterface screen)
    {
        screen.Hide();
    }

    internal override void ShowWindowOrPanel(IPanelBaseInterface screen)
    {
        screen.Show();
    }
}
