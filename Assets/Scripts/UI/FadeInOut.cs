using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    public static FadeInOut instance
    {
        get; private set;
    }
    public Image im;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public IEnumerator Fade(float s, float a) {
        var iC = im.color.a;

        for (int i = 0; i < 257; i++) {
            im.color = new Color(0, 0, 0, Mathf.Lerp(iC, a, i / 256f));
            yield return new WaitForSecondsRealtime(s / 256f);
        }
    }
}
