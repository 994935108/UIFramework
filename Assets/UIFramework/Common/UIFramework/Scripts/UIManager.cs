using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonMonoBehaviour<UIManager>
{
    //public static UIFrame Instance;

   
   
    public UIPanelLayer panelLayer;
    public UIWindowLayer windowLayer;




    private Canvas canvas;
    public Canvas UICanvase {
        get {

            if (canvas==null) {
                canvas= GetComponent<Canvas>(); ;
            }
            return canvas;
        }
    }
    private void Awake()
    {

        //?ֱ???????
        CanvasScaler canvasScaler = GetComponent<CanvasScaler>();

        float referenceResolution= canvasScaler.referenceResolution.x / canvasScaler.referenceResolution.y;

        float screenResolution = (Screen.currentResolution.width * 1.0f) / (Screen.currentResolution.height);


        if (screenResolution > referenceResolution)
        {
            canvasScaler.matchWidthOrHeight = 0;
        }
        else { 
            canvasScaler.matchWidthOrHeight=1;

        }
        Initialized();
    }

  
    public void Initialized()
    {
        if (panelLayer == null)
        {
            panelLayer = gameObject.GetComponent<UIPanelLayer>();
            if (panelLayer == null)
            {
                MyDebugTool.LogError("[UI Frame] UI Frame lacks Panel Layer!");
            }
            else
            {
                panelLayer.Initialize();
            }
        }
        else {
            panelLayer.Initialize();
        }

        if (windowLayer == null)
        {
            windowLayer = gameObject.GetComponent<UIWindowLayer>();
            if (panelLayer == null)
            {
                MyDebugTool.LogError("[UI Frame] UI Frame lacks Window Layer!");
            }
            else
            {
                windowLayer.Initialize();

            }
        }
        else {
            windowLayer.Initialize();
        }

       

    }

   
   

    /// <summary>
    /// ??ʾ????
    /// </summary>
    /// <param name="screenId"></param>
    public void ShowWindowOrPanelByType<T>()where T:UIWindowAndPanelBaseInterfaces {
        string screenId = typeof(T).ToString();
        if (!WindowOrPanelAlreadyRegistered(screenId))
        {
            CreatePanelOrWindowByID(screenId);
        }

        if (typeof(IWindowBaseInterface).IsAssignableFrom(typeof(T)))
        {
            windowLayer.ShowWindowOrPanelById(screenId);
        }
        else if (typeof(IPanelBaseInterface).IsAssignableFrom(typeof(T)))
        {
            panelLayer.ShowWindowOrPanelById(screenId);

        }
        else
        {
            MyDebugTool.LogError("û???ҵ?UI??"+ screenId);
        }
    }

    /// <summary>
    /// ??ʾһ?????壬????????ID????????????ʾ????
    /// </summary>
    /// <typeparam name="TProps"></typeparam>
    /// <param name="screenId"></param>
    /// <param name="properties"></param>
    public void ShowWindowOrPanelByType<T,TProps>( TProps properties) where TProps : UIPropertiesInterface
    {
        string screenId = typeof(T).ToString();

        if (!WindowOrPanelAlreadyRegistered(screenId))
        {
            CreatePanelOrWindowByID(screenId);
        }
      
        if (typeof(IWindowBaseInterface).IsAssignableFrom(typeof(T)))
        {
            windowLayer.ShowWindowOrPanelById<TProps>(screenId, properties);
        }
        else if (typeof(IPanelBaseInterface).IsAssignableFrom(typeof(T)))
        {
            panelLayer.ShowWindowOrPanelById<TProps>(screenId, properties);

        }
        else
        {
            MyDebugTool.LogError("ɶҲ????");
        }

    }
    private void  CreatePanelOrWindowByID(string screenId) {
        GameObject UI = Instantiate(Resources.Load<GameObject>("UI/Panel/" + screenId));
        UI.gameObject.name = screenId;
        UI.SetActive(false);
        UIWindowAndPanelBaseInterfaces uIController = UI.GetComponent<UIWindowAndPanelBaseInterfaces>();
        uIController.Init();
        RegisterWindowOrPanelToLayer(screenId, UI.GetComponent<UIWindowAndPanelBaseInterfaces>(), UI.transform);

        MyDebugTool.LogError("????UI:" + screenId);

    }
    public T GetWindowOrPanel<T>()where T: UIWindowAndPanelBaseInterfaces
    {
        string screenId = typeof(T).ToString();

        if (typeof(IWindowBaseInterface).IsAssignableFrom(typeof(T)))
        {
          return   (T)windowLayer.GetWindowOrPanelByScreenId(screenId);
        }
        else if (typeof(IPanelBaseInterface).IsAssignableFrom(typeof(T)))
        {
            return (T)panelLayer.GetWindowOrPanelByScreenId(screenId);

        }
        else {
            MyDebugTool.LogError("??ǰ???ص?UI???廹û??ʼ????????ֱ?ӻ?ȡ?????????ƣ?"+ screenId);
        }
       
       

        return default;
    
    }

    public void HideCurrentWindow() {
        if (windowLayer.CurrentWindow!=null) {
            windowLayer.HideWindowOrPanel(windowLayer.CurrentWindow);
        }
    }

   
    public void HidePanelById<T>()where T:UIWindowAndPanelBaseInterfaces {
        string screenId = typeof(T).ToString();

        if (WindowOrPanelAlreadyRegistered(screenId))
        {
            if (typeof(IWindowBaseInterface).IsAssignableFrom(typeof(T)))
            {
                // windowLayer.HideScreenById(screenId);

                Debug.LogError("??ǰ?رյĴ???ΪWindow???ͣ?????ֱ?ӹر?,??????ǰ????Ϊ????Window????ʹ??HideCurrentWindow()???????йر?");
            }
            else if (typeof(IPanelBaseInterface).IsAssignableFrom(typeof(T)))
            {
                 panelLayer.HideWindowOrPanelById(screenId);

            }
            else
            {
                MyDebugTool.LogError("??ǰ??????δ??ʼ??");
            }

           
        }
    }

    public void HideAllWindowOrPanel(bool animate = true)
    {
        HideAllWindows(animate);
        HideAllPanels(animate);
    }

    public void HideAllPanels(bool animate = true)
    {
        panelLayer.HideAll(animate);
    }

    public void HideAllWindows(bool animate = true)
    {
        windowLayer.HideAll(animate);
    }


    
    private bool WindowOrPanelAlreadyRegistered(string screenId)
    {
        if (windowLayer.WindowOrPanelAlreadyRegistered(screenId))
        {
           
            return true;
        }

        if (panelLayer.WindowOrPanelAlreadyRegistered(screenId))
        {
            
            return true;
        }

       
        return false;
    }

    private void RegisterWindowOrPanelToLayer(string screenId, UIWindowAndPanelBaseInterfaces controller, Transform screenTransform)
    {
        IWindowBaseInterface window = controller as IWindowBaseInterface;
        if (window != null)
        {
            windowLayer.RegisterWindowOrPanel(screenId, window);
            if (screenTransform != null)
            {
                windowLayer.SetScreenParent(controller, screenTransform);
               
            }

            return;
        }

        IPanelBaseInterface panel = controller as IPanelBaseInterface;
        if (panel != null)
        {
            panelLayer.RegisterWindowOrPanel(screenId, panel);
            if (screenTransform != null)
            {
                panelLayer.SetScreenParent(controller, screenTransform);
            }
        }
    }
    private void UnregisterScreen(string screenId, UIWindowAndPanelBaseInterfaces controller, Transform screenTransform)
    {
        IWindowBaseInterface window = controller as IWindowBaseInterface;
        if (window != null)
        {
            windowLayer.UnregisterWindowOrPanel(screenId, window);
            if (screenTransform != null)
            {
                windowLayer.SetScreenParent(controller, screenTransform);
            }
           

            return;
        }

        IPanelBaseInterface panel = controller as IPanelBaseInterface;
        if (panel != null)
        {
            panelLayer.UnregisterWindowOrPanel(screenId, panel);
            if (screenTransform != null)
            {
                panelLayer.SetScreenParent(controller, screenTransform);
            }
        }
    }

  
  
   
}
