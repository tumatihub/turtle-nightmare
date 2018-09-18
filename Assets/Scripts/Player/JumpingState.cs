using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/JumpingState")]
public class JumpingState : State
{
    [SerializeField] bool _isPressingJump;
    [SerializeField] float _jumpTimeCounter;
    [SerializeField] float _yv;
    [SerializeField] bool _canJump = true;
    [SerializeField] bool _jumping;
    Collider2D _result;

    public override void handle_input(PlayerStateController controller)
    {
        controller.moveInput = Input.GetAxisRaw("Horizontal");
        controller.Flip();
        if (_jumping)
        {
            if (Input.GetButton("Jump"))
            {
                _isPressingJump = true;
            }
            else
            {
                _canJump = false;
                _isPressingJump = false;
            }

            if (_jumpTimeCounter > 0)
            {
                _jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                _canJump = false;
            }

            
        }
        

        // Decision to MovingState
        if (controller.isGrounded)
        {
            //_isPressingJump = false;
            //_canJump = true;
            //_jumping = false;
            
            controller.ChangeState(controller.movingState);
        }

    }

    public override void update(PlayerStateController controller)
    {
        
        if (_jumping)
        {
            // Fixed
            if (_isPressingJump && _canJump)
            {
                _yv = controller.jumpForce;
            }
            else
            {
                if (controller.rb.velocity.y > 0)
                {
                    _yv = controller.rb.velocity.y / 2;
                }
                else
                {
                    _yv = controller.rb.velocity.y;
                }

            }
            controller.rb.velocity = new Vector2(controller.moveInput * controller.speed, _yv);
            
        }
        
    }

    public override void onEnter(PlayerStateController controller)
    {
        controller.isGrounded = false;
        controller.rb.velocity = Vector2.up * controller.jumpForce;
        _isPressingJump = true;
        _jumpTimeCounter = controller.jumpTime;
        _jumping = true;
        controller.audioSource.PlayOneShot(controller.jumpSound);
    }

    public override void onExit(PlayerStateController controller)
    {
        _isPressingJump = false;
        _canJump = true;
        _jumping = false;
    }
}
