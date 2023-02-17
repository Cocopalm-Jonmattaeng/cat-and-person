using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerInfoMenuButtons : MonoBehaviour
{
    public void PISave() {
        if (File.Exists(Application.persistentDataPath + "/savef.ile"))
        {
            DisplayWindow.Display("SaveConfirm");
        }
        else {
            SaveLoadUtility.Save();
            DisplayWindow.Display("Saved");
        }
    }
}
