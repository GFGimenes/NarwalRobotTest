using System.ComponentModel;

namespace NarwalRobotTest.Enum
{
    public enum ArmWristMovimentEnum
    {
        [Description("Rotação para -90 graus")]
        RotationMinus90 = 1,
        [Description("Rotação para -45 graus")]
        RotationMinus45 = 2,
        [Description("Em Repouso")]
        StandBy = 3,
        [Description("Rotação para 45 graus")]
        Rotation45 = 4,
        [Description("Rotação para 90 graus")]
        Rotation90 = 5,
        [Description("Rotação para 135 graus")]
        Rotation135 = 6,
        [Description("Rotação para 180 graus")]
        Rotation180 = 7
    }
}