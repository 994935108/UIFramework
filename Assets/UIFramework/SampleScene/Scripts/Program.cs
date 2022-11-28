using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Program : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIFrame.Instance.ShowWindowOrPanelByType<StartGameWindow>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
