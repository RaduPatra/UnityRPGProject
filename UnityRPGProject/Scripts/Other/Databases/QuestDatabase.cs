using UnityEngine;

[CreateAssetMenu(fileName = "New Quest Database", menuName = "Databases/Quest Database", order = 1)]
public class QuestDatabase : Database<QuestSO>
{
    [ContextMenu("Populate Database")]
    public override void PopulateDatabase()
    {
        base.PopulateDatabase();
    }
}