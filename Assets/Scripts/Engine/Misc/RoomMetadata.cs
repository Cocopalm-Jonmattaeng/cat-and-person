using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum RoomType {
    INDOOR,
    OUTDOOR,
    SPECIAL
}

[Serializable]
public class RoomMetadata: MonoBehaviour {
    public string roomGroup;
    public string roomKey;

    public KeyedText roomName;

    public RoomType roomType;

    public AudioSource roomBGM;

    public Vector2 roomAreaLT;
    public Vector2 roomAreaRB;

    /*public Dictionary<string, TRObject> objects;*/

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(roomAreaLT, new Vector2(roomAreaLT.x, roomAreaRB.y));
        Gizmos.DrawLine(roomAreaRB, new Vector2(roomAreaRB.x, roomAreaLT.y));
        Gizmos.DrawLine(roomAreaRB, new Vector2(roomAreaLT.x, roomAreaRB.y));
        Gizmos.DrawLine(roomAreaLT, new Vector2(roomAreaRB.x, roomAreaLT.y));
    }
}
