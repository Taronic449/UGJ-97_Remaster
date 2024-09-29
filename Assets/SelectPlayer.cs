using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectPlayer : MonoBehaviour
{
    private bool keyboardPlayerRegistered = false;
    private bool gamepadPlayerRegistered = false;

    public GameObject keyboardPlayer;
    public GameObject gamepadPlayer;

    public byte count = 0;


    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && !keyboardPlayerRegistered)
        {
            RegisterPlayer("Keyboard");
            keyboardPlayer.gameObject.SetActive(true);
            keyboardPlayerRegistered = true;
        }

        if (Gamepad.current != null && Gamepad.current.dpad.down.wasPressedThisFrame && !gamepadPlayerRegistered)
        {
            RegisterPlayer("Gamepad");
            gamepadPlayer.gameObject.SetActive(true);
            gamepadPlayerRegistered = true;
        }
    }

    void RegisterPlayer(string playerType)
    {
        Debug.Log($"{playerType} player registered!");
    }

    public void NewPlayer()
    {
        count++;
        transform.GetChild(count + 1).gameObject.SetActive(true);
    }

    public void PlayerLeave()
    {
        count++;
        transform.GetChild(count + 1).GetChild(0).gameObject.SetActive(true);
    }
}
