using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public enum UIType
{
    DefaultType,//默认形式
    FixedDistanceType,//固定距离显示
    FollowType,//跟随类型
    InSpaceType,//放置于空间中
}
public enum UILayer
{

    BackgroundLayer,//背景层
    MiddleLayer,//中间层
    Foreground,//前景层
    Popup,//弹出层
    Toast,//消息提示层
    Top,//底层，比如网络连接失败以后不让操作

}

public interface UIWindowAndPanelBaseInterfaces
{
    string ScreenId { get; set; }
     bool IsVisible { get; }

    bool HasInAnim { get; }
    bool HasOutAnim { get; }


    public abstract UILayer uilayer { get; }//所处层级
    public abstract UIType uiType { get; }//UI类型
    public void Init();


    public void Show(UIPropertiesInterface props = null, bool anim = true);//显示
    public void Hide(bool anim = true);

    //隐藏面板或窗口时候的过渡动画
    public void HideTransitionAnimFinish(Action finish);

    //显示面板或窗口时候的过度动画
    public void ShowTransitionAnimFinish(Action finish);

}


public interface IWindowBaseInterface : UIWindowAndPanelBaseInterfaces
{
     bool IsHideOnOpenForegroundWindow { get; }//打开前景窗口的时候是否隐藏当前窗口

    public void Redisplay(UIPropertiesInterface props = null);//顶层返回的时候重新显示

}




public interface IPanelBaseInterface : UIWindowAndPanelBaseInterfaces
{
   

}


