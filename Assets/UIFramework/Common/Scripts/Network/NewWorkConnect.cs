using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class NewWorkConnect : MonoBehaviour
{
   
    public bool IsConnect
    {

        get
        {
            return isConnect;
        }
    }
    private bool isConnect;
    private float time;
    private void Update()
    {
        time += Time.deltaTime;
        if (time>=5) {
            time = 0;
            StartCoroutine(CheckConnect());
        }
    }
    

    private IEnumerator CheckConnect() {
        UnityWebRequest unityWebRequest= UnityWebRequest.Get("https://www.baidu.com");

        yield return unityWebRequest;
        isConnect=unityWebRequest.result== UnityWebRequest.Result.Success;
        
    }

    
}
