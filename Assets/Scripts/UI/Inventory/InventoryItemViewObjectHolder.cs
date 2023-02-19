using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemViewObjectHolder : MonoBehaviour
{
    public static InventoryItemViewObjectHolder instance
    {
        get; private set;
    }

    public Image iT;
    public Text iN;
    public Text iD;

    public Button uB;
    public Button tB;

    private void Start()
    {
        instance = this;
    }
}
