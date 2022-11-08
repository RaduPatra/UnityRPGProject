using Player;
using UnityEngine;

public class UnlockableDoor : MonoBehaviour, IInteractable
{
    [SerializeField] public ItemWithAttributes requiredItemToUnlock;
    [SerializeField] private BoxCollider interactableCollider;
    [SerializeField] private BoxCollider doorCollider;
    [SerializeField] private bool disableColliderOnOpen;
    private Animator animator;
    private bool doorOpened;
    private const string DoorOpenAnimation = "OpenDoor";

    private void Awake()
    {
        animator = GetComponent<Animator>();
        SaveSystem.OnLoad += LoadDoor;
    }

    private void OnDestroy()
    {
        SaveSystem.OnLoad -= LoadDoor;
    }

    private void LoadDoor(SaveData saveData)
    {

        if (saveData.openedDoors.Contains(GetComponent<GameObjectId>().Id))
        {
            animator.Play(DoorOpenAnimation);
            // GetComponent<BoxCollider>().enabled = false;
            interactableCollider.enabled = false;
            if (disableColliderOnOpen)
            {
                doorCollider.enabled = false;
            }

            doorOpened = true;
        }
    }

    public void Interact(Interactor user)
    {
        if (doorOpened) return;

        if (user.GetComponent<InventoryHolder>().IsItemInInventory(requiredItemToUnlock))
        {
            //unlock the door
            OpenDoor(user);
            SaveData.Current.openedDoors.Add(GetComponent<GameObjectId>().Id);
            // GetComponent<BoxCollider>().enabled = false;
            interactableCollider.enabled = false;
            if (disableColliderOnOpen)
            {
                doorCollider.enabled = false;
            }
            doorOpened = true;

            //save door state
        }
    }

    private void OpenDoor(Interactor user)
    {
        user.GetComponent<CharacterAnimator>().PlayAnimation(CharacterAnimator.takeItem, true);
        animator.Play(DoorOpenAnimation);
    }

    public void InteractPreview(Interactor user)
    {
    }

    public void InteractExit(Interactor user)
    {
    }
}