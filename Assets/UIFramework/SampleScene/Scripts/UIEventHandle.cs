using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventHandle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIEventCenter.Get<StartGameWindowEvet>().AddListener(StartGameWindowEvet);
        
    }

    public void StartGameWindowEvet() {
        UIFrame.Instance.ShowWindowOrPanelByType<MainWindow>();
    }

   
}
