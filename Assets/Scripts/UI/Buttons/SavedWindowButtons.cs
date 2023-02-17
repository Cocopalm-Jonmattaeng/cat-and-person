using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedWindowButtons : MonoBehaviour
{
    public void SWOK() {
        DisplayWindow.instance.prevWin = "PlayerInfo";
        DisplayWindow.Close();
    }
}
