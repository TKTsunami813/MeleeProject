using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    public static PlayerStateController Instance;

    [SerializeField] private int _damageForSlash;
    [SerializeField] private Animator[] _playerAnimator;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Transform _targetTransform;
    private Vector2 _startPosition;

    public PlayerIdleState IdleState { get; private set ; }
    public PlayerAttackState AttackState { get; private set; }
    public PlayerBase CurrentState { get; private set; }

    private void Awake()
    {
        Instance = this;
        _startPosition = transform.position;

        IdleState = new PlayerIdleState(this);
        AttackState = new PlayerAttackState(this, _damageForSlash, _playerAnimator, _moveSpeed, _targetTransform, _startPosition);
    }

    private void Start()
    {        
        TransitionToState(IdleState);
    }

    public void TransitionToState(PlayerBase playerState)
    {
        if (CurrentState != null)
            CurrentState.Exit();
        CurrentState = playerState;
        CurrentState.Entry();
    }


    private void Update()
    {
        if(CurrentState != null)
            CurrentState.Update();
    }

    public void StartingCoroutine(IEnumerator currentCoroutine)
    {
        StartCoroutine(currentCoroutine);
    }
}
