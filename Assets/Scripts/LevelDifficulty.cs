using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDifficulty
{
    public float noteReloadTime;
    public int noteCount;
    public LevelDifficulty(float noteReloadTime, int noteCount)
    {
        this.noteReloadTime = noteReloadTime;
        this.noteCount = noteCount;
    }
}
