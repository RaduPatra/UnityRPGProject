using UnityEngine;

[CreateAssetMenu(fileName = "New Event Channel", menuName = "Event Channels/GameObjectEventChannel", order = 6)]
public class GameObjectEventChannel : GameEvent<GameObject>
{
}