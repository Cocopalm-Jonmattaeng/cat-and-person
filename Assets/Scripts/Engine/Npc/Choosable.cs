using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Choosable
{
    protected NPC n;

    public KeyedText text;

    public Choosable(NPC n, KeyedText t) { 
        this.text = t;
        this.n = n;
    }

    public abstract IEnumerator Choose();
}
