using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region choosables
public class portraitdebug_Break : Choosable
{
    public portraitdebug_Break(NPC n) : base(n, "부수기")
    {
    }

    public override IEnumerator Choose()
    {
        TextManager.instance.isChoosing = false;

        Move.instance.movable = false;

        yield return null;
        yield return null;

        n.Speak(KeyedText.keyedText("Home", 4));
        yield return new WaitUntil(() =>
             TextManager.instance.isSpeaking == false
        );

        n.PlayerThink(KeyedText.keyedText("PlayerThink", 3));
        yield return new WaitUntil(() =>
            TextManager.instance.isSpeaking == false
        );

        Move.instance.movable = true;
    }
}

public class portraitdebug_Touch : Choosable
{
    public portraitdebug_Touch(NPC n) : base(n, "쓰다듬기")
    {
    }

    public override IEnumerator Choose()
    {
        TextManager.instance.isChoosing = false;

        Move.instance.movable = false;

        yield return null;
        yield return null;

        n.Speak(KeyedText.keyedText("Home", 4));
        yield return new WaitUntil(() =>
             TextManager.instance.isSpeaking == false
        );

        n.PlayerSpeak(KeyedText.keyedText("PlayerSpeak", 1));
        yield return new WaitUntil(() =>
            TextManager.instance.isSpeaking == false
        );

        n.Speak(KeyedText.keyedText("Home", 5));
        yield return new WaitUntil(() =>
             TextManager.instance.isSpeaking == false
        );

        Move.instance.movable = true;
    }
}
#endregion

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

        PlayerThink(KeyedText.keyedText("PlayerThink", 2), new portraitdebug_Break(this), new portraitdebug_Touch(this));
        yield return new WaitUntil(() =>
            TextManager.instance.isSpeaking == false
        );

        Move.instance.movable = true;
    }
}
