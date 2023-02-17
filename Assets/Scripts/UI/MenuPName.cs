using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MenuPName : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GetComponent<Text>().text = Stat.instance._name;
    } 
}