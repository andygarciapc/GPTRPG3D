using System;
using UnityEngine;
namespace Basic.Events
{
    public class InputEvent
    {
        public event Action<Vector2> onMovePressed;
        public void MovePressed(Vector2 moveDir)
        {
            if (onMovePressed != null)
            {
                onMovePressed(moveDir);
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
        public event Action onQuestLogTogglePressed;
        public void QuestLogTogglePressed()
        {
            if (onQuestLogTogglePressed != null)
            {
                onQuestLogTogglePressed();
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
        public event Action onInteractPressed;
        public void InteractPressed()
        {
            if (onInteractPressed != null)
            {
                onInteractPressed();
            }
        }
    }
}