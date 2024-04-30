using UnityEngine;
using UnityEngine.InputSystem;
using Basic.Events;

namespace Basic
{
	[RequireComponent(typeof(PlayerInput))]
	public class InputManager : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool atkclick;
		public bool roll;
		public bool sheathe;
		public bool submit;
		public bool escape;
		public bool interact;

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
		}
		public void OnSprint(InputValue value)
		{
			sprint = value.isPressed;
		}

		public void OnAttack(InputValue value)
        {
			atkclick = value.isPressed;
        }
		public void OnRoll(InputValue value)
        {
			roll = value.isPressed;
        }
		public void OnSheathe(InputValue value)
        {
			sheathe = value.isPressed;
		}
		public void OnInteract(InputValue value)
        {
			interact = value.isPressed;
			GameEventsManager.instance.inputEvents.InteractPressed();
        }
		public void OnEscape(InputValue value)
		{
			escape = value.isPressed;
			GameEventsManager.instance.inputEvents.EscapePressed();
		}
		public void OnSubmit(InputValue value)
        {
			submit = value.isPressed;
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
	}
	
}