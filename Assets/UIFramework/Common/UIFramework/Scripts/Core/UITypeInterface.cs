using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum UIType
{
    DefaultType,//Ĭ����ʽ
    FixedDistanceType,//�̶�������ʾ
    FollowType,//��������
    InSpaceType,//�����ڿռ���
}
public interface UITypeInterface 
{
    public UIType uiType { get; }
  
   
}

public interface DefaultTypeInterface: UITypeInterface
{ 


}

public interface FixedDistanceTypeInterface : UITypeInterface
{
    public float fixDistance { get; set; }//�̶�����


}

public interface FollowTypeInterface : UITypeInterface
{
    public Transform followNode { set; get; }//����Ľڵ�
    public float followDistance { set; get; }//����ľ���


}
public interface InSpaceTypeInterface : UITypeInterface
{
    public Vector3 spacePosition { set; get; }//�ڿռ��е�λ��
    public Quaternion spaceRotation { set; get; }//�ڿռ��жԵ���ת


}

