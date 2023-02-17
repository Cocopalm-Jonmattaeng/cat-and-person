using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TextManager;

public class DisplayWindow : MonoBehaviour
{
    [System.Serializable]
    public class StrObjMap : CustomDic.SerializableDictionary<string, GameObject>
    {

    }

    public static DisplayWindow instance { 
        get; private set; 
    }

    public string prevWin = "";
    public string currWin = "";

    public StrObjMap som;

    private void Start()
    {
        instance = this;
    }

    public static void Display(string win) {
        instance.prevWin = instance.currWin;
        instance.currWin = win;

        if (instance.prevWin != "")
        {
            instance.som[instance.prevWin].SetActive(false);
        }
        instance.som[win].SetActive(true);
    }

    public static void Close() {
        if (instance.currWin != "")
        {
            instance.som[instance.currWin].SetActive(false);
        }

        if (instance.prevWin != "") {
            instance.som[instance.prevWin].SetActive(true);
        }

        var ww = instance.currWin;
        instance.currWin = instance.prevWin;
        instance.prevWin = ww;
    }
}
