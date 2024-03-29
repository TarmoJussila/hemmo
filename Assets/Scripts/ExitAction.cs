﻿using UnityEngine;
using UnityEngine.InputSystem;

public class ExitAction : MonoBehaviour
{
    private bool isActionTriggered = false;

    public void ExitGame()
    {
        if (isActionTriggered)
        {
            return;
        }

        Application.Quit();
        isActionTriggered = true;
    }

    private void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null)
        {
            return;
        }

        if (keyboard.enterKey.wasPressedThisFrame)
        {
            ExitGame();
        }
    }
}