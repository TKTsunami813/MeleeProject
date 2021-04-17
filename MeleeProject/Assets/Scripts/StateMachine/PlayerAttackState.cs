using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBase
{
    private int _damageForSlash;
    private Animator[] _playerAnimator;
    private float _moveSpeed;
    private Transform _targetTransform;
    private Vector2 _startPosition;
    private Transform _transform;

    public PlayerAttackState(PlayerStateController playerStateController, int damageForSlash, Animator[] playerAnimator, float moveSpeed, Transform targetTransform, Vector2 startPosition) : base(playerStateController)
    {
        _transform = _playerStateController.transform;
        _damageForSlash = damageForSlash;
        _playerAnimator = playerAnimator;
        _moveSpeed = moveSpeed;
        _targetTransform = targetTransform;
        _startPosition = startPosition;
    }

    public override void Entry()
    {
        JumpTowardsEnemy();
    }

    public void JumpTowardsEnemy()
    {
        _playerStateController.StartCoroutine(JumpingTowards());
    }

    // Coroutine where the player moves to his target position before the enemy
    public IEnumerator JumpingTowards()
    {
        // The animator of the player goes into the jump animation.
        for (int i = 0; i < _playerAnimator.Length; i++)
        {
            _playerAnimator[i].SetBool("isJumpingToEnemy", true);
        }

        while (Vector2.Distance(_transform.position, _targetTransform.position) >= 0.1f)
        {
            _transform.position = Vector2.MoveTowards(_transform.position, _targetTransform.position, _moveSpeed * Time.deltaTime);
            yield return null;
        }

        // After the player is in his attack position, jump animation will be deactivated
        for (int i = 0; i < _playerAnimator.Length; i++)
        {
            _playerAnimator[i].SetBool("isJumpingToEnemy", false);
        }

        // Wait for a second before the attack starts
        yield return new WaitForSeconds(1f);
        Attack();
    }

    // Animator goes to the attack animation and two effects (slash effect and text effect) will be called from the fighting script
    private void Attack()
    {
        for (int i = 0; i < _playerAnimator.Length; i++)
        {
            _playerAnimator[i].SetTrigger("isAttacking");
        }

        GUIFighting.Instance.ShowEffect();
        GUIFighting.Instance.ShowDamage(_damageForSlash);
    }

    // This function will be called after the animation for the attack is finished. It starts a coroutine to move the player back to his startposition
    public void MoveBack()
    {
        _playerStateController.StartCoroutine(JumpBack(1f));
    }

    // Coroutine to move the player back to his startposition 
    public IEnumerator JumpBack(float delay)
    {
        // Wait before the player jumps back
        yield return new WaitForSeconds(delay);


        // The animator of the player goes into the jump animation.
        for (int i = 0; i < _playerAnimator.Length; i++)
        {
            _playerAnimator[i].SetBool("isJumpingToEnemy", true);
        }

        while (Vector2.Distance(_transform.position, _startPosition) >= 0.1f)
        {
            _transform.position = Vector2.MoveTowards(_transform.position, _startPosition, _moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Make sure the position is really the start position
        _transform.position = _startPosition;

        // The animator of the player deactivates the jump animation.
        for (int i = 0; i < _playerAnimator.Length; i++)
        {
            _playerAnimator[i].SetBool("isJumpingToEnemy", false);
        }

        // The attack is now finished
        _playerStateController.TransitionToState(_playerStateController.IdleState);
    }
}
