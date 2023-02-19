using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomNameAnimation : MonoBehaviour
{
    public Text rNT;

    // Start is called before the first frame update
    void Start()
    {
        rNT = GetComponentInChildren<Text>();
        transform.localPosition = new Vector2(-(Screen.width / 2), -(Screen.height / 2) + 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Animate(string name) {
        rNT.text = name;
        transform.localPosition = new Vector2(-(Screen.width / 2), -(Screen.height / 2) + 10);
        StopAllCoroutines();

        StartCoroutine(AnimateInternal());
    }

    private IEnumerator AnimateInternal() {
        while (transform.localPosition.x < (-(Screen.width / 2) + 350f)) {
            transform.Translate(Vector2.right * 15f);
            yield return new WaitForSecondsRealtime(0.02f);
        }
        yield return new WaitForSecondsRealtime(1.75f);
        while (transform.localPosition.x > -(Screen.width / 2))
        {
            transform.Translate(Vector2.left * 15f);
            yield return new WaitForSecondsRealtime(0.02f);
        }
    }
}
