using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    public static ItemBehavior instance {
        get; private set;
    }

    public bool[] itemUsable;

    void Start()
    {
        instance = this;
    }

    public static void Use(int id) { 
        switch (id) {
            case 1:
                Debug.Log("모릔 대갈통 사용");
                break;
            default:
                return;
        }
    }
}
