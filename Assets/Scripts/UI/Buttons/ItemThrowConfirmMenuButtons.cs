using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Inventory;

public class ItemThrowConfirmMenuButtons : MonoBehaviour
{
    public void ITCThrow() {
        RemoveItem(new Item(InventoryItemViewObjectHolder.instance.sID, 1));

        Debug.Log("ass");

        StartCoroutine(iisiir());
    }

    public void ITCCancel() {
        Debug.Log("ass");

        StartCoroutine(iisiir());
    }

    private IEnumerator iisiir() {
        DisplayWindow.instance.som["PlayerInfo"].SetActive(true);
        yield return null;
        InventoryItemSlotInit.instance.Refresh();
        yield return null;
        yield return null;
        DisplayWindow.Close();
    }
}
