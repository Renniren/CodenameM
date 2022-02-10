using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHudInfoManager : MonoBehaviour
{
    public int ForWhichPlayer = 0;
    public TMP_Text damage, PlayerName, CharacterName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        damage.text = PlayerCharacter.Players[ForWhichPlayer]?.Damage.ToString() + "%";
        PlayerName.text = PlayerCharacter.Players[ForWhichPlayer]?.character.PlayerName;
        CharacterName.text = PlayerCharacter.Players[ForWhichPlayer]?.character.CharacterName;
    }
}
