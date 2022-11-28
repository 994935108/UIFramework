using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupPanelEvent :UIEventBase<Button> { 

}

public class PopupPanel : UIPanelBase
{
    public Button Yes;

    public Button No;


    protected override void VirInit()
    {
        base.VirInit();

        Yes =transform.Find("Yes"). GetComponent<Button>();
        No = transform.Find("No").GetComponent<Button>();
        Yes.onClick.AddListener(() =>
        {

            UIEventCenter.Get<PopupPanelEvent>().Dispatch(Yes);

        });

        No.onClick.AddListener(() =>
        {

            UIEventCenter.Get<PopupPanelEvent >().Dispatch(No);

        });
    }
}
