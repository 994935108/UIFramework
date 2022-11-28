using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DemoShow : MonoBehaviour
{

    public GameObject mObj;
    // Start is called before the first frame update
    void Start()
    {
        if(mObj != null)
        {
            //mObj.transform.DOLocalRotateQuaternion(mQ, 10f);
            mObj.transform.DORotate(new Vector3(0, 270, 0), 10f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).OnComplete(() =>
            {
                Debug.Log("localRotation anim complete!");
            });
            //mObj.transform.DOLocalMove(new Vector3(1,1,1),10f);
            

            DOTween.To
                (
                () => mObj.transform.localScale,
                (x) => mObj.transform.localScale = x,
                new Vector3(2, 2, 2),
                10f
                )
                .OnComplete
                (
                    () =>
                    {
                        Debug.Log("localScale anim complete!");
                    }
                )
                .SetEase(Ease.Linear);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LocalMovePosition(Vector3 targetPosition)
    {
        DOTween.To
                (
                () => mObj.transform.localPosition,
                (x) => mObj.transform.localPosition = x,
                new Vector3(1, 1, 1),
                10f
                )
                .OnComplete
                (
                    () =>
                    {
                        Debug.Log("localPosition anim complete!");
                    }
                )
                .SetEase(Ease.Linear);
    }
}
