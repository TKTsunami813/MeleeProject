using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEnd : MonoBehaviour
{
    // Function who will be called by an animation effent. This function calls the MoveBack function from the player, so the player knows when he has to jump back
    public void AttackAnimationEnd()
    {
        if(PlayerStateController.Instance.CurrentState == PlayerStateController.Instance.AttackState)
            PlayerStateController.Instance.AttackState.MoveBack();
    }
}
