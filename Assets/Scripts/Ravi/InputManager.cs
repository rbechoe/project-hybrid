using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Dictionary<KeyCode, EventType> keybindings = new Dictionary<KeyCode, EventType>();

    private void Start()
    {
        keybindings.Add(KeyCode.M, EventType.TOGGLE_DIVE);
        keybindings.Add(KeyCode.Space, EventType.SHOOT);
        keybindings.Add(KeyCode.W, EventType.AIM_UP);
        keybindings.Add(KeyCode.A, EventType.AIM_LEFT);
        keybindings.Add(KeyCode.S, EventType.AIM_DOWN);
        keybindings.Add(KeyCode.D, EventType.AIM_RIGHT);
    }

    private void Update()
    {
        // check if any key is pressed and fire event related to it
        foreach (KeyValuePair<KeyCode, EventType> keybinding in keybindings)
        {
            if (Input.GetKey(keybinding.Key))
            {
                EventSystem.InvokeEvent(keybinding.Value);
            }
        }
    }
}