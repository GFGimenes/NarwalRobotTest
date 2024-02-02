using FluentValidation;
using NarwalRobotTest.Enum;
using System.Text.Json.Serialization;

namespace NarwalRobotTest.Models
{
    public class Robot
    {
        [JsonIgnore]
        public int Id { get; set; } = 1;

        /// <summary>
        /// Inclinacao da cabeca
        /// </summary>
        public HeadTiltMovimentEnum HeadTilt { get; set; } = HeadTiltMovimentEnum.StandBy;

        /// <summary>
        /// Rotacao da cabeca
        /// </summary>
        public HeadRotationMovimentEnum HeadRotation { get; set; } = HeadRotationMovimentEnum.StandBy;

        /// <summary>
        /// Pulso direito
        /// </summary>
        public ArmWristMovimentEnum RightArmWrist { get; set; } = ArmWristMovimentEnum.StandBy;

        /// <summary>
        /// Cotovelo Direito
        /// </summary>
        public ArmElbowMovimentEnum RightArmElbow { get; set; } = ArmElbowMovimentEnum.StandBy;

        /// <summary>
        /// Pulso esquerdo
        /// </summary>
        public ArmWristMovimentEnum LeftArmWrist { get; set; } = ArmWristMovimentEnum.StandBy;

        /// <summary>
        /// Cotovelo Esquerdo
        /// </summary>
        public ArmElbowMovimentEnum LeftArmElbow { get; set; } = ArmElbowMovimentEnum.StandBy;
    }

    public class RobotValidator : AbstractValidator<Robot>
    {
        public RobotValidator()
        {
            RuleFor(x => x.HeadTilt)
               .NotEmpty()
               .IsInEnum()
               .WithMessage("Os Estados de Inclinação de cabeça vão de 1 a 3, favor consultar o estado do R.O.B.O, os possíveis estados e tentar novamente.");

            RuleFor(x => x.HeadRotation)
               .NotEmpty()
               .IsInEnum()
               .WithMessage("Os Estados de Inclinação de cabeça vão de 1 a 5, favor consultar o estado do R.O.B.O, os possíveis estados e tentar novamente.");

            RuleFor(x => x.RightArmWrist)
               .NotEmpty()
               .IsInEnum()
               .WithMessage("Os Estados de Inclinação de cabeça vão de 1 a 7, favor consultar o estado do R.O.B.O, os possíveis estados e tentar novamente.");

            RuleFor(x => x.RightArmElbow)
               .NotEmpty()
               .IsInEnum()
               .WithMessage("Os Estados de Inclinação de cabeça vão de 1 a 4, favor consultar o estado do R.O.B.O, os possíveis estados e tentar novamente.");

            RuleFor(x => x.LeftArmWrist)
               .NotEmpty()
               .IsInEnum()
               .WithMessage("Os Estados de Inclinação de cabeça vão de 1 a 5, favor consultar o estado do R.O.B.O, os possíveis estados e tentar novamente.");

            RuleFor(x => x.LeftArmElbow)
               .NotEmpty()
               .IsInEnum()
               .WithMessage("Os Estados de Inclinação de cabeça vão de 1 a 5, favor consultar o estado do R.O.B.O, os possíveis estados e tentar novamente.");
        }
    }
}
