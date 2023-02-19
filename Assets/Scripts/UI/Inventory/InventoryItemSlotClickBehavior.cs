using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemSlotClickBehavior : MonoBehaviour, IPointerDownHandler
{
    public int idx;

    public void OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine(OPDProcessor());
    }

    private IEnumerator OPDProcessor() {
        var id = InventoryItemSlotInit.instance.itemDetail;

        if (Inventory.items[idx].id <= 0)
        {
            id.SetActive(false);
            yield break;
        }

        id.SetActive(true);

        var iih = InventoryItemViewObjectHolder.instance;

        while (iih == null) {
            yield return null;
            iih = InventoryItemViewObjectHolder.instance;
        }

        yield return null;

        Sprite tit = null;

        foreach (var i in GetComponentsInChildren<Image>())
        {
            if (i.gameObject.name.Contains("texture")) tit = i.sprite;
        }

        yield return null;
        yield return null;

        Debug.Log(tit);
        Debug.Log(iih.iT);

        iih.iT.sprite = tit;
        iih.iN.text = Inventory.items[idx].name;
        iih.iD.text = Inventory.items[idx].description;

        iih.sID = Inventory.items[idx].id;

        if (Inventory.items[idx].id <= 0)
        {
            id.SetActive(false);
        }
    }

    void Start() {
        foreach (var i in GetComponentsInChildren<Text>())
        {
            if (i.gameObject.name.Contains("order")) {
                i.text = (idx + 1).ToString();
            }
            if (i.gameObject.name.Contains("count")) {
                i.text = (Inventory.items[idx].count < 2 ? "" : $"x{Inventory.items[idx].count}");
            }
        }
    }
}
