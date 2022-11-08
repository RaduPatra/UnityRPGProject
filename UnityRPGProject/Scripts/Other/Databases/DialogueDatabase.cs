using UnityEngine;

[CreateAssetMenu(fileName = "New DialogueNode Database", menuName = "Databases/Dialogue Database", order = 1)]
public class DialogueDatabase : Database<DialogueNode>
{
    [ContextMenu("Populate Database")]
    public override void PopulateDatabase()
    {
        base.PopulateDatabase();
    }
}