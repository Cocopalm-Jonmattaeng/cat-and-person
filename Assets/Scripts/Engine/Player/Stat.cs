using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using static TextManager;

[System.Serializable]
public class StatDict {
    public int fatigue;
    public int speed;
    public int sight;
    public int strength;
    public int stress;

    public StatDict(int fatigue, int speed, int sight, int strength, int stress)
    {
        this.fatigue = fatigue;
        this.speed = speed;
        this.sight = sight;
        this.strength = strength;
        this.stress = stress;
    }

    public Dictionary<string, int> vars {
        get {
            var prop = new Dictionary<string, int>();

            var values = this.GetType()
                     .GetFields()
                     .Select(field => (int)field.GetValue(this))
                     .ToList();
            var names = typeof(StatDict).GetFields()
                            .Select(field => field.Name)
                            .ToList();

            foreach (var nn in names) {
                prop[nn] = values[names.IndexOf(nn)];
            }

            return prop;
        }
    }
}

public class Stat : MonoBehaviour
{
    public List<string> locTextIndexer = new();

    public string _name {
        get { return SaveLoadUtility.instance.data.name; }
        set { SaveLoadUtility.instance.data.name = value; }
    }
    public StatDict prop { 
        get { return SaveLoadUtility.instance.data.prop; }
        set {
            SaveLoadUtility.instance.data.prop = value;
        }
    }

    #region singleton
    public static Stat instance
    {
        get; private set;
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }
    #endregion
}
