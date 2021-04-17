using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBase
{
    protected PlayerStateController _playerStateController;

    public PlayerBase(PlayerStateController playerStateController)
    {
        _playerStateController = playerStateController;
    }

    public abstract void Entry();

    public virtual void Update()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual IEnumerator WaitingCoroutine(int waitTime)
    {
        yield return null;
    }
}
