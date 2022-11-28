using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForegroundWindowEvent: UIEventBase<Button>{ 

}
public class ForegroundWindow : UIWindowBase
{

    public Button back;

    public Button backStart;

    protected override void VirInit()
    {
        base.VirInit();

        back.onClick.AddListener(()=> {
            UIEventCenter.Get<ForegroundWindowEvent>().Dispatch(back);
        });

        backStart.onClick.AddListener(() => {
            UIEventCenter.Get<ForegroundWindowEvent>().Dispatch(backStart);
        });
    }

}
