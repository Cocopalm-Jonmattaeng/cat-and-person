using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteract : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !TextManager.instance.isSpeaking && !PlayerUI.instance.menuOpened) {
            RaycastHit2D hit = Physics2D.Raycast((new Vector2(transform.localPosition.x, transform.localPosition.y - 0.25f) + Move.instance.facing * 0.255f) + Move.instance.facing * 0.275f, Vector2.zero);

            if (hit.collider == null) return;

            GameObject target = hit.collider.gameObject;

            if (target != null && target.TryGetComponent<NPC>(out NPC cNpc))
            {
                StartCoroutine(MoveController(cNpc));
            }
        }
    }


    private IEnumerator MoveController(NPC npc) {
        yield return StartCoroutine(npc.ScriptRunnerW());
    }
}
