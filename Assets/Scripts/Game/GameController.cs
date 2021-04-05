using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameController 
{
    public static Action OnGameWin;
    public static Action OnGameLose;


    public static PlayerChar picher;
    public static PlayerChar cacher;
    public static float offset;
    public static List<PlayerChar> playerChars = new List<PlayerChar>();
    public static int playersPas = 0;
    public static int gameRuned = 0;
    public static bool canThrow;


}
