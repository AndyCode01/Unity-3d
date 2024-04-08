using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimestamp 
{
    public int day, hour, minute;
    public GameTimestamp(int day, int hour, int minute)
    {
        this.day=day;
        this.hour=hour;
        this.minute=minute;
    }
}
//un dia en el juego-> 12m de juego continuo y una pausa para prepararse al siguiente dia. 15m de juego por dia de granja

//