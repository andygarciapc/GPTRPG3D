using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading.Tasks;

namespace BasicAI
{
    public class NPCDialogue : MonoBehaviour
    {
        [SerializeField] private GameObject mainCamera;
        [SerializeField] private GameObject dialogueHolder;

        [SerializeField] private Transform standingPoint;
        private Transform avatar;

        private async void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            avatar = other.transform;
            avatar.GetComponent<PlayerInput>().enabled = false;
            await Task.Delay(50);

            avatar.position = standingPoint.position;
            avatar.rotation = standingPoint.rotation;

            mainCamera.SetActive(false);
            dialogueHolder.SetActive(true);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        public void QuitButton()
        {
            avatar.GetComponent<PlayerInput>().enabled = true;

            mainCamera.SetActive(true);
            dialogueHolder.SetActive(false);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
