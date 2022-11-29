using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class UIEventCenter {
   
    private static Dictionary<Type, UIEventInterface> events = new Dictionary<Type, UIEventInterface>();


    public static T Get<T>() where T : UIEventInterface, new()
    {
        Type eventType = typeof(T);
        UIEventInterface tmpEvent;

        if (events.TryGetValue(eventType, out tmpEvent))
        {
            return (T)tmpEvent;
        }

        return (T)BindEvent(eventType);
    }


    private static UIEventInterface BindEvent(Type eventType)
    {
        UIEventInterface tmpEvent;
        if (events.TryGetValue(eventType, out tmpEvent))
        {

            return tmpEvent;
        }

        tmpEvent = (UIEventInterface)Activator.CreateInstance(eventType);
        events.Add(eventType, tmpEvent);
        return tmpEvent;
    }
}


public interface UIEventInterface
{

}

public class UIEvent:UIEventInterface { 

}


public class UIEventBase : UIEvent
{
    private Action callback;
    public void AddListener(Action handler)
    {

        callback += handler;
    }


    public void RemoveListener(Action handler)
    {
        callback -= handler;
    }


    /// <summary>
    /// 向外发送数据请求
    /// </summary>
    public void Dispatch()
    {
        if (callback != null)
        {
            callback();
        }
    }

    public void RequestDate() { 
    
    }
}
public class UIEventBase<T> : UIEvent
{
    private Action<T> callback;
    public void AddListener(Action<T> handler)
    {

        callback += handler;
    }

  
    public void RemoveListener(Action<T> handler)
    {
        callback -= handler;
    }

  
    public void Dispatch(T arg1)
    {
        if (callback != null)
        {
            callback(arg1);
        }
    }

}
public class UIEventBase<T,U> : UIEvent
{
    private Action<T, U> callback;
    public void AddListener(Action<T, U> handler)
    {

        callback += handler;
    }


    public void RemoveListener(Action<T, U> handler)
    {
        callback -= handler;
    }


    public void Dispatch(T arg1,U arg2)
    {
        if (callback != null)
        {
            callback(arg1, arg2);
        }
    }
}
public class UIEventBase<T,U,V> : UIEvent
{
    private Action<T, U, V> callback;
    public void AddListener(Action<T, U, V> handler)
    {

        callback += handler;
    }


    public void RemoveListener(Action<T, U, V> handler)
    {
        callback -= handler;
    }


    public void Dispatch(T arg1, U arg2,V arg3)
    {
        if (callback != null)
        {
            callback(arg1, arg2,arg3);
        }
    }
}
public class UIEventBase<T, U, V,S> : UIEvent
{
    private Action<T, U, V, S> callback;
    public void AddListener(Action<T, U, V, S> handler)
    {

        callback += handler;
    }


    public void RemoveListener(Action<T, U, V, S> handler)
    {
        callback -= handler;
    }


    public void Dispatch(T arg1, U arg2, V arg3,S arg4)
    {
        if (callback != null)
        {
            callback(arg1, arg2, arg3,arg4);
        }
    }
}
