using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Move : MonoBehaviour
{
    public static Move instance
    {
        get; private set;
    }

    private PlayerUI pui;

    public bool movable = true;

    public Vector2 facing = new Vector2(0, -1);

    public Sprite[] animSprites; // 0 left 4 right 8 up 12 down
    public Sprite[] idleSprites; // 0 left 1 right 2 up 3 down

    private bool moving = false;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        pui = GetComponent<PlayerUI>();

        StartCoroutine(Animator());
    }

    // Update is called once per frame
    void Update()
    {
        var movVec = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Horizontal") == 0 ? Input.GetAxisRaw("Vertical") : 0);

        var fSpd = ((Stat.instance.prop.speed * 0.01f) + 0.5f) * 2f * Time.deltaTime * movVec;

        if (!pui.menuOpened && movable) transform.Translate(fSpd);
        
        if (!pui.menuOpened && movable && (Math.Sign(Input.GetAxisRaw("Horizontal")) != 0 || Math.Sign(Input.GetAxisRaw("Vertical")) != 0)) {
            facing = new Vector2(
                Math.Sign(Input.GetAxisRaw("Horizontal")),
                Math.Sign(Input.GetAxisRaw("Horizontal")) == 0 ? Math.Sign(Input.GetAxisRaw("Vertical")) : 0
            );
        }

        moving = movVec.normalized != Vector2.zero;
    }

    IEnumerator Animator() {
        var _fTemp = Vector2.zero;

        while (true) {
            Sprite tSpr = null;

            if (moving)
            {
                if (facing.x >= 0.5f || facing.x <= -0.5f)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (!moving || _fTemp.normalized != facing.normalized) break;

                        GetComponent<SpriteRenderer>().sprite = animSprites[(facing.x >= 0.5f ? 1 : 0) * 4 + i];

                        yield return new WaitForSeconds(0.125f);
                    }
                }
                else if (facing.y >= 0.5f || facing.y <= -0.5f)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (!moving || _fTemp.normalized != facing.normalized) break;

                        GetComponent<SpriteRenderer>().sprite = animSprites[(facing.y >= 0.5f ? 2 : 3) * 4 + i];

                        yield return new WaitForSeconds(0.125f);
                    }
                }
            }
            else
            {
                if (facing.x >= 0.5f || facing.x <= -0.5f)
                {
                    tSpr = idleSprites[facing.x >= 0.5f ? 1 : 0];
                }
                else if (facing.y >= 0.5f || facing.y <= -0.5f)
                {
                    tSpr = idleSprites[facing.y >= 0.5f ? 2 : 3];
                }
            }

            if (tSpr != null) {
                if (GetComponent<SpriteRenderer>().sprite != tSpr)
                {
                    GetComponent<SpriteRenderer>().sprite = tSpr;
                }
            }

            yield return new WaitForSeconds(0.0333333333f);

            _fTemp = facing.normalized;
        }
    }
}
