using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum UIType
{
    DefaultType,//默认形式
    FixedDistanceType,//固定距离显示
    FollowType,//跟随类型
    InSpaceType,//放置于空间中
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
    public float fixDistance { get; set; }//固定距离


}

public interface FollowTypeInterface : UITypeInterface
{
    public Transform followNode { set; get; }//跟随的节点
    public float followDistance { set; get; }//跟随的距离


}
public interface InSpaceTypeInterface : UITypeInterface
{
    public Vector3 spacePosition { set; get; }//在空间中的位置
    public Quaternion spaceRotation { set; get; }//在空间中对的旋转


}

