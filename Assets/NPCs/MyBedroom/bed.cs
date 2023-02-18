using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

#region Choosables
public class bed_Sleep : Choosable
{
    public bed_Sleep(NPC n) : base(n, KeyedText.keyedText("Home", 2))
    {
    }

    public override IEnumerator Choose()
    {
        TextManager.instance.isChoosing = false;

        Move.instance.movable = false;

        yield return null;
        yield return null;

        Time.timeScale = 0;

        yield return n.StartCoroutine(FadeInOut.instance.Fade(0.75f, 1));

        var fat = Stat.instance.prop.fatigue;

        for (int i = 0; i < fat; i++) {
            var prop = Stat.instance.prop;
            prop.fatigue -= 1;
            Stat.instance.prop = prop;

            Day.instance.date.AddDate(0, 0, 3);

            yield return new WaitForSecondsRealtime(3f / fat);
        }

        SaveLoadUtility.Save();

        yield return n.StartCoroutine(FadeInOut.instance.Fade(0.75f, 0));

        Time.timeScale = 1;
        Move.instance.movable = true;
    }
}

public class bed_NoSleep : Choosable
{
    public bed_NoSleep(NPC n) : base(n, KeyedText.keyedText("Home", 3))
    {
    }

    public override IEnumerator Choose()
    {
        TextManager.instance.isChoosing = false;

        Move.instance.movable = false;

        yield return null;
        yield return null;

        n.PlayerThink(KeyedText.keyedText("Home", 4));
        yield return new WaitUntil(() =>
            TextManager.instance.isSpeaking == false
        );

        Move.instance.movable = true;
    }
}
#endregion

public class bed : NPC
{
    // Start is called before the first frame update
    public override IEnumerator ScriptRunnerW()
    {
        Move.instance.movable = false;
        PlayerThink(KeyedText.keyedText("Home", 1), new bed_Sleep(this), new bed_NoSleep(this));
        yield return new WaitUntil(() => 
            TextManager.instance.isSpeaking == false
        );
        Move.instance.movable = true;
    }
}
