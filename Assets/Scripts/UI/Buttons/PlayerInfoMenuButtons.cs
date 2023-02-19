using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Experimental.AI;
using static Inventory;

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

    public void PIItemUse() {
        if (ItemBehavior.instance.itemUsable[InventoryItemViewObjectHolder.instance.sID - 1]) {
            RemoveItem(new Item(InventoryItemViewObjectHolder.instance.sID, 1));

            ItemBehavior.Use(InventoryItemViewObjectHolder.instance.sID);

            InventoryItemSlotInit.instance.Refresh();
        }
    }

    public void PIItemThrow() {
        DisplayWindow.Display("ItemThrowConfirm");
    }
}
