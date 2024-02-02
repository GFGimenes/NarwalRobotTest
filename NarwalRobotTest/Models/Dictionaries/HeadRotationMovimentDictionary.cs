using NarwalRobotTest.Enum;

namespace NarwalRobotTest.Models.Dictionaries
{
    public class HeadRotationMovimentDictionary
    {
        public Dictionary<HeadRotationMovimentEnum, string> headRotationMovimentDictionary = new()
        {
            {HeadRotationMovimentEnum.RotationMinus90, "Rotação para -90 graus"},
            {HeadRotationMovimentEnum.RotationMinus45, "Rotação para -45 graus"},
            {HeadRotationMovimentEnum.StandBy, "Em Repouso"},
            {HeadRotationMovimentEnum.Rotation45, "Rotação para 45 graus"},
            {HeadRotationMovimentEnum.Rotation90, "Rotação para 90 graus"}
        };
    }
}
