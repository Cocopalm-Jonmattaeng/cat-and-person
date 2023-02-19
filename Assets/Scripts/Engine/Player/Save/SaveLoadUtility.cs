using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;
using static Day;
using static Inventory;

[Serializable]
public class SData {
    public Vector2 pos;
    public string name;
    public StatDict prop;

    public DDate date;

    public Item[] items;

    public SData(Vector2 pos, string name, StatDict prop, DDate d, Item[] items)
    {
        this.pos = pos;
        this.name = name;
        this.prop = prop;
        date = d;
        this.items = items;
    }
}

public class SaveLoadUtility : MonoBehaviour
{
    public static SaveLoadUtility instance
    {
        get; private set;
    }

    public SData data;

    private void Start()
    {
        instance = this;

        Load();
    }

    private void Update()
    {
        if (this != instance) {
            instance = this;
        }
    }

    public static void Save() {
        instance.data.pos = (Vector2)Move.instance.transform.localPosition;

        File.Delete(Application.persistentDataPath + "/savef.ile");
        File.WriteAllText(
            Application.persistentDataPath + "/savef.ile",
            JsonUtility.ToJson(instance.data)
        );
    }

    public static void Load()
    {
        try
        {
            instance.data = JsonUtility.FromJson<SData>(File.ReadAllText(Application.persistentDataPath + "/savef.ile"));
        }
        catch (Exception) {

            instance.data = new(
                Vector2.zero,
                "!",
                new StatDict(
                    100,
                    50,
                    0,
                    25,
                    0
                ),
                new(2023, 1, 0.0f),
                new Item[15] { 
                    new Item(0, 0),
                    new Item(0, 0),
                    new Item(0, 0),
                    new Item(0, 0),
                    new Item(0, 0),
                    new Item(0, 0),
                    new Item(0, 0),
                    new Item(0, 0),
                    new Item(0, 0),
                    new Item(0, 0),
                    new Item(0, 0),
                    new Item(0, 0),
                    new Item(0, 0),
                    new Item(0, 0),
                    new Item(0, 0)
                }
            );
        }
    }
}
