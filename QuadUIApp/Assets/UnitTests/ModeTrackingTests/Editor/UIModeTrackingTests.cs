using Assets.Services.ModeCtrl;
using Assets.Services.ModeCtrl.Controllers;
using Assets.Services.ModeCtrl.Exceptions;
using Assets.Services.UICtrl.Interfaces;
using Assets.Services.UIs.Interfaces;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.UnitTests.ModeTrackingTests.Editor
{
    [TestFixture]
    internal class UIModeTrackingTests
    {
        [Test]
        public void UIControllerOnlyRegisterOnceExcptionIfMore()
        {
            //Arrange
            var mockedCtrl = Substitute.For<IUIController>();
            var register = new ModeTrackingController();

            //Act
            register.RegisterToUpdate(mockedCtrl);

            //Assert
            Assert.Throws(Is.TypeOf<UICtrlAlreadyRegisteredException>(), delegate { register.RegisterToUpdate(mockedCtrl); });
        }

        [Test]
        public void ViewControllerOnlyRegisterOnceExcptionIfMore()
        {
            //Arrange
            var mockedCtrl = Substitute.For<IUIMode>();
            var register = new ModeTrackingController();

            //Act
            register.RegisterToUpdate(mockedCtrl);

            //Assert
            Assert.Throws(Is.TypeOf<ViewCtrlAlreadyRegisteredException>(), delegate { register.RegisterToUpdate(mockedCtrl); });
        }

        [Test]
        public void UnRegisteredUICtrlCannotChangeModeException()
        {
            //Arrange
            var mockedCtrl = Substitute.For<IUIController>();
            var register = new ModeTrackingController();

            //Assert
            Assert.Throws(Is.TypeOf<UICtrlNotRegisteredException>(), delegate { register.ChangeActiveMode(Modes.Execute, mockedCtrl); });
        }

        [Test]
        public void UnRegisteredViewCtrlCannotChangeStatusException()
        {
            //Arrange
            var mockedCtrl = Substitute.For<IUIMode>();
            var register = new ModeTrackingController();

            //Assert
            Assert.Throws(Is.TypeOf<ViewCtrlNotRegisteredException>(), delegate { register.ChangeActiveModeStatus(ExecutionStatus.Canceled, mockedCtrl); });
        }

        [Test]
        public void RegisteredUICtrlCanChangeMode()
        {
            //Arrange
            var mockedCtrl = Substitute.For<IUIController>();
            var register = new ModeTrackingController();

            //Act
            register.RegisterToUpdate(mockedCtrl);
            register.ChangeActiveMode(Modes.Initialise, mockedCtrl);

            //Assert
            Assert.AreEqual(Modes.Initialise, register.ActiveMode);
        }

        [Test]
        public void RegisteredViewCtrlCanChangeStatus()
        {
            //Arrange
            var mockedCtrl = Substitute.For<IUIMode>();
            var register = new ModeTrackingController();

            //Act
            register.RegisterToUpdate(mockedCtrl);
            register.ChangeActiveModeStatus(ExecutionStatus.Running, mockedCtrl);

            //Assert
            Assert.AreEqual(ExecutionStatus.Running, register.Status);
        }
    }
}
