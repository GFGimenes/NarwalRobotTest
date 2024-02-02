using System.ComponentModel;

namespace NarwalRobotTest.Enum
{
    public enum HeadTiltMovimentEnum
    {
        [Description("Para Cima")]
        Up = 1,
        [Description("Em Repouso")]
        StandBy = 2,
        [Description("Para Baixo")]
        Down =  3
    }
}
