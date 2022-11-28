using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelLayerController : UILayerBase<IPanelController>
{

    /// <summary>
    /// …Ë÷√√Ê∞ÂµƒParent
    /// </summary>
    /// <param name="controller"></param>
    /// <param name="screenTransform"></param>
    internal override void SetPanelOrWindowParent(UIControllerInterfaces controller, Transform screenTransform)
    {

        var ctl = controller as IPanelController;
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
            base.SetPanelOrWindowParent(controller, screenTransform);
        }
    }
  

    internal override void ShowWindowOrPanel<TProps>(IPanelController screen, TProps properties)
    {
        screen.Show(properties);
    }



    internal override void HideWindowOrPanel(IPanelController screen)
    {
        screen.Hide();
    }

    internal override void ShowWindowOrPanel(IPanelController screen)
    {
        screen.Show();
    }
}
