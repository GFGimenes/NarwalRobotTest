using Amazon.Runtime.Internal;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using NarwalRobotTest.Data;
using NarwalRobotTest.Enum;
using NarwalRobotTest.Models;
using NarwalRobotTest.Models.Dictionaries;
using System.Linq;
using System.Text.Json.Serialization;

namespace NarwalRobotTest.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RobotController : ControllerBase
    {
        private readonly ApiContext _context;
        private IValidator<Robot> _validator;
        private readonly ArmWristMovimentDictionary _armWristMovimentDictionary;
        private readonly ArmElbowMovimentDictionary _armElbowMovimentDictionary;
        private readonly HeadRotationMovimentDictionary _headRotationMovimentDictionary;
        private readonly HeadTiltMovimentDictionary _headTiltMovimentDictionary;

        public RobotController(
            ApiContext context,
            IValidator<Robot> validator,
            ArmWristMovimentDictionary armWristMovimentDictionary,
            ArmElbowMovimentDictionary armElbowMovimentDictionary,
            HeadRotationMovimentDictionary headRotationMovimentDictionary,
            HeadTiltMovimentDictionary headTiltMovimentDictionary)
        {
            _context = context;
            _validator = validator;        
            _armWristMovimentDictionary = armWristMovimentDictionary;
            _armElbowMovimentDictionary = armElbowMovimentDictionary;
            _headRotationMovimentDictionary = headRotationMovimentDictionary;
            _headTiltMovimentDictionary = headTiltMovimentDictionary;
        }

        [HttpPost]
        public IActionResult CreateRobot()
        {
            try
            {
                var existingRobot = _context.Robot.FirstOrDefault();

                if (existingRobot != null)
                {
                    return BadRequest("Já existe uma unidade R.O.B.O criada, caso queira, utilize o modulo de Delete, e crie outra unidade.");
                }

                Robot newRobot = new();

                _context.Robot.Add(newRobot);
                _context.SaveChanges();

                var robotCreated = _context.Robot.Find(newRobot.Id);

                _context.Entry(newRobot).State = EntityState.Detached;

                return Ok(robotCreated);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse() { Message = ex.Message });
            }
        }


        [HttpGet]
        public IActionResult GetActualRobot()
        {
            var actualRobotState = _context.Robot.FirstOrDefault();

            if (actualRobotState == null)
            {
                return NotFound("R.O.B.O ainda não foi criado. Para utilizar a função de consulta, favor criar o R.O.B.O primeiro");
            }

            return Ok(actualRobotState);
        }


        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ProducesErrorResponseTypeAttribute), 400)]
        [ProducesResponseType(typeof(ProducesErrorResponseTypeAttribute), 404)]
        [HttpPost]
        public IActionResult UpdateRobot(Robot robotMoves)
        {
            var actualRobotState = _context.Robot.FirstOrDefault();

            if (actualRobotState == null)
            {
                return NotFound("R.O.B.O ainda não foi criado. Para usar a função de atualização de posições, favor criar o R.O.B.O primeiro");
            }

            _validator.ValidateAndThrow(robotMoves);

            var headValidation = HeadValidation(actualRobotState, robotMoves);

            if (((IStatusCodeActionResult)headValidation).StatusCode == 400)
            {
                return BadRequest(headValidation);
            }

            var armValidation = ArmValidation(actualRobotState, robotMoves);

            if (((IStatusCodeActionResult)armValidation).StatusCode == 400)
            {
                return BadRequest(armValidation);
            }

            _context.Entry(actualRobotState).State = EntityState.Detached;

            try
            {
                _context.Update(robotMoves);

                var savedRobot = _context.Robot.Find(robotMoves.Id);

                _context.SaveChanges();

                return new JsonResult(Ok(savedRobot));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse() { Message = ex.Message});
            }
        }

        [HttpDelete]
        public IActionResult DeleteRobot() 
        {
            var robotToDelete = _context.Robot.FirstOrDefault();

            if(robotToDelete == null)
            {
                return NotFound("R.O.B.O ainda não foi criado. Para usar a função de deletar, favor criar o R.O.B.O primeiro");
            }

            try
            {
                _context.Robot.Remove(robotToDelete);
                _context.SaveChanges();
                
                var verifyDeletedRobot = _context.Robot.FirstOrDefault();

                _context.Entry(verifyDeletedRobot).State = EntityState.Detached;

                if (verifyDeletedRobot == null)
                {
                    return Ok("R.O.B.O Deletado com sucesso!");
                }

                return BadRequest("R.O.B.O não deletado, favor contactar um administrador do sistema.");
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse() { Message = ex.Message });
            }
        }

        [NonAction]
        public IActionResult HeadValidation(Robot actualRobot, Robot newMoviments)
        {
            if (actualRobot.HeadRotation != newMoviments.HeadRotation)
            {
                if (actualRobot.HeadTilt == HeadTiltMovimentEnum.Down)
                {
                    ErrorResponse errorResponse = new ErrorResponse() { Message = "Só poderá Rotacionar a Cabeça caso sua Inclinação da Cabeça não esteja em estado Para Baixo." };
                    return BadRequest(errorResponse.Message);
                }
            }

            var diferenceBetweenRotationSteps = Math.Abs(actualRobot.HeadRotation - newMoviments.HeadRotation);

            if (diferenceBetweenRotationSteps > 1)
            {
                ErrorResponse errorResponse = new ErrorResponse() 
                { 
                    Message = 
                    "A Progressão de estados de Rotação para a Cabeça precisa seguir na ordem crescente ou decrescente, não pulando etapas. Para mais informações, consulte a lista de movimentos. Atualmente a posição é: "
                    + _headRotationMovimentDictionary.headRotationMovimentDictionary.Where(x => x.Key == actualRobot.HeadRotation).Select(x => x.Value).FirstOrDefault()
                };
                return BadRequest(errorResponse.Message);
            } 

            var diferenceBetweenTiltSteps = Math.Abs(actualRobot.HeadTilt - newMoviments.HeadTilt);

            if (diferenceBetweenTiltSteps < -1 || diferenceBetweenTiltSteps > 1)
            {
                ErrorResponse errorResponse = new ErrorResponse() 
                { 
                    Message = 
                    "A Progressão de estados de Inclinação para a Cabeça precisa seguir na ordem crescente ou decrescente, não pulando etapas. Para mais informações, consulte a lista de movimentos"
                    + _headTiltMovimentDictionary.headTiltMovimentDictionary.Where(x => x.Key == actualRobot.HeadTilt).Select(x => x.Value).FirstOrDefault()
                };
                return BadRequest(errorResponse.Message);
            }

            return Ok();
        }

        [NonAction]
        public IActionResult ArmValidation(Robot actualRobot, Robot newMoviments)
        {
            if (actualRobot.LeftArmWrist != newMoviments.LeftArmWrist)
            {
                if (actualRobot.LeftArmElbow != ArmElbowMovimentEnum.StronglyContracted)
                {
                    ErrorResponse errorResponse = new ErrorResponse() { Message = "Só poderá movimentar o Pulso Esquerdo caso o Cotovelo Esquerdo esteja Fortemente Contraído." };
                    return BadRequest(errorResponse.Message);
                }
            }

            if (actualRobot.RightArmWrist != newMoviments.RightArmWrist)
            {
                if (actualRobot.RightArmElbow != ArmElbowMovimentEnum.StronglyContracted)
                {
                    ErrorResponse errorResponse = new ErrorResponse() { Message = "Só poderá movimentar o Pulso Direito caso o Cotovelo Direito esteja Fortemente Contraído." };
                    return BadRequest(errorResponse.Message);
                }
            }

            var diferenceBetweenLeftWristSteps = Math.Abs(actualRobot.LeftArmWrist - newMoviments.LeftArmWrist);
            var diferenceBetweenRightWristSteps = Math.Abs(actualRobot.RightArmWrist - newMoviments.RightArmWrist);
            var diferenceBetweenLeftElbowSteps = Math.Abs(actualRobot.LeftArmElbow - newMoviments.LeftArmElbow);
            var diferenceBetweenRightElbowSteps = Math.Abs(actualRobot.RightArmElbow - newMoviments.RightArmElbow);

            if (diferenceBetweenLeftWristSteps > 1)
            {
                ErrorResponse errorResponse = new ErrorResponse() 
                {
                    Message = 
                    "A Progressão de estados de do Pulso Esquerdo precisa seguir na ordem crescente ou decrescente, não pulando etapas. Para mais informações, consulte a lista de movimentos. Atualmente a posição é:" 
                    + _armWristMovimentDictionary.armWristMovimentDictionary.Where(x => x.Key == actualRobot.LeftArmWrist).Select(x => x.Value).FirstOrDefault()
                };
                return BadRequest(errorResponse.Message);
            }

            if (diferenceBetweenRightWristSteps > 1)
            {
                ErrorResponse errorResponse = new ErrorResponse()
                {
                    Message =
                    "A Progressão de estados de do Pulso Direito precisa seguir na ordem crescente ou decrescente, não pulando etapas. Para mais informações, consulte a lista de movimentos. Atualmente a posição é:"
                    + _armWristMovimentDictionary.armWristMovimentDictionary.Where(x => x.Key == actualRobot.RightArmWrist).Select(x => x.Value).FirstOrDefault()
                };
                return BadRequest(errorResponse.Message);
            }

            if (diferenceBetweenLeftElbowSteps > 1)
            {
                ErrorResponse errorResponse = new ErrorResponse()
                {
                    Message =
                    "A Progressão de estados de do Cotovelo Esquerdo precisa seguir na ordem crescente ou decrescente, não pulando etapas. Para mais informações, consulte a lista de movimentos. Atualmente a posição é:"
                    + _armElbowMovimentDictionary.armElbowMovimentDictionary.Where(x => x.Key == actualRobot.LeftArmElbow).Select(x => x.Value).FirstOrDefault()
                };
                return BadRequest(errorResponse.Message);
            }

            if (diferenceBetweenRightElbowSteps > 1)
            {
                ErrorResponse errorResponse = new ErrorResponse()
                {
                    Message =
                    "A Progressão de estados de do Cotovelo Direito precisa seguir na ordem crescente ou decrescente, não pulando etapas. Para mais informações, consulte a lista de movimentos. Atualmente a posição é:"
                    + _armElbowMovimentDictionary.armElbowMovimentDictionary.Where(x => x.Key == actualRobot.RightArmElbow).Select(x => x.Value).FirstOrDefault()
                };
                return BadRequest(errorResponse.Message);
            }

            return Ok();
        }

    }
}
