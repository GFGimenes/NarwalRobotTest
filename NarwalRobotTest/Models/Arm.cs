using NarwalRobotTest.Enum;
using System.Text.Json.Serialization;

namespace NarwalRobotTest.Models
{
    public class Arm
    {
        [JsonIgnore]
        public int ArmId { get; private set; } 

        public ArmWristMovimentEnum Wrist { get; set; } = ArmWristMovimentEnum.StandBy;

        public ArmElbowMovimentEnum Elbow { get; set; } = ArmElbowMovimentEnum.StandBy;

        [JsonIgnore]
        public RightLeftArmEnum RightLeftArm { get; set; }
    }
}
