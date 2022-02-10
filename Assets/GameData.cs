using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameDataSO", order = 1)]

public class GameData : UnityEngine.ScriptableObject
{
    public CharacterData[] Characters;
}
