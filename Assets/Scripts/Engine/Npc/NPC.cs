using System.Collections;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public abstract class NPC : MonoBehaviour
{
    public KeyedText npcName;

    public NPC instance
    {
        get; private set;
    }

    protected bool isMoving = false;

    public AudioSource ass;

    private void Start()
    {
        instance = this;
    }

    public IEnumerator MoveSec(Vector3 pos, float sec) {
        if (!isMoving) {
            isMoving = true;

            Vector3 oPos = transform.localPosition;
            float estTime = 0f;

            while (estTime <= sec + 0.01f)
            {
                Debug.Log(estTime / sec);

                transform.localPosition = (Vector3.Lerp(oPos, pos, estTime / sec));

                estTime += 0.0166667f;
                yield return new WaitForSeconds(0.0166667f);
            }

            isMoving = false;
        }
    }

    public void Speak(KeyedText text, params Choosable[] ch) 
    {
        StartCoroutine(TextManager.Text(text, this, false, false, ass, ch));
    }

    public void PlayerSpeak(KeyedText text, params Choosable[] ch)
    {
        StartCoroutine(TextManager.Text(text, null, false, true, null, ch));
    }

    public void Think(KeyedText text, params Choosable[] ch)
    {
        StartCoroutine(TextManager.Text(text, this, true, false, ass, ch));
    }

    public void PlayerThink(KeyedText text, params Choosable[] ch)
    {
        StartCoroutine(TextManager.Text(text, null, true, true, null, ch));
    }

    public abstract IEnumerator ScriptRunnerW();
}
