using Amazon.Runtime.Internal.Auth;
using FakeItEasy;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using NarwalRobotTest.Controllers;
using NarwalRobotTest.Data;
using NarwalRobotTest.Models;
using NarwalRobotTest.Models.Dictionaries;
using System.Diagnostics;
using Telerik.JustMock;
using Xunit;
using Mock = Telerik.JustMock.Mock;

namespace NarwalRobotTest.Test
{
    [TestClass]
    public class NarwalRobotApiTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly ApiContext _apiContext;
        private readonly IValidator<Robot> _validator;
        private readonly ArmWristMovimentDictionary _armWristMovimentDictionary;
        private readonly ArmElbowMovimentDictionary _armElbowMovimentDictionary;
        private readonly HeadTiltMovimentDictionary _headTiltMovimentDictionary;
        private readonly HeadRotationMovimentDictionary _headRotationMovimentDictionary;

        [TestMethod]
        public void RobotController_CreateRobot_ReturnsOk()
        {
            var controller = new RobotController(
                _apiContext,
                _validator,
                _armWristMovimentDictionary,
                _armElbowMovimentDictionary,
                _headRotationMovimentDictionary,
                _headTiltMovimentDictionary);
            
            var result = controller.CreateRobot();

            Assert.IsNotNull(result);
        }

       

        [TestMethod]
        public void RobotController_HeadValidation_Ok()
        {
            var controller = new RobotController(
                _apiContext,
                _validator,
                _armWristMovimentDictionary,
                _armElbowMovimentDictionary,
                _headRotationMovimentDictionary,
                _headTiltMovimentDictionary);

            var oldRobot = new Robot
            {
                HeadRotation = Enum.HeadRotationMovimentEnum.StandBy,
                HeadTilt = Enum.HeadTiltMovimentEnum.StandBy,
                LeftArmWrist = Enum.ArmWristMovimentEnum.StandBy,
                LeftArmElbow = Enum.ArmElbowMovimentEnum.StandBy,
                RightArmWrist = Enum.ArmWristMovimentEnum.StandBy,
                RightArmElbow = Enum.ArmElbowMovimentEnum.StandBy,
            };

            var newRobot = new Robot
            {
                HeadRotation = Enum.HeadRotationMovimentEnum.RotationMinus45,
                HeadTilt = Enum.HeadTiltMovimentEnum.StandBy,
                LeftArmWrist = Enum.ArmWristMovimentEnum.StandBy,
                LeftArmElbow = Enum.ArmElbowMovimentEnum.StandBy,
                RightArmWrist = Enum.ArmWristMovimentEnum.StandBy,
                RightArmElbow = Enum.ArmElbowMovimentEnum.StandBy,
            };

            var result = controller.HeadValidation(oldRobot, newRobot);

            Assert.IsNotNull(result);
            Assert.IsTrue(((IStatusCodeActionResult)result).StatusCode == 200);
        }

        [TestMethod]
        public void RobotController_HeadValidation_BadRequestHeadRule()
        {
            var controller = new RobotController(
                _apiContext,
                _validator,
                _armWristMovimentDictionary,
                _armElbowMovimentDictionary,
                _headRotationMovimentDictionary,
                _headTiltMovimentDictionary);

            var oldRobot = new Robot
            {
                HeadRotation = Enum.HeadRotationMovimentEnum.StandBy,
                HeadTilt = Enum.HeadTiltMovimentEnum.Down,
                LeftArmWrist = Enum.ArmWristMovimentEnum.StandBy,
                LeftArmElbow = Enum.ArmElbowMovimentEnum.StandBy,
                RightArmWrist = Enum.ArmWristMovimentEnum.StandBy,
                RightArmElbow = Enum.ArmElbowMovimentEnum.StandBy,
            };

            var newRobot = new Robot
            {
                HeadRotation = Enum.HeadRotationMovimentEnum.RotationMinus45,
                HeadTilt = Enum.HeadTiltMovimentEnum.StandBy,
                LeftArmWrist = Enum.ArmWristMovimentEnum.StandBy,
                LeftArmElbow = Enum.ArmElbowMovimentEnum.StandBy,
                RightArmWrist = Enum.ArmWristMovimentEnum.StandBy,
                RightArmElbow = Enum.ArmElbowMovimentEnum.StandBy,
            };

            var result = controller.HeadValidation(oldRobot, newRobot);

            Assert.IsNotNull(result);
            Assert.IsTrue(((IStatusCodeActionResult)result).StatusCode == 400);
        }

        [TestMethod]
        public void RobotController_ArmValidation_BadRequestLeftArmRule()
        {
            var controller = new RobotController(
                _apiContext,
                _validator,
                _armWristMovimentDictionary,
                _armElbowMovimentDictionary,
                _headRotationMovimentDictionary,
                _headTiltMovimentDictionary);

            var oldRobot = new Robot
            {
                HeadRotation = Enum.HeadRotationMovimentEnum.StandBy,
                HeadTilt = Enum.HeadTiltMovimentEnum.StandBy,
                LeftArmWrist = Enum.ArmWristMovimentEnum.Rotation180,
                LeftArmElbow = Enum.ArmElbowMovimentEnum.Contracted,
                RightArmWrist = Enum.ArmWristMovimentEnum.StandBy,
                RightArmElbow = Enum.ArmElbowMovimentEnum.StandBy,
            };

            var newRobot = new Robot
            {
                HeadRotation = Enum.HeadRotationMovimentEnum.StandBy,
                HeadTilt = Enum.HeadTiltMovimentEnum.StandBy,
                LeftArmWrist = Enum.ArmWristMovimentEnum.StandBy,
                LeftArmElbow = Enum.ArmElbowMovimentEnum.Contracted,
                RightArmWrist = Enum.ArmWristMovimentEnum.StandBy,
                RightArmElbow = Enum.ArmElbowMovimentEnum.StandBy,
            };

            var result = controller.ArmValidation(oldRobot, newRobot);

            Assert.IsNotNull(result);
            Assert.IsTrue(((IStatusCodeActionResult)result).StatusCode == 400);
        }

        [TestMethod]
        public void RobotController_ArmValidation_BadRequestRightArmRule()
        {
            var controller = new RobotController(
                _apiContext,
                _validator,
                _armWristMovimentDictionary,
                _armElbowMovimentDictionary,
                _headRotationMovimentDictionary,
                _headTiltMovimentDictionary);

            var oldRobot = new Robot
            {
                HeadRotation = Enum.HeadRotationMovimentEnum.StandBy,
                HeadTilt = Enum.HeadTiltMovimentEnum.StandBy,
                LeftArmWrist = Enum.ArmWristMovimentEnum.StandBy,
                LeftArmElbow = Enum.ArmElbowMovimentEnum.StandBy,
                RightArmWrist = Enum.ArmWristMovimentEnum.Rotation180,
                RightArmElbow = Enum.ArmElbowMovimentEnum.Contracted,
            };

            var newRobot = new Robot
            {
                HeadRotation = Enum.HeadRotationMovimentEnum.StandBy,
                HeadTilt = Enum.HeadTiltMovimentEnum.StandBy,
                LeftArmWrist = Enum.ArmWristMovimentEnum.StandBy,
                LeftArmElbow = Enum.ArmElbowMovimentEnum.Contracted,
                RightArmWrist = Enum.ArmWristMovimentEnum.StandBy,
                RightArmElbow = Enum.ArmElbowMovimentEnum.StandBy,
            };

            var result = controller.ArmValidation(oldRobot, newRobot);

            Assert.IsNotNull(result);
            Assert.IsTrue(((IStatusCodeActionResult)result).StatusCode == 400);
        }

        [TestMethod]
        public void RobotController_ArmValidation_Ok()
        {
            var controller = new RobotController(
                _apiContext,
                _validator,
                _armWristMovimentDictionary,
                _armElbowMovimentDictionary,
                _headRotationMovimentDictionary,
                _headTiltMovimentDictionary);

            var oldRobot = new Robot
            {
                HeadRotation = Enum.HeadRotationMovimentEnum.StandBy,
                HeadTilt = Enum.HeadTiltMovimentEnum.StandBy,
                LeftArmWrist = Enum.ArmWristMovimentEnum.StandBy,
                LeftArmElbow = Enum.ArmElbowMovimentEnum.StandBy,
                RightArmWrist = Enum.ArmWristMovimentEnum.Rotation180,
                RightArmElbow = Enum.ArmElbowMovimentEnum.Contracted,
            };

            var newRobot = new Robot
            {
                HeadRotation = Enum.HeadRotationMovimentEnum.StandBy,
                HeadTilt = Enum.HeadTiltMovimentEnum.StandBy,
                LeftArmWrist = Enum.ArmWristMovimentEnum.StandBy,
                LeftArmElbow = Enum.ArmElbowMovimentEnum.Contracted,
                RightArmWrist = Enum.ArmWristMovimentEnum.StandBy,
                RightArmElbow = Enum.ArmElbowMovimentEnum.StandBy,
            };

            var result = controller.ArmValidation(oldRobot, newRobot);

            Assert.IsNotNull(result);
            Assert.IsTrue(((IStatusCodeActionResult)result).StatusCode == 400);
        }
    }
}