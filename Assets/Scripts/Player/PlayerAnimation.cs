using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    private Character character;
    private PlayController playController;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        character = GetComponent<Character>();
        playController = GetComponent<PlayController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetAnimation();
    }

    private void SetAnimation()
    {
        animator.SetFloat("veloctiyX", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("velocityY", rb.velocity.y);
        animator.SetBool("isGround", physicsCheck.isGround);
        animator.SetBool("isDead", playController.isDead);
        animator.SetBool("isAttack", playController.isAttack);
    }

    public void PlayerHurt()
    {
        animator.SetTrigger("hurt");
    }

    public void PlayerAttack()
    {
        animator.SetTrigger("attack");
    }
}
