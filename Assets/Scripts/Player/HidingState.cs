using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/HidingState")]
public class HidingState : State
{

    public override void handle_input(PlayerStateController controller)
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Debug.Log("Move");
            controller.ChangeState(controller.movingState);
        }
    }

    public override void update(PlayerStateController controller)
    {
    }

    public override void onEnter(PlayerStateController controller)
    {
        controller.isHidden = true;
        controller.gameObject.layer = LayerMask.NameToLayer("Hidden");
        controller.turtleSprite.sortingLayerName = "Hidden";
        controller.transform.position = new Vector2(
            controller.wallHole.position.x, 
            controller.transform.position.y
        );
        controller.animator.SetBool("Hide", true);
        controller.rb.velocity = Vector2.zero;
    }

    public override void onExit(PlayerStateController controller)
    {
        controller.animator.SetBool("Hide", false);
        controller.isHidden = false;
        controller.turtleSprite.sortingLayerName = "Player";
        controller.gameObject.layer = LayerMask.NameToLayer("Player");
    }
}
