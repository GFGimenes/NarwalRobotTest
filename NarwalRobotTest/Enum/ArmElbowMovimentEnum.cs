using System.ComponentModel;

namespace NarwalRobotTest.Enum
{
    public enum ArmElbowMovimentEnum
    {
        [Description("Em Repouso")]
        StandBy = 1,
        [Description("Levemente Contraído")]
        LightlyContracted = 2,
        [Description("Contraído")]
        Contracted = 3 ,
        [Description("Fortemente Contraído")]
        StronglyContracted = 4
    }
}
