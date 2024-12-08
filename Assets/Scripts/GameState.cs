using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UIElements;
public class GameState
{
    public static bool isDay { get; set; }
    public static bool isFpv { get; set; }
    public static int room { get; set; } = 1;

    public static Dictionary<String, object> collectedItems { get; private set; } = new();

    #region effectsVolume
    public static float _effectsVolume { get; set; } = 1f;
    public static float effectsVolume
    {
        get => _effectsVolume;
        set
        {
            if (_effectsVolume != value)
            {
                _effectsVolume = value;
                NotifyListeners(nameof(effectsVolume));
            }
        }
    }
    #endregion

    #region ambientVolume
    public static float _ambientVolume { get; set; } = 1f;
    public static float ambientVolume
    {
        get => _ambientVolume;
        set
        {
            if (_ambientVolume != value)
            {
                _ambientVolume = value;
                NotifyListeners(nameof(ambientVolume));
            }
        }
    }
    #endregion

    #region isSoundsMuted (Mute All)
    public static bool _isSoundsMuted = false;
    public static bool isSoundsMuted
    {
        get => _isSoundsMuted;
        set
        {
            if (_isSoundsMuted != value)
            {
                _isSoundsMuted = value;
                NotifyListeners(nameof(isSoundsMuted));
            }
        }
    }
    #endregion

    #region Change Notifier
    private static Dictionary<String, List<Action<string>>> chaangeListeners = new();
    public static void AddChangeListener(Action<string> listener,params String[] names)
    {
        foreach (String name in names)
        {
            if (!chaangeListeners.ContainsKey(name))
            {
                chaangeListeners[name] = new List<Action<string>>();
            }
            chaangeListeners[name].Add(listener);
            listener(name);
        }
    }

    public static void RemoveChangeListener(Action<string> listener, params String[] names)
    {
        foreach (String name in names)
        {
            if (chaangeListeners.ContainsKey(name))
            {
                chaangeListeners[name].Remove(listener);

            }
        }
    }
    private static void NotifyListeners(String name)
    {
        if (chaangeListeners.ContainsKey(name))
        {
            foreach(var action in chaangeListeners[name])
            {
                action(name);
            }

        }
    }
    #endregion

    #region collectSubscribers
    private static List<Action<String>> collectSubscribers = new List<Action<String>>();
    public static void AddCollectListener(Action<String> subscriber)
    {
        collectSubscribers.Add(subscriber);
    }
    public static void RemoveCollectListener(Action<String> subscriber) 
    {
        collectSubscribers.Remove(subscriber);
    }
    public static void Collect(String itemName)
    {
        collectSubscribers.ForEach(s => s(itemName));
    }
    #endregion

    #region eventSubscribers
    private const string broadcastKey = "Broadcast";
    private static Dictionary<String, List<Action<String, object>>> eventSubscribers = new();
    public static void AddEventListener(Action<String, object> subscriber, params string[] eventNames)
    {
        if(eventNames == null || eventNames.Length == 0)
        {
            eventNames = new string[1] { broadcastKey };
        }
        foreach (string eventName in eventNames)
        {
            if (eventSubscribers.ContainsKey(eventName))
            {
                eventSubscribers[eventName].Add(subscriber);
            }
            else
            {
                eventSubscribers[eventName] = new() { subscriber };
            }
        }
    }
    public static void RemoveEventListener(Action<String, object> subscriber, params string[] eventNames)
    {
        if (eventNames == null || eventNames.Length == 0)
        {
            eventNames = new string[1] { broadcastKey };
        }
        foreach (string eventName in eventNames)
        {
            if (eventSubscribers.ContainsKey(eventName))
            {
                eventSubscribers[eventName].Remove(subscriber);
            }
            else UnityEngine.Debug.LogError("RemoveEventListener: Empty key -" + eventName);
        }
    }
    public static void TriggerEvent(String eventName, object data)
    {
        if (eventSubscribers.ContainsKey(eventName))
        {
            eventSubscribers[eventName].ForEach(s => s(eventName, data));
        }
        if (eventName!= broadcastKey && eventSubscribers.ContainsKey(broadcastKey))
        {
            eventSubscribers[broadcastKey].ForEach(s => s(eventName, data));
        }
    }
    #endregion

}
