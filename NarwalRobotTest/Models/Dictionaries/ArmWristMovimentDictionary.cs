using NarwalRobotTest.Enum;

namespace NarwalRobotTest.Models.Dictionaries
{
    public class ArmWristMovimentDictionary
    {
        public Dictionary<ArmWristMovimentEnum, string> armWristMovimentDictionary = new()
        {
            {ArmWristMovimentEnum.RotationMinus90, "Rotação para -90 graus"},
            {ArmWristMovimentEnum.RotationMinus45, "Rotação para -45 graus"},
            {ArmWristMovimentEnum.StandBy, "Em Repouso"},
            {ArmWristMovimentEnum.Rotation45, "Rotação para 45 graus"},
            {ArmWristMovimentEnum.Rotation90, "Rotação para 90 graus"},
            {ArmWristMovimentEnum.Rotation135, "Rotação para 135 graus"},
            {ArmWristMovimentEnum.Rotation180, "Rotação para 180 graus"}
        };
    }
}
