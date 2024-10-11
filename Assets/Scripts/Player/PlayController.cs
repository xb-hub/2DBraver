using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class PlayController : MonoBehaviour
{
    public PlayerInputControls playInputControls;
    public Vector2 inputDirection;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public PhysicsCheck physicsCheck;
    public PlayerAnimation playerAnimation;
    [Header("移动参数")]
    public float speed;
    public float thrust;

    [Header("碰撞参数")]
    public bool isHurt;
    public float hurtForce;

    public bool isDead;

    public bool isAttack;

    private void Awake()
    {
        playInputControls = new PlayerInputControls();
        // 获取SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();
        // 获取刚体
        //rb = GetComponent<Rigidbody2D>();
        playInputControls.Gameplay.Jump.started += Jump;
        // 获取脚本
        physicsCheck = GetComponent<PhysicsCheck>();
        playerAnimation = GetComponent<PlayerAnimation>();

        playInputControls.Gameplay.Attack.started += PlayerAttack;
    }

    private void PlayerAttack(InputAction.CallbackContext context)
    {
        playerAnimation.PlayerAttack();
        isAttack = true;
    }

    private void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump");
        if (physicsCheck.isGround)
        {
            rb.AddForce(transform.up * thrust, ForceMode2D.Impulse);
        }
    }

    private void OnEnable()
    {
        playInputControls.Enable();
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        inputDirection = playInputControls.Gameplay.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        //if (inputDirection.x < 0)
        //{
        //    spriteRenderer.flipX = true;
        //}
        //else
        //{
        //    spriteRenderer.flipX = false;
        //}
        if (!isHurt)
        {
            Move();
        }
    }

    public void Move()
    {
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);
        //if (inputDirection.x < 0)
        //{
        //    spriteRenderer.flipX = true;
        //}
        //else
        //{
        //    spriteRenderer.flipX = false;
        //}
        int faceDir = (int)Math.Floor(transform.localScale.x);
        if (inputDirection.x < 0)
        {
            faceDir = -1;
        }
        else
        {
            faceDir = 1;
        }
        transform.localScale = new Vector3(faceDir, 1, 1);
    }

    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        rb.velocity = Vector2.zero;
        Vector2 direct = new Vector2(rb.position.x - attacker.position.x, 0).normalized;
        rb.AddForce(direct * hurtForce, ForceMode2D.Impulse);
    }

    public void PlayerDead()
    {
        isDead = true;
        playInputControls.Gameplay.Disable();
    }

    private void OnDisable()
    {
        playInputControls.Disable();
    }

}
