using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Choosable
{
    protected NPC n;

    public string text;

    public Choosable(NPC n, string t) { 
        this.text = t;
        this.n = n;
    }

    public abstract IEnumerator Choose();
}
