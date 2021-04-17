using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIFighting : MonoBehaviour
{
    public static GUIFighting Instance;

    [SerializeField] private GameObject _arrows;
    [SerializeField] private GameObject _actionButton;
    [SerializeField] private GameObject _slashAttackButton;
    [SerializeField] private Animator _effectAnimator;
    [SerializeField] private Text _textDamage;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There should be only one instance GUIFightingThisEnemy!");
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && PlayerStateController.Instance.CurrentState == PlayerStateController.Instance.IdleState)
            SelectAction();

        // Player can only attack if the _slashAttackButton is active and the right key get down.
        // The selection menu will be deactivated
        if (Input.GetKeyDown(KeyCode.Space) && _slashAttackButton.activeSelf)
        {            
            DeactivateAllSelection();
            PlayerStateController.Instance.TransitionToState(PlayerStateController.Instance.AttackState);
        }

        // The arrows can only be deactivated if the arrows are active and the right key get down.
        if (_arrows.activeSelf && Input.GetKeyDown(KeyCode.Escape))
            DeactivateSelection();
    }

    // Will activate the selecting GUI and checks which should be active now.
    public void SelectAction()
    {
        if(!_arrows.activeSelf)
        {
            _arrows.SetActive(true);
        }
        else if(_arrows.activeSelf && !_actionButton.activeSelf && !_slashAttackButton.activeSelf)
        {
            _actionButton.SetActive(true);
        }
        else if(_arrows.activeSelf && _actionButton.activeSelf)
        {
            _slashAttackButton.SetActive(true);
            _actionButton.SetActive(false);
        }
    }

    // Will deactivate the selecting GUI and checks which should be deactive now.
    public void DeactivateSelection()
    {
        if (_arrows.activeSelf && !_actionButton.activeSelf && !_slashAttackButton.activeSelf)
        {
            _arrows.SetActive(false);
        }
        else if (_actionButton.activeSelf)
        {
            _actionButton.SetActive(false);
        }
        else if (_slashAttackButton.activeSelf)
        {
            _slashAttackButton.SetActive(false);
            if(!_actionButton.activeSelf)
                _actionButton.SetActive(true);
        }
    }

    // Will deactivate all GUI of the selection menu.
    private void DeactivateAllSelection()
    {
        _arrows.SetActive(false);
        _slashAttackButton.SetActive(false);
        _actionButton.SetActive(false);
    }

    // Activate the slash effect animation.
    public void ShowEffect()
    {
        _effectAnimator.SetTrigger("isAttacked");
    }

    // Text shows the right damage points and the animation for the damage text will be triggered.
    public void ShowDamage(int damage)
    {
        _textDamage.text = damage.ToString();
        _textDamage.GetComponent<Animator>().SetTrigger("isAttacked");
    }
}
