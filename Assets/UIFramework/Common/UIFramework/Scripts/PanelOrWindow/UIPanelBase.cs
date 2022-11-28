using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIPanelBase : UIScenePanel<UIPanelProperties>, IPanelController
{
    
}
public class UIPanelBase<TProps> : UIScenePanel<TProps>, IPanelController where TProps : UIPropertiesInterface
{
  
}
