using UnityEngine;
using System.Collections.Generic;

// event system that takes no arguments
public static class EventSystem
{
    private static Dictionary<EventType, System.Action> eventDictionary = new Dictionary<EventType, System.Action>();

    public static void AddListener(EventType type, System.Action function)
    {
        if (!eventDictionary.ContainsKey(type))
        {
            eventDictionary.Add(type, null);
        }

        eventDictionary[type] += (function);
    }

    public static void RemoveListener(EventType type, System.Action function)
    {
        if (eventDictionary.ContainsKey(type))
        {
            eventDictionary[type] -= (function);
        }
    }
    
    public static void InvokeEvent(EventType type)
    {
        if (eventDictionary.ContainsKey(type))
        {
            eventDictionary[type]?.Invoke();
        }
    }
}

// event system that takes an argument
public static class EventSystem<T>
{
    private static Dictionary<EventType, System.Action<T>> eventDictionary = new Dictionary<EventType, System.Action<T>>();

    public static void AddListener(EventType type, System.Action<T> function)
    {
        if (!eventDictionary.ContainsKey(type))
        {
            eventDictionary.Add(type, null);
        }

        eventDictionary[type] += (function);
    }

    public static void RemoveListener(EventType type, System.Action<T> function)
    {
        if (eventDictionary.ContainsKey(type))
        {
            eventDictionary[type] -= (function);
        }
    }
    
    public static void InvokeEvent(EventType type, T parameters)
    {
        if (eventDictionary.ContainsKey(type))
        {
            eventDictionary[type]?.Invoke(parameters);
        }
    }
}

public enum EventType
{
    // input related
    TURRET_X = 1,
    TURRET_Y = 2,
    TOGGLE_DIVE = 3,
    SHOOT = 4,
    // debug inputs
    AIM_LEFT = 5,
    AIM_RIGHT = 6,
    AIM_UP = 7,
    AIM_DOWN = 8,
    // gameplay related
    DAMAGE_PLAYER = 9,
    START_BOSS = 10,
    BOSS_DAMAGED = 11,
    // arduino input
    ROT_X = 12,
    ROT_Y = 13,
    DIVE_ON = 14,
    DIVE_OFF = 15,
    // cheats
    LIVE_DOWN = 16,
    LIVE_UP = 17,
    SCORE_UP = 18,
    OFFSET_X_P = 19, // x rotation offset +
    OFFSET_X_M = 20, // x rotation offset -
    OFFSET_Y_P = 21, // y rotation offset +
    OFFSET_Y_M = 22, // y rotation offset -
    GAME_START = 23,
    GAME_END = 24,
}