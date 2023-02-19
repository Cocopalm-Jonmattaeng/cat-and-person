using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemSlotClickBehavior : MonoBehaviour, IPointerClickHandler
{
    public int idx;

    public void OnPointerClick(PointerEventData eventData)
    {
        var id = InventoryItemSlotInit.instance.itemDetail;

        id.SetActive(true);

        var iih = InventoryItemViewObjectHolder.instance;

        Sprite tit = null;

        foreach(var i in GetComponentsInChildren<Image>()) {
            if (i.gameObject.name.Contains("texture")) tit = i.sprite;
        }

        iih.iT.sprite = tit;
        iih.iN.text = Inventory.items[idx].name;
        iih.iD.text = Inventory.items[idx].description;
    }

    void Start() {
        foreach (var i in GetComponentsInChildren<Text>())
        {
            if (i.gameObject.name.Contains("order")) {
                i.text = (idx + 1).ToString();
            }
            if (i.gameObject.name.Contains("count")) {
                i.text = (Inventory.items[idx].count <= 2 ? "" : $"x{Inventory.items[idx].count}");
            }
        }
    }
}
