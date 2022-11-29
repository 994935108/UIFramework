using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainWindowEvent : UIEventBase<Button> { 

}
public class MainWindow : UIWindowBase
{
    public Button back;

    public Button toast;

    public Button popup;
    public Button ForegroundWindow;

    public Button LeftPanel;





    protected override void VirInit()
    {
        base.VirInit();

        back = transform.Find("Back").GetComponent<Button>();
        toast = transform.Find("Toast").GetComponent<Button>();

        popup = transform.Find("Popup").GetComponent<Button>();

        ForegroundWindow= transform.Find("ForegroundWindow").GetComponent<Button>();

        LeftPanel = transform.Find("LeftPanel").GetComponent<Button>();
        back.onClick.AddListener(() =>
        {

            UIEventCenter.Get<MainWindowEvent>().Dispatch(back);

        });

        toast.onClick.AddListener(() =>
        {

            UIEventCenter.Get<MainWindowEvent>().Dispatch(toast);

        });

        popup.onClick.AddListener(() =>
        {

            UIEventCenter.Get<MainWindowEvent>().Dispatch(popup);

        });

        ForegroundWindow.onClick.AddListener(() =>
        {

            UIEventCenter.Get<MainWindowEvent>().Dispatch(ForegroundWindow);

        });

        LeftPanel.onClick.AddListener(() =>
        {
            UIEventCenter.Get<MainWindowEvent>().Dispatch(LeftPanel);


        });
    }


}
