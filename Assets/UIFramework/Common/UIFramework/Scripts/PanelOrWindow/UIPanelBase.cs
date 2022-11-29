using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIPanelBase : UIWindowAndPanelBasePanel<UIPanelProperties>, IPanelBaseInterface
{
    
}
public class UIPanelBase<TProps> : UIWindowAndPanelBasePanel<TProps>, IPanelBaseInterface where TProps : UIPropertiesInterface
{
  
}
