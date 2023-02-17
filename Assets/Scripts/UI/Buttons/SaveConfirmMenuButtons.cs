using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveConfirmMenuButtons : MonoBehaviour
{
    public void SCOverwrite() {
        SaveLoadUtility.Save();
        DisplayWindow.Display("Saved");
    }

    public void SCCancel() { 
        DisplayWindow.Close();
    }
}
