using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/DeadState")]
public class DeadState : State {
    public override void handle_input(PlayerStateController controller)
    {
        
    }

    public override void update(PlayerStateController controller)
    {
        

    }

    public override void onEnter(PlayerStateController controller)
    {
        
        controller.hookLine.enabled = false;
        controller.hook.parent = controller.transform;
        controller.isRetracting = false;
        controller.hook.position = controller.transform.position;
        controller.rb.isKinematic = true;
        controller.rb.velocity = Vector3.zero;
        controller.animator.SetBool("Dead", true);
        controller.animator.SetTrigger("DeathTrigger");
        
    }
}
