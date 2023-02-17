using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bed : NPC
{
    // Start is called before the first frame update
    public override IEnumerator ScriptRunnerW()
    {
        Move.instance.movable = false;
        Speak(KeyedText.keyedText("Home", 1));
        yield return new WaitUntil(() => 
            TextManager.instance.isSpeaking == false
        );

        Think(KeyedText.keyedText("Home", 2));
        yield return new WaitUntil(() =>
            TextManager.instance.isSpeaking == false
        );

        var pr = Stat.instance.prop;
        pr.fatigue -= (Stat.instance.prop.fatigue < 65 ? Stat.instance.prop.fatigue : 65);
        Stat.instance.prop = pr;

        Debug.Log(Stat.instance.prop.fatigue);

        PlayerThink(KeyedText.keyedText("PlayerThink", 1));
        yield return new WaitUntil(() =>
            TextManager.instance.isSpeaking == false
        );
        Move.instance.movable = true;
    }
}
