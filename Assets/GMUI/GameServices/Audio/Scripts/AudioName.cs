
namespace GreiB.GameServices.Audio.Scripts
{
    public enum AudioName
    {
        // Main Music

        NoSound = -1,

        #region BGM 1 - 10

        BGM_Menu = 0,
        BGM_GAMEPLAY = 1,

        #endregion

        #region UI SOUND 11 - 100

        UI_Click = 11,
        UI_Transition_Door_Sliding = 12,
        UI_Transition_Door_Close = 13,
        UI_Transition_Door_Shut = 14,
        UI_Transition_Door_Ting = 15,

        #endregion

        #region GAME PLAY > 100

        GP_Type = 101,
        GP_TAPTAP = 102,

        #endregion
    }

    public enum AudioType
    {
        SFX = 0,
        BGM = 1,
        AMB = 2,
    }
}