using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ToastPanelProperties : UIPanelPropertiesInterface
{
    public string content;
    public float autoHideTime;
}

public class ToastPanel : UIPanelBase<ToastPanelProperties>
{
    public Text toastText;

    private ToastPanelProperties pro;

    protected override void SetProperties(ToastPanelProperties props)
    {
        base.SetProperties(props);
        pro = props;
        toastText.text = pro.content;
        StartCoroutine(AutoHide());

    }

 

    public IEnumerator AutoHide() {
        yield return new WaitForSeconds(pro.autoHideTime);
        UIFrame.Instance.HidePanelById<ToastPanel>();
    
    }
}
