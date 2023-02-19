using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemSlotInit : MonoBehaviour
{
    public static InventoryItemSlotInit instance
    {
        get; private set;
    }

    public GameObject itemDetail;

    public GameObject slot;

    public Sprite[] textures;

    private void Start()
    {
        instance = this;

        Refresh();
    }

    // Update is called once per frame
    public void Refresh()
    {
        itemDetail.SetActive(false);

        if (GameObject.FindWithTag("InvenItem") != null)
        {
            foreach (var go in GameObject.FindGameObjectsWithTag("InvenItem"))
            {
                Destroy(go);
            }
        }

        for (int i = 0; i < 15; i++) {
            var obj = Instantiate(slot);

            obj.transform.SetParent(transform, false);
            obj.GetComponent<InventoryItemSlotClickBehavior>().idx = i;

            foreach (var j in obj.GetComponentsInChildren<Image>())
            {
                if (j.gameObject.name.Contains("texture")) {
                    j.sprite = Inventory.items[i].id <= 0 ? null : textures[Inventory.items[i].id - 1];
                }
            }
        }
    }
}
