
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

        Gameplay_PlayerJump = 102,
        Gameplay_PlayerScore = 103,
        Gameplay_PlayerLose = 104,
        Gameplay_TimerRun = 105,
        Gameplay_WeaponSwitch = 106,
        Gameplay_WeaponEquip = 107,
        Gameplay_WeaponUnequip = 108,
        Gameplay_WeaponPickup = 109,
        Gameplay_WeaponDrop = 110,
        Gameplay_WeaponThrow = 111,
        Gameplay_WeaponImpact = 112,
        Gameplay_WeaponExplosion = 113,
        Gameplay_WeaponMelee = 114,
        Gameplay_WeaponCharge = 115,

        #endregion
    }

    public enum AudioType
    {
        SFX = 0,
        BGM = 1,
        AMB = 2,
    }
}