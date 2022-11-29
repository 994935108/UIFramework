using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftPanelEvent : UIEventBase<string> { 

}

public class LeftPanel : UIPanelBase
{
    public Button fold;

    protected override void VirInit()
    {
        fold.onClick.AddListener(()=> {

            UIEventCenter.Get<LeftPanelEvent>().Dispatch("Fold");
        });
    }

}
