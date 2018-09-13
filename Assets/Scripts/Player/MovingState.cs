using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/MovingState")]
public class MovingState : State
{
    public override void handle_input(PlayerStateController controller)
    {
        controller.moveInput = Input.GetAxisRaw("Horizontal");
        controller.Flip();
            // Decision to jump
            if (controller.isGrounded && Input.GetButtonDown("Jump"))
            {
                controller.ChangeState(controller.jumpingState);
            }

            // Decision to shoot
            if (controller.isGrounded && Input.GetButtonDown("Fire1"))
            {
                controller.ChangeState(controller.shootingState);
            }

            // Decision to hide
            if (controller.isInShadow && Input.GetButtonDown("Fire2"))
            {
                Debug.Log("Hide");
                controller.ChangeState(controller.hidingState);
            }        
    }

    public override void update(PlayerStateController controller)
    {
  
            controller.rb.velocity = new Vector2(controller.moveInput * controller.speed, controller.rb.velocity.y);
        
    }

    public override void onEnter(PlayerStateController controller)
    {
        controller.hookLine.enabled = false;
        controller.hook.parent = controller.transform;
        controller.isRetracting = false;
        controller.hook.position = controller.transform.position;
        controller.rb.isKinematic = false;
    }
}
