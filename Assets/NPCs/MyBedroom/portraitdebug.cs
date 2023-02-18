using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class portraitdebug : NPC
{
    // Start is called before the first frame update
    public override IEnumerator ScriptRunnerW()
    {
        Move.instance.movable = false;

        Speak(KeyedText.keyedText("Home", 3));
        yield return new WaitUntil(() =>
             TextManager.instance.isSpeaking == false
        );

        Move.instance.movable = true;
    }
}
