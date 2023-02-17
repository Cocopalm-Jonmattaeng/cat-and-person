using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Tilemaps;

public class PlayerUI : MonoBehaviour
{
    public RoomMetadata currentRoom;
    public RoomNameAnimation ra;

    public PostProcessVolume blurEffVolume;

    public bool menuOpened = false;
    private bool menuTogglable = true;

    public static PlayerUI instance {
        get; private set;
    }

    private void Start()
    {
        instance = this;

        blurEffVolume.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (menuTogglable && !TextManager.instance.isSpeaking)
        {
            if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape))
            {
                if (!menuOpened)
                {
                    DisplayWindow.Display("PlayerInfo");
                    StartCoroutine(MenuRefresh());
                }
                else {
                    if (DisplayWindow.instance.currWin == "PlayerInfo")
                    {
                        DisplayWindow.instance.prevWin = "";
                        DisplayWindow.Close();
                    }
                    else {
                        return;
                    }
                }

                menuOpened = !menuOpened;

                Move.instance.movable = !menuOpened;

                blurEffVolume.gameObject.SetActive(menuOpened);
                StartCoroutine(ToggleDelayer());
            }
        }
    }

    private IEnumerator MenuRefresh() {
        while (MenuBarController.instance == null) { 
            yield return null;
        }

        MenuBarController.instance.Refresh();
    }

    private IEnumerator ToggleDelayer()
    {
        menuTogglable = false;
        yield return new WaitForSecondsRealtime(1.0f);
        menuTogglable = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentRoom = collision.gameObject.GetComponentInParent<RoomMetadata>();
        ra.Animate(currentRoom.roomName.text);

        foreach (var i in GameObject.FindGameObjectsWithTag("MapVisuals"))
        {
            if (i.name != "Objects")
            {
                if (i.GetComponentInParent<RoomMetadata>().roomGroup != currentRoom.roomGroup) i.GetComponent<TilemapRenderer>().enabled = false;
                else i.GetComponent<TilemapRenderer>().enabled = true;
            }
            else
            {
                foreach (var j in i.GetComponentsInChildren<Transform>())
                {
                    j.gameObject.SetActive(i.GetComponentInParent<RoomMetadata>().roomGroup == currentRoom.roomGroup);
                }
            }
        }
    }
}
