using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private bool _north;
    [SerializeField] private bool _east;
    [SerializeField] private bool _south;
    [SerializeField] private bool _west;
    private Animator _arrowAnimator;

    // If the object will be activated it checks which arrow it is and activate the right animation
    private void OnEnable()
    {
        _arrowAnimator = GetComponent<Animator>();

        if (_north)
            _arrowAnimator.SetTrigger("isNorth");
        else if(_east)
            _arrowAnimator.SetTrigger("isEast");
        else if (_south)
            _arrowAnimator.SetTrigger("isSouth");
        else
            _arrowAnimator.SetTrigger("isWest");
    }
}
