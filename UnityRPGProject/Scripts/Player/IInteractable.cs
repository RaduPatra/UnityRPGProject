namespace Player
{
    public interface IInteractable
    {
        void Interact(Interactor user);
        void InteractPreview(Interactor user);
        void InteractExit(Interactor user);
        //void Use(PlayerManager user);
    }
}