using NarwalRobotTest.Enum;

namespace NarwalRobotTest.Models.Dictionaries
{
    public class HeadTiltMovimentDictionary
    {
        public Dictionary<HeadTiltMovimentEnum, string> headTiltMovimentDictionary = new()
        {
            {HeadTiltMovimentEnum.Up, "Para Cima"},
            {HeadTiltMovimentEnum.StandBy, "Em Repouso"},
            {HeadTiltMovimentEnum.Down, "Para Baixo"}
        };
    }
}
