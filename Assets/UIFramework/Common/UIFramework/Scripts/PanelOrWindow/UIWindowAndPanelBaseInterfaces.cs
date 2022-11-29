using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public enum UIType
{
    DefaultType,//Ĭ����ʽ
    FixedDistanceType,//�̶�������ʾ
    FollowType,//��������
    InSpaceType,//�����ڿռ���
}
public enum UILayer
{

    BackgroundLayer,//������
    MiddleLayer,//�м��
    Foreground,//ǰ����
    Popup,//������
    Toast,//��Ϣ��ʾ��
    Top,//�ײ㣬������������ʧ���Ժ��ò���

}

public interface UIWindowAndPanelBaseInterfaces
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

    //�������򴰿�ʱ��Ĺ��ɶ���
    public void HideTransitionAnimFinish(Action finish);

    //��ʾ���򴰿�ʱ��Ĺ��ȶ���
    public void ShowTransitionAnimFinish(Action finish);

}


public interface IWindowBaseInterface : UIWindowAndPanelBaseInterfaces
{
     bool IsHideOnOpenForegroundWindow { get; }//��ǰ�����ڵ�ʱ���Ƿ����ص�ǰ����

    public void Redisplay(UIPropertiesInterface props = null);//���㷵�ص�ʱ��������ʾ

}




public interface IPanelBaseInterface : UIWindowAndPanelBaseInterfaces
{
   

}


