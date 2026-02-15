using DenkKits.UIManager.Scripts.UIAnimation;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private Animator animator;
    private bool _allowMove = false;
    private string _currentAnim = "idle";

    private void Start()
    {
        if(animator)
        {
            animator = GetComponent<Animator>();
        }
    }

    private void Update()
    {
        if(_allowMove)
        {
            Vector2 dir = joystick.Direction;

        }
    }
    private void SetAnimation(string name)
    {
        if (name == _currentAnim) return;
        animator.ResetTrigger(_currentAnim);
        _currentAnim = name;
        animator.SetTrigger(_currentAnim);

    }
}
