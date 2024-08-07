using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Room Set", menuName = "Dungeon Generator/Room Set", order = 1)]
public class RoomSet : ScriptableObject
{
    public GameObject[] Rooms;
}
