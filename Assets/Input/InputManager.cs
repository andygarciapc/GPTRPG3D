using UnityEngine;
using UnityEngine.InputSystem;
using Basic.Events;

namespace Basic.Input
{
	[RequireComponent(typeof(PlayerInput))]
	public class InputManager : MonoBehaviour
	{
		public static InputManager instance;

		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;
		public void OnMove(InputValue value)
		{
			Vector2 moveDir = value.Get<Vector2>();
			move = moveDir;
			GameEventsManager.instance.inputEvents.MovePressed(moveDir);
		}
		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				look = value.Get<Vector2>();
			}
		}
		public void OnJump(InputValue value)
		{
			jump = value.isPressed;
			GameEventsManager.instance.inputEvents.JumpPressed();
		}
		public void OnSprint(InputValue value)
		{
			sprint = value.isPressed;
		}
		public void OnAttack(InputValue value)
        {
			GameEventsManager.instance.inputEvents.AttackPressed();
        }
		public void OnEvade(InputValue value)
        {
			GameEventsManager.instance.inputEvents.EvadePressed();
        }
		public void OnSheathe(InputValue value)
        {
			GameEventsManager.instance.inputEvents.SheathePressed();
		}
		public void OnInteract(InputValue value)
        {
			GameEventsManager.instance.inputEvents.InteractPressed();
        }
		public void OnEscape(InputValue value)
		{
			GameEventsManager.instance.inputEvents.EscapePressed();
		}
		public void OnSubmit(InputValue value)
        {
			GameEventsManager.instance.inputEvents.SubmitPressed();
        }
		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
        private void Awake()
        {
			instance = this;
        }
    }
	
}