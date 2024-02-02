using FluentValidation;
using NarwalRobotTest.Enum;
using System.Text.Json.Serialization;

namespace NarwalRobotTest.Models
{
    public class Head
    {
        [JsonIgnore]
        public int HeadId { get; private set; }

        //Inclinacao da cabeca
        public HeadTiltMovimentEnum Tilt { get; set; } = HeadTiltMovimentEnum.StandBy;

        //Rotacao da cabeca
        public HeadRotationMovimentEnum Rotation { get; set; } = HeadRotationMovimentEnum.StandBy;

    }
    public class HeadValidator : AbstractValidator<Head>
    {
        public HeadValidator()
        {
        }
    }
}
