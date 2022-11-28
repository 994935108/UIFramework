using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum UILayer
{

    BackgroundLayer,//������
    MiddleLayer,//�м��
    Foreground,//ǰ����
    Popup,//������
    Toast,//��Ϣ��ʾ��
    Top,//�ײ㣬������������ʧ���Ժ��ò���

}

public interface UIControllerInterfaces
{
    string ScreenId { get; set; }
     bool IsVisible { get; }

    bool HasInAnim { get; }
    bool HasOutAnim { get; }


    public abstract UILayer uilayer { get; }//�����㼶
    public abstract UIType uiType { get; }//UI����
    public void Init();


    public void Show(UIPropertiesInterface props = null, bool anim = true);//��ʾ
    public void Hide(bool anim = true);

    public void InTransitionFinish(Action finish);

    public void OutTransitionFinish(Action finish);

}


public interface IWindowController : UIControllerInterfaces
{
     bool IsHideOnOpenForegroundWindow { get; }//��ǰ�����ڵ�ʱ���Ƿ����ص�ǰ����

    public void ReShow(UIPropertiesInterface props = null);//���㷵�ص�ʱ��������ʾ

  

}




public interface IPanelController : UIControllerInterfaces
{
    // public PanelShowType PanelShowType { get; }//��ʾ��������


}


