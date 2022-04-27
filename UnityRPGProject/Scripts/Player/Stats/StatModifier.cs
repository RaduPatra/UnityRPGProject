using System;

[Serializable]
public class StatModifier
{
    public StatType type;
    public float value;
    public StatModifier(StatType type, float value)
    {
        this.type = type;
        this.value = value;
    }
}