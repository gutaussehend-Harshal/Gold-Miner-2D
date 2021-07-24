using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookScript : MonoBehaviour
{
    [SerializeField] private Transform itemHolder;
    private bool itemAttached;
    private HookController hookController;
    private PlayerAnimation playerAnim;
    void Awake()
    {
        hookController = GetComponentInParent<HookController>();
        playerAnim = GetComponentInParent<PlayerAnimation>();
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.CompareTag("SmallGold") || target.gameObject.CompareTag("MiddleGold") || target.gameObject.CompareTag("LargeGold") ||
        target.gameObject.CompareTag("LargeStone") || target.gameObject.CompareTag("MiddleStone"))
        {
            itemAttached = true;
            target.transform.parent = itemHolder;
            target.transform.position = itemHolder.position;
            hookController.move_Speed = target.GetComponent<ItemScript>().hook_Speed;
            hookController.HookAttachedItem();

            // animate player
            playerAnim.PullingItemAnimation();

            if (target.gameObject.CompareTag("SmallGold") || target.gameObject.CompareTag("MiddleGold") ||
                target.gameObject.CompareTag("LargeGold"))
            {
                SoundManager.instance.HookGrab_Gold();
            }
            else if (target.gameObject.CompareTag("LargeStone") || target.gameObject.CompareTag("MiddleStone"))
            {
                SoundManager.instance.HookGrab_Stone();
            }
            SoundManager.instance.PullSound(true);
        }

        if (target.gameObject.CompareTag("DeliverItem"))
        {
            if (itemAttached)
            {
                itemAttached = false;
                Transform objChild = itemHolder.GetChild(0);
                objChild.parent = null;
                objChild.gameObject.SetActive(false);

                // animate player
                playerAnim.IdleAnimation();

                SoundManager.instance.PullSound(false);
            }
        }
    }
}
