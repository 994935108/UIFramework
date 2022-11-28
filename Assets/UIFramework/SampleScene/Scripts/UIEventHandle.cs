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



    }

    public void StartGameWindowEvet() {
        UIFrame.Instance.ShowWindowOrPanelByType<MainWindow>();
    }


    public void MainWindowEvent(Button bt) {

        switch (bt.name)
        {

            case "Back":

                UIFrame.Instance.HideCurrentWindow();
                break;

            case "Toast":
                UIFrame.Instance.ShowWindowOrPanelByType<ToastPanel, ToastPanelProperties>(new ToastPanelProperties {
                    autoHideTime = 0.5f,
                    content = "显示一个Toast提示框"
                });
                break;

                
            case "Popup":
                UIFrame.Instance.ShowWindowOrPanelByType<PopupPanel>();
                break;
            case "ForegroundWindow":
                UIFrame.Instance.ShowWindowOrPanelByType<ForegroundWindow>();
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

                UIFrame.Instance.ShowWindowOrPanelByType<ToastPanel, ToastPanelProperties>(new ToastPanelProperties
                {
                    autoHideTime = 0.5f,
                    content = "选择了Yes"
                });
                break;

            case "No":
                UIFrame.Instance.ShowWindowOrPanelByType<ToastPanel, ToastPanelProperties>(new ToastPanelProperties
                {
                    autoHideTime = 0.5f,
                    content = "选择了No"
                });
                break;


            default:
                break;
        }

        UIFrame.Instance.HidePanelById<PopupPanel>();

    }


    public void ForegroundWindowEvent(Button bt){
        switch (bt.name)
        {

            case "Back":
                UIFrame.Instance.HideCurrentWindow();
                break;

            case "StartGame":
                UIFrame.Instance.ShowWindowOrPanelByType<StartGameWindow>();
                break;


            default:
                break;
        }

    }
}
