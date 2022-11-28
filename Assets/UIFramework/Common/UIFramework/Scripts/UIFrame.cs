using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFrame : SingletonMonoBehaviour<UIFrame>
{
    //public static UIFrame Instance;

   
   
    public UIPanelLayerController panelLayer;
    public UIWindowLayerController windowLayer;




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

        //分辨率适配
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
            panelLayer = gameObject.GetComponent<UIPanelLayerController>();
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
            windowLayer = gameObject.GetComponent<UIWindowLayerController>();
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
    /// 显示面板
    /// </summary>
    /// <param name="screenId"></param>
    public void ShowWindowOrPanelByType<T>()where T:UIControllerInterfaces {
        string screenId = typeof(T).ToString();
        if (!WindowOrPanelAlreadyRegistered(screenId))
        {
            CreatePanelOrWindowByID(screenId);
        }

        if (typeof(IWindowController).IsAssignableFrom(typeof(T)))
        {
            windowLayer.ShowScreenById(screenId);
        }
        else if (typeof(IPanelController).IsAssignableFrom(typeof(T)))
        {
            panelLayer.ShowScreenById(screenId);

        }
        else
        {
            MyDebugTool.LogError("没有找到UI："+ screenId);
        }
    }

    /// <summary>
    /// 显示一个面板，根据面板ID，并且设置显示参数
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
      
        if (typeof(IWindowController).IsAssignableFrom(typeof(T)))
        {
            windowLayer.ShowWindowOrPanelById<TProps>(screenId, properties);
        }
        else if (typeof(IPanelController).IsAssignableFrom(typeof(T)))
        {
            panelLayer.ShowWindowOrPanelById<TProps>(screenId, properties);

        }
        else
        {
            MyDebugTool.LogError("啥也不是");
        }

    }
    private void  CreatePanelOrWindowByID(string screenId) {
        GameObject UI = Instantiate(Resources.Load<GameObject>("UI/Panel/" + screenId));
        UI.gameObject.name = screenId;
        UI.SetActive(false);
        UIControllerInterfaces uIController = UI.GetComponent<UIControllerInterfaces>();
        uIController.Init();
        RegisterWindowOrPanelToLayer(screenId, UI.GetComponent<UIControllerInterfaces>(), UI.transform);

        MyDebugTool.LogError("加载UI:" + screenId);

    }
    public T GetWindowOrPanel<T>()where T: UIControllerInterfaces
    {
        string screenId = typeof(T).ToString();

        if (typeof(IWindowController).IsAssignableFrom(typeof(T)))
        {
          return   (T)windowLayer.GetWindowOrPanelByScreenId(screenId);
        }
        else if (typeof(IPanelController).IsAssignableFrom(typeof(T)))
        {
            return (T)panelLayer.GetWindowOrPanelByScreenId(screenId);

        }
        else {
            MyDebugTool.LogError("当前加载的UI窗体还没初始化，不能直接获取，窗体名称："+ screenId);
        }
       
       

        return default;
    
    }

    public void HideCurrentWindow() {
        if (windowLayer.CurrentWindow!=null) {
            windowLayer.HideWindowOrPanel(windowLayer.CurrentWindow);
        }
    }

   
    public void HidePanelById<T>()where T:UIControllerInterfaces {
        string screenId = typeof(T).ToString();

        if (WindowOrPanelAlreadyRegistered(screenId))
        {
            if (typeof(IWindowController).IsAssignableFrom(typeof(T)))
            {
                // windowLayer.HideScreenById(screenId);

                Debug.LogError("当前关闭的窗体为Window类型，不能直接关闭,如果当前窗体为顶层Window，可使用HideCurrentWindow()方法进行关闭");
            }
            else if (typeof(IPanelController).IsAssignableFrom(typeof(T)))
            {
                 panelLayer.HideWindowOrPanelById(screenId);

            }
            else
            {
                MyDebugTool.LogError("当前窗体尚未初始化");
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

    private void RegisterWindowOrPanelToLayer(string screenId, UIControllerInterfaces controller, Transform screenTransform)
    {
        IWindowController window = controller as IWindowController;
        if (window != null)
        {
            windowLayer.RegisterWindowOrPanel(screenId, window);
            if (screenTransform != null)
            {
                windowLayer.SetPanelOrWindowParent(controller, screenTransform);
               
            }

            return;
        }

        IPanelController panel = controller as IPanelController;
        if (panel != null)
        {
            panelLayer.RegisterWindowOrPanel(screenId, panel);
            if (screenTransform != null)
            {
                panelLayer.SetPanelOrWindowParent(controller, screenTransform);
            }
        }
    }
    private void UnregisterScreen(string screenId, UIControllerInterfaces controller, Transform screenTransform)
    {
        IWindowController window = controller as IWindowController;
        if (window != null)
        {
            windowLayer.UnregisterWindowOrPanel(screenId, window);
            if (screenTransform != null)
            {
                windowLayer.SetPanelOrWindowParent(controller, screenTransform);
            }
           

            return;
        }

        IPanelController panel = controller as IPanelController;
        if (panel != null)
        {
            panelLayer.UnregisterWindowOrPanel(screenId, panel);
            if (screenTransform != null)
            {
                panelLayer.SetPanelOrWindowParent(controller, screenTransform);
            }
        }
    }

  
  
   
}
