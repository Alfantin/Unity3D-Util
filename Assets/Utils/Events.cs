using System.Collections.Generic;
using UnityEngine.Events;

public static class Events {

    private static Dictionary<string, BaseEvent> Register = new Dictionary<string, BaseEvent>();
    private class BaseEvent : UnityEvent<object> { }

    public static void Add(string name, UnityAction<object> listener) {
        if (!Register.TryGetValue(name, out BaseEvent e)) {
            e = new BaseEvent();
            Register.Add(name, e);
        }
        e.AddListener(listener);
    }

    public static void Remove(string name, UnityAction<object> listener) {
        if (Register.TryGetValue(name, out BaseEvent e)) {
            e.RemoveListener(listener);
        }
    }

    public static void Invoke(string name, object arg = null) {
        if (Register.TryGetValue(name, out BaseEvent e)) {
            e.Invoke(arg);
        }
    }

}