using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_ : MonoBehaviour
{
    /*基本定義*/
    private Rigidbody2D rbody;
    private float axisH = 0.0f;
    public float speed = 5.0f;
    public float jumpPw = 5.0f;
    private bool goJump = false;
    public LayerMask groundLayer;
    private SpriteRenderer spriteRenderer;
    private bool flipX;
    private bool flipTurnX;

    private bool isMoving; 
    /*ここまで基本定義*/

    void Start()
    {
        /*基本初期化*/
        isMoving = false;
        rbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        flipX = priteRenderer.flipX;
        flipTurnX = !flipX;
        /*ここまで基本初期化*/
    }

    void Update()
    {
        //移動中でない時のみ、キーによる方向入力を受け付ける
        if(isMoving==false){
            axisH = Input.GetAxisRaw("Horizontal");
        }
        Move();
        
        if (Input.GetButton("Jump"))
        {
            JumpOn();
        }
    }

    private void FixedUpdate()
    {
        if (goJump)
        {
            Jump();
        }
    }
    /*地面判定関数*/
    public bool onGroundCheck()
    {
        return Physics2D.CircleCast(transform.position, 0.2f, Vector2.down, 0.0f, groundLayer);
    }
    /*ここまで地面判定関数*/

    /*歩く関数*/
    public void Move()
    {
        if (axisH > 0)
        {
            spriteRenderer.flipX = flipX;
        }
        else if (axisH < 0)
        {
            spriteRenderer.flipX = flipTurnX;
        }

        if (onGroundCheck() || axisH != 0)
        {
            rbody.velocity = new Vector2(speed * axisH, rbody.velocity.y);
        }
    }
    /*ここまで歩く関数*/

    /*ジャンプフラグ管理*/
    public void JumpOn()
    {
        if (onGroundCheck())
        {
            goJump = true;
        }
    }
    /*ここまでジャンプフラグ管理*/

    /*ジャンプを実行する関数*/
    public void Jump()
    {
        Vector2 jumpVec = new Vector2(0, jumpPw);
        rbody.velocity = new Vector2(rbody.velocity.x, 0);
        rbody.AddForce(jumpVec, ForceMode2D.Impulse);
        goJump = false;
    }
    /*ジャンプ実行関数*/

    /*外部からの移動入力用*/
    public void MoveRight(){
        isMoving = true;
        axisH = 1.0f;
    }
    public void MoveLeft(){
        isMoving = true;
        axisH = -1.0f;
    }
    public void MoveStop(){
        isMoving = false;
        axisH = 0;
    }


    /*ここまで外部からの移動入力用*/


}
