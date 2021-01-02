using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : UIButtonController
{
    protected override void OnButtonClicked()
    {
        GameManager.instance.StartGame();
    }
}