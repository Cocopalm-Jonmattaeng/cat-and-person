using System.Collections;
using UnityEngine;

#region Choosables
public class fridge_Take : Choosable
{
    public fridge_Take(NPC n) : base(n, KeyedText.keyedText("Home", 9))
    {
    }

    public override IEnumerator Choose()
    {
        TextManager.instance.isChoosing = false;

        Move.instance.movable = false;

        yield return null;
        yield return null;

        Inventory.AddItem(new Inventory.Item(2, 1));
        n.PlayerThink(KeyedText.keyedText("ItemTakeText", 2));
        yield return new WaitUntil(() =>
            TextManager.instance.isSpeaking == false
        );

        Move.instance.movable = true;
    }
}

public class fridge_NoTake : Choosable
{
    public fridge_NoTake(NPC n) : base(n, KeyedText.keyedText("Home", 10))
    {
    }

    public override IEnumerator Choose()
    {
        TextManager.instance.isChoosing = false;

        Move.instance.movable = false;

        yield return null;
        yield return null;

        n.PlayerThink(KeyedText.keyedText("Home", 11));
        yield return new WaitUntil(() =>
            TextManager.instance.isSpeaking == false
        );

        Move.instance.movable = true;
    }
}
#endregion

public class fridge : NPC
{
    // Start is called before the first frame update
    public override IEnumerator ScriptRunnerW()
    {
        Move.instance.movable = false;
        PlayerThink(KeyedText.keyedText("Home", 8), new fridge_Take(this), new fridge_NoTake(this));
        yield return new WaitUntil(() =>
            TextManager.instance.isSpeaking == false
        );
        Move.instance.movable = true;
    }
}