using NarwalRobotTest.Enum;

namespace NarwalRobotTest.Models.Dictionaries
{
    public class ArmElbowMovimentDictionary
    {
        public Dictionary<ArmElbowMovimentEnum, string> armElbowMovimentDictionary = new()
        {
            {ArmElbowMovimentEnum.StandBy, "Em Repouso"},
            {ArmElbowMovimentEnum.LightlyContracted, "Levemente Contraído"},
            {ArmElbowMovimentEnum.Contracted, "Contraído"},
            {ArmElbowMovimentEnum.StronglyContracted, "Fortemente Contraído"}
        };
    }
}
