using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameWindowEvet : UIEventBase { 

}

public class StartGameWindowPro : UIWindowPropertiesInterface { 

}

public class StartGameWindow : UIWindowBase
{

    public Button startGame;

    protected override void VirInit()
    {
        startGame.onClick.AddListener(StartGame);
    }


    private void StartGame() {

        UIEventCenter.Get<StartGameWindowEvet>().Dispatch();
    }
}
