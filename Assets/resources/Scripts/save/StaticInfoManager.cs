using System;
using UnityEngine;

public static class StaticInfoManager
{
    public static float current_player_position = 0f;
    public static bool isStageNameInit = false;
    public static bool isNewRecord;
    public static bool isCleared;
    public static int maxCombo;
    public static float clearPercent;
    public static int current_stage;
    public static int life = 1;
    public static bool background_sound_enable;
    public static bool effect_sound_enable;
    public static int last_checkpoint;
    public static int experiment_request_stage;

    public static void ValueInit()
    {
        isNewRecord = false;
        isCleared = false;
        maxCombo = 0;
        clearPercent = 0f;
        life = 1;
        last_checkpoint = 0;
        current_player_position = 0f;
    }
}
