//Code adapted from:
//Zyger - https://www.youtube.com/watch?v=aNZw588BQBo

using System.Collections;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    public bool CanAttack = true;
    public float AttackCooldown = 1.0f;
    public bool isAttacking = false;
    public Animator anim;
    public AudioClip swordAttackSound;
    private CollisionDetection colli;

    private void Start()
    {
        colli = gameObject.GetComponent<CollisionDetection>();
    }

    void Update()
    {        
        if (Input.GetMouseButtonDown(0))
        {
            if (CanAttack && PauseMenu.isPaused == false)
            {
                SwordAttack();
            }
        }
    }

    public void SwordAttack()
    {
        isAttacking = true;
        CanAttack = false;
        anim.SetTrigger("attacking");
        AudioSource ac = GetComponent<AudioSource>();
        ac.PlayOneShot(swordAttackSound);
        
        StartCoroutine(ResetAttackCooldown());
    }

    IEnumerator ResetAttackCooldown()
    {
        StartCoroutine(ResetAttackBool());
        yield return new WaitForSeconds(AttackCooldown);
        CanAttack = true;
        colli.hasAttacked = false;
    }

    IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(AttackCooldown);
        isAttacking = false;
    }
}
