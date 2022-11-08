using UnityEngine;

[CreateAssetMenu(fileName = "New DialogueOption", menuName = "Dialogue/DialogueOption", order = 6)]

public class DialogueOption : ScriptableObject
{
    public string optionText;
    public DialogueNode optionNode;
}