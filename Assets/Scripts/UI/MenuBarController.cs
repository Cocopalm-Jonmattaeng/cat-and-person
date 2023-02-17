using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MenuBarController : MonoBehaviour
{
    public static MenuBarController instance
    {
        get; private set;
    }

    public GameObject menuBar;

    private void Start()
    {
        instance = this;

        Refresh();
    }

    // Update is called once per frame
    public void Refresh()
    {
        if (GameObject.FindWithTag("TextAndBar") != null)
        {
            foreach (var go in GameObject.FindGameObjectsWithTag("TextAndBar")) { 
                Destroy(go);
            }
        }

        foreach (var propI in Stat.instance.prop.vars)
        {
            var obj = Instantiate(instance.menuBar, instance.transform);
            obj.GetComponentInChildren<Text>().text = KeyedText.keyedText("PropName", Stat.instance.locTextIndexer.IndexOf(propI.Key) + 1).text;

            foreach (var im in obj.GetComponentsInChildren<Image>())
            {
                if (im.type == Image.Type.Filled) im.fillAmount = (propI.Value / 100f);
            }
        }
    }
}