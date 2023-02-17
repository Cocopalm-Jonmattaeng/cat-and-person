using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct KeyedText { 
    public string nameSpace;
    public int textId;

    public readonly string text {
        get { 
            return TextManager.GetText(this);
        }
    }

    public static KeyedText keyedText(string nameSpace, int textId)
    {
        return new KeyedText
        {
            nameSpace = nameSpace,
            textId = textId
        };
    }
}

public class TextManager : MonoBehaviour
{
    public GameObject textObj;
    public LangTexts textAssets = new LangTexts();

    public GameObject currentTalkBalloon;

    public static TextManager instance
    {
        get; private set;
    }

    private Dictionary<string, string[]> texts = new Dictionary<string, string[]>();

    public bool isSpeaking = false;
    public bool isChoosing = false;

    private KeyCode[] keyCodes = {
            KeyCode.Alpha1,
            KeyCode.Alpha2,
            KeyCode.Alpha3,
            KeyCode.Alpha4,
            KeyCode.Alpha5,
            KeyCode.Alpha6,
            KeyCode.Alpha7,
            KeyCode.Alpha8,
            KeyCode.Alpha9,
        };

    public List<Sprite> balloons;

    internal Choosable[] cch;

    [System.Serializable]
    public class LangTexts : CustomDic.SerializableDictionary<string, TAssetArray>
    {

    }

    [System.Serializable]
    public class TAssetArray : CustomDic.SerializableTextAsset
    { 
    }

    private void Start()
    {
        instance = this;

        foreach (var i in textAssets["ko"]) {
            var values = i.text.Trim().Split('\n');
            var vCopy = new string[values.Length - 1];

            Array.Copy(values, 1, vCopy, 0, values.Length - 1);

            texts[values[0].Trim()] = vCopy;
        }
    }

    private void Update()
    {
        if (!isChoosing) return;

        for (int i = 0; i < keyCodes.Length; i++)
        {
            if (Input.GetKeyDown(keyCodes[i]))
            {
                StartCoroutine(cch[i].Choose());
            }
        }
    }

    private IEnumerator TBalloonTracer() {
        while (currentTalkBalloon != null) {
            currentTalkBalloon.transform.localPosition = Move.instance.transform.localPosition;
            yield return null;
        }
    }

    public GameObject GetOrCreateTalkBalloon() {
        if (currentTalkBalloon == null) { 
            currentTalkBalloon = Instantiate(textObj);
        }

        return currentTalkBalloon;
    }

    public static string GetText(KeyedText text) {
        return instance.texts[text.nameSpace][text.textId - 1].Trim().Replace("&", "\n");
    }

    private IEnumerator Destroyer(bool chosed) {
        if(!chosed) yield return new WaitForSeconds(1.5f);
        Destroy(instance.currentTalkBalloon);
        StopCoroutine(TBalloonTracer());
    }

    public static IEnumerator Text(KeyedText text, NPC npc, bool think, bool player, AudioSource ass, params Choosable[] choosables) {
        if (!instance.isSpeaking) {
            instance.isSpeaking = true;
            instance.isChoosing = false;
            instance.StopCoroutine("Destroyer");

            var wText = text.text;

            var lP = npc != null ? npc.transform.localPosition : Vector3.zero;

            var obj = instance.GetOrCreateTalkBalloon();
            if (!player) obj.transform.localPosition = lP * 0.5f;
            obj.GetComponent<Canvas>().worldCamera = Camera.main;

            if (player) instance.StartCoroutine(instance.TBalloonTracer());

            obj.GetComponentInChildren<Image>().sprite = instance.balloons[think ? 1 : 0];

            obj.GetComponentInChildren<Text>().text = npc != null ? $"<size=30><color=#3D5AFE>{npc.npcName.text}</color></size>\n" : "";

            var wTextCache = npc != null ? $"<size=30><color=#3D5AFE>{npc.npcName.text}</color></size>\n" : "";

            var boldNow = false;

            foreach (var wChar in wText.ToCharArray())
            {
                if (wChar == '`') { 
                    boldNow = !boldNow; 
                    continue;
                }

                lP = npc != null ? npc.transform.localPosition : Move.instance.transform.localPosition;

                obj = instance.GetOrCreateTalkBalloon();
                if(!player) obj.transform.localPosition = lP * 0.5f;
                obj.GetComponent<Canvas>().worldCamera = Camera.main;

                var wCharPostprocessed = boldNow ? $"<b>{wChar}</b>" : wChar.ToString();

                wTextCache += wCharPostprocessed;
                obj.GetComponentInChildren<Text>().text = wTextCache;
                if (!char.IsWhiteSpace(wChar) && ass != null) {
                    ass.Stop();
                    ass.Play();
                }
                yield return new WaitForSeconds(char.IsWhiteSpace(wChar) ? 0.0166667f : 0.075f);
            }

            yield return new WaitForSeconds(0.5f);

            if (choosables.Length != 0) { instance.isChoosing = true; obj.GetComponentInChildren<Text>().text += "\n"; }

            var ind = 1;
            instance.cch = choosables;

            foreach (var cb in choosables) {
                obj.GetComponentInChildren<Text>().text += $"<size=17>\n<color=#304FFE>Num{ind++}</color> {cb.text}</size>";
                yield return new WaitForSeconds(0.333f);
            }

            yield return new WaitUntil(() => !instance.isChoosing);

            yield return instance.StartCoroutine(instance.Destroyer((choosables.Length != 0)));
            instance.isSpeaking = false;
        }
    }
}
