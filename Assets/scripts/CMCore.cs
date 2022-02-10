using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CMCore
{
    public static class Entities
    {
        public static PlayerCharacter GetPlayerCharacter(GameObject fromWhat)
        {
            PlayerCharacter character;
            fromWhat.TryGetComponent(out character);
            return character;
        }
    }

    public static class ExtraMath
    {
        public static float PercentOf(float desired, float ofwhat)
        {
            return (desired / 100) * ofwhat;
        }
    }
}