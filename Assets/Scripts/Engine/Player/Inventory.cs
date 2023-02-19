using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Serializable]
    public class Item {
        public int id;
        public int count;

        public Item(int id, int count)
        {
            this.id = id;
            this.count = count;
        }

        public string name {
            get
            {
                return (id <= 0 ? "" : KeyedText.keyedText("Item", id).text);
            }
        }

        public string description
        {
            get
            {
                return (id <= 0 ? "" : KeyedText.keyedText("ItemDesc", id).text);
            }
        }
    }

    public static Item[] items {
        get {
            return SaveLoadUtility.instance.data.items;
        }
        set {
            SaveLoadUtility.instance.data.items = value;
        }
    }

    public static int Count() {
        var ret = 0;

        for (int i = 0; i < 15; i++) {
            if (items[i].id != 0) {
                ret += 1;
            }
        }

        return ret;
    }

    public static void Validator() {
        if (items.Length < 15) { 
            var iL = 15 - items.Length;
            var iL2 = items.Length;

            for (int i = 0; i < iL; i++) {
                items[i + iL2] = new Item(0, 0);
            }
        }

        for (int i = 0; i < 15; i++)
        {
            if (items[i].count < 1 && items[i].id > 0)
            {
                items[i] = new Item(0, 0);
            }
            if (items[i].count > 999) {
                items[i].count = 999;
            }
        }
    }

    public static void AddItem(Item item) {
        var rItem = item;

        if (Count() >= 15) return;

        for (int i = 0; i < 15; i++)
        {
            if (items[i].id == item.id)
            {
                if ((items[i].count + item.count) > 999)
                {
                    rItem.count = (items[i].count + item.count) - 999;
                    items[i].count = 999;
                }
                else {
                    items[i].count = (items[i].count + item.count);
                    Validator();
                    return;
                }
            }
        }

        for (int i = 0; i < 15; i++)
        {
            if (items[i].id == 0)
            {
                items[i] = rItem;
            }
        }

        Validator();
    }

    public static void RemoveItem(int id)
    {
        if (Count() <= 0) return;

        for (int i = 0; i < 15; i++)
        {
            if (items[i].id == id)
            {
                items[i] = new Item(0, 0);
            }
        }

        Validator();
    }

    public static void RemoveItem(Item item) {
        if (Count() <= 0) return;

        for (int i = 0; i < 15; i++)
        {
            if (items[i].id == item.id)
            {
                items[i].count -= item.count;
            }
        }

        Validator();
    }
}
