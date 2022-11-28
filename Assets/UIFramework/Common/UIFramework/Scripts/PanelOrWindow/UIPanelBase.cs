using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIPanelBase : UIScenePanel<UIPanelProperties>
{
    
}
public class UIPanelBase<TProps> : UIScenePanel<TProps>, IPanelController where TProps : UIPropertiesInterface
{
  
}
