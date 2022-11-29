using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEventHandle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIEventCenter.Get<StartGameWindowEvet>().AddListener(StartGameWindowEvet);

        UIEventCenter.Get<MainWindowEvent>().AddListener(MainWindowEvent);


        UIEventCenter.Get<PopupPanelEvent>().AddListener(PopupPanelEvent);

        UIEventCenter.Get<ForegroundWindowEvent>().AddListener(ForegroundWindowEvent);


        UIEventCenter.Get<LeftPanelEvent>().AddListener(LeftPanelEvent);




    }

    public void StartGameWindowEvet() {
        UIManager.Instance.ShowWindowOrPanelByType<MainWindow>();
    }


    public void MainWindowEvent(Button bt) {

        switch (bt.name)
        {

            case "Back":

                UIManager.Instance.HideCurrentWindow();
                break;

            case "Toast":
                UIManager.Instance.ShowWindowOrPanelByType<ToastPanel, ToastPanelProperties>(new ToastPanelProperties {
                    autoHideTime = 0.5f,
                    content = "显示一个Toast提示框"
                });
                break;

                
            case "Popup":
                UIManager.Instance.ShowWindowOrPanelByType<PopupPanel>();
                break;
            case "ForegroundWindow":
                UIManager.Instance.ShowWindowOrPanelByType<ForegroundWindow>();
                break;

            case "LeftPanel":
                UIManager.Instance.ShowWindowOrPanelByType<LeftPanel>();
                break;
            default:
                break;
        }

    }

    public void PopupPanelEvent(Button bt)
    {

        switch (bt.name)
        {

            case "Yes":

                UIManager.Instance.ShowWindowOrPanelByType<ToastPanel, ToastPanelProperties>(new ToastPanelProperties
                {
                    autoHideTime = 0.5f,
                    content = "选择了Yes"
                });
                break;

            case "No":
                UIManager.Instance.ShowWindowOrPanelByType<ToastPanel, ToastPanelProperties>(new ToastPanelProperties
                {
                    autoHideTime = 0.5f,
                    content = "选择了No"
                });
                break;


            default:
                break;
        }

        UIManager.Instance.HidePanelById<PopupPanel>();

    }


    public void ForegroundWindowEvent(Button bt){
        switch (bt.name)
        {

            case "Back":
                UIManager.Instance.HideCurrentWindow();
                break;

            case "StartGame":
                UIManager.Instance.ShowWindowOrPanelByType<StartGameWindow>();
                break;


            default:
                break;
        }

    }

    public void LeftPanelEvent(string msg) {

        if (msg=="Fold") {

            UIManager.Instance.HidePanelById<LeftPanel>();
        
        }
    
    }
}
