using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Dictionary<KeyCode, EventType> keybindingsHold = new Dictionary<KeyCode, EventType>();
    public Dictionary<KeyCode, EventType> keybindingsUp = new Dictionary<KeyCode, EventType>();

    private void Start()
    {
        keybindingsUp.Add(KeyCode.M, EventType.TOGGLE_DIVE);
        keybindingsHold.Add(KeyCode.Space, EventType.SHOOT);
        keybindingsHold.Add(KeyCode.W, EventType.AIM_UP);
        keybindingsHold.Add(KeyCode.A, EventType.AIM_LEFT);
        keybindingsHold.Add(KeyCode.S, EventType.AIM_DOWN);
        keybindingsHold.Add(KeyCode.D, EventType.AIM_RIGHT);
        // cheats etc
        keybindingsHold.Add(KeyCode.Alpha1, EventType.OFFSET_X_M);
        keybindingsHold.Add(KeyCode.Alpha2, EventType.OFFSET_X_P);
        keybindingsHold.Add(KeyCode.Alpha3, EventType.OFFSET_Y_M);
        keybindingsHold.Add(KeyCode.Alpha4, EventType.OFFSET_Y_P);
        keybindingsHold.Add(KeyCode.Alpha5, EventType.LIVE_DOWN);
        keybindingsHold.Add(KeyCode.Alpha6, EventType.LIVE_UP);
    }

    private void Update()
    {
        // check if any key is pressed and fire event related to it
        foreach (KeyValuePair<KeyCode, EventType> keybinding in keybindingsHold)
        {
            if (Input.GetKey(keybinding.Key))
            {
                EventSystem.InvokeEvent(keybinding.Value);
            }
        }

        // check if any key is released and fire event related to it
        foreach (KeyValuePair<KeyCode, EventType> keybinding in keybindingsUp)
        {
            if (Input.GetKeyUp(keybinding.Key))
            {
                EventSystem.InvokeEvent(keybinding.Value);
            }
        }
    }
}