using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class UIEventCenter {
    private static readonly SignalHub hub = new SignalHub();
    public static T Get<T>() where T : UIEventInterface, new()
    {
        return hub.Get<T>();
    }
}

public class SignalHub {
    private Dictionary<Type, UIEventInterface> signals = new Dictionary<Type, UIEventInterface>();

   
    public T Get<T>() where T : UIEventInterface, new()
    {
        Type signalType = typeof(T);
        UIEventInterface signal;

        if (signals.TryGetValue(signalType, out signal))
        {
            return (T)signal;
        }

        return (T)Bind(signalType);
    }

   
    private UIEventInterface Bind(Type signalType)
    {
        UIEventInterface signal;
        if (signals.TryGetValue(signalType, out signal))
        {
            UnityEngine.Debug.LogError(string.Format("Signal already registered for type {0}",
                signalType.ToString()));
            return signal;
        }

        signal = (UIEventInterface)Activator.CreateInstance(signalType);
        signals.Add(signalType, signal);
        return signal;
    }

    private UIEventInterface Bind<T>() where T : UIEventInterface, new()
    {
        return Bind(typeof(T));
    }

   

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
