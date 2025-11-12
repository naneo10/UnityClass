using UnityEngine;

public class SkillEffect : MonoBehaviour
{
    public Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void FireballEffect()
    {
        anim.SetTrigger("Fireball");
    }

    public void IceSpearEffect()
    {
        anim.SetTrigger("IceSpear");
    }
}
