using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void IdleAnimation()
    {
        anim.Play(AnimationTags.Idle_Animation);
        // anim.Play("Idle");
    }
    public void PullingItemAnimation()
    {
        anim.Play(AnimationTags.Rope_Wrap_Animation);
        // anim.Play("Rope_Wrap");
    }
    public void ChearAnimation()
    {
        anim.Play(AnimationTags.Chear_Animation);
        // anim.Play("Chear");
    }
}
