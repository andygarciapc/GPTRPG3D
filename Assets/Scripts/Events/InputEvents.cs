using System;
using UnityEngine;
namespace Basic.Events
{
    public class InputEvents
    {
        public event Action<Vector2> onMovePressed;
        public void MovePressed(Vector2 moveDir)
        {
            if (onMovePressed != null)
            {
                onMovePressed(moveDir);
            }
        }
        public event Action onJumpPressed;
        public void JumpPressed()
        {
            if (onJumpPressed != null)
            {
                onJumpPressed();
            }
        }
        public event Action onAttackPressed;
        public void AttackPressed()
        {
            if(onAttackPressed != null)
            {
                onAttackPressed();
            }
        }
        public event Action onEvadePressed;
        public void EvadePressed()
        {
            if (onEvadePressed != null)
            {
                onEvadePressed();
            }
        }
        public event Action onSheathePressed;
        public void SheathePressed()
        {
            if (onSheathePressed != null)
            {
                onSheathePressed();
            }
        }
        public event Action onInteractPressed;
        public void InteractPressed()
        {
            if (onInteractPressed != null)
            {
                onInteractPressed();
            }
        }
        public event Action onEscapePressed;
        public void EscapePressed()
        {
            if (onEscapePressed != null)
            {
                onEscapePressed();
            }
        }
        public event Action onSubmitPressed;
        public void SubmitPressed()
        {
            if (onSubmitPressed != null)
            {
                onSubmitPressed();
            }
        }
    }
}