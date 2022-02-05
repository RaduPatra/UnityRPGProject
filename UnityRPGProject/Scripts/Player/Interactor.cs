using System;
using UnityEngine;

namespace Player
{
    public class Interactor : MonoBehaviour
    {
        // private PlayerManager playerManager;
        public PlayerManager PlayerManager { get; set; }
        private GameObject interactObject;
        
        private void Start()
        {
            PlayerManager = GetComponent<PlayerManager>();
        }
        private void Update()
        {
            if (PlayerManager == null) return;
            if (!PlayerManager.InputManager.interactInput) return;
            if (interactObject)
            {
                Interact(interactObject);
            }
        }
        
        private void Interact(GameObject go)
        {
            var interactObj = go.GetComponent<IInteractable>();
            interactObj?.Interact(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            interactObject = other.gameObject;
        }

        private void OnTriggerExit(Collider other)
        {
            interactObject = null;
        }
    }
}