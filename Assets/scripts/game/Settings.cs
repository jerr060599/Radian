using UnityEngine;
using System.Collections;

public static class Settings
{
    public static Vector2 defCursorCenter = new Vector2(9f, 9f);
    public static readonly int player = 0;
    public static readonly int dash = 0, toggleEnergy = 1;
    public static KeyCode[,] keys = {
        { KeyCode.Space, KeyCode.E}
    };
}
