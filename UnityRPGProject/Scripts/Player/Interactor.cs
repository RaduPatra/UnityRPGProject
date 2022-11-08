using System;
using UnityEngine;

namespace Player
{
    public class Interactor : MonoBehaviour
    {
        // private PlayerManager playerManager;
        // public PlayerManager PlayerManager { get; set; }
        private GameObject interactObject;

        [SerializeField] private InputManager inputManager;

        private void Start()
        {
            // PlayerManager = GetComponent<PlayerManager>();
            inputManager.interactAction += StartInteract;
            interactObject = null;
        }

        private void OnDestroy()
        {
            inputManager.interactAction -= StartInteract;

        }

        private void StartInteract()
        {
            Interact(interactObject);
        }

        private void Interact(GameObject go)
        {
            if (go == null) return;
            var interactObj = go.GetComponent<IInteractable>();
            interactObj?.Interact(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            interactObject = other.gameObject;
            var interactable = interactObject.GetComponent<IInteractable>();
            interactable?.InteractPreview(this);
        }

        private void OnTriggerExit(Collider other)
        {
            interactObject = other.gameObject;
            var interactable = interactObject.GetComponent<IInteractable>();
            interactable?.InteractExit(this);
            interactObject = null;
        }
    }
}