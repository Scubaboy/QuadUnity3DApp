using Assets.Services.ViewSystem.ModeControl;
using Assets.Services.ViewSystem.ModeControl.Controllers;
using Assets.Services.ViewSystem.ModeControl.Exceptions;
using Assets.Services.ViewSystem.View.Interfaces;
using Assets.Services.ViewSystem.ViewControl.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Assets.UnitTests.ModeTrackingTests.Editor
{
    [TestFixture]
    internal class UIModeTrackingTests
    {
        [Test]
        public void UIControllerOnlyRegisterOnceExcptionIfMore()
        {
            //Arrange
            var mockedCtrl = Substitute.For<IViewController>();
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
            var mockedCtrl = Substitute.For<IView>();
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
            var mockedCtrl = Substitute.For<IViewController>();
            var register = new ModeTrackingController();

            //Assert
            Assert.Throws(Is.TypeOf<UICtrlNotRegisteredException>(), delegate { register.ChangeActiveMode(Modes.Execute, mockedCtrl); });
        }

        [Test]
        public void UnRegisteredViewCtrlCannotChangeStatusException()
        {
            //Arrange
            var mockedCtrl = Substitute.For<IView>();
            var register = new ModeTrackingController();

            //Assert
            Assert.Throws(Is.TypeOf<ViewCtrlNotRegisteredException>(), delegate { register.ChangeActiveModeStatus(ExecutionStatus.Canceled, mockedCtrl); });
        }

        [Test]
        public void RegisteredUICtrlCanChangeMode()
        {
            //Arrange
            var mockedCtrl = Substitute.For<IViewController>();
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
            var mockedCtrl = Substitute.For<IView>();
            var register = new ModeTrackingController();

            //Act
            register.RegisterToUpdate(mockedCtrl);
            register.ChangeActiveModeStatus(ExecutionStatus.Running, mockedCtrl);

            //Assert
            Assert.AreEqual(ExecutionStatus.Running, register.Status);
        }
    }
}
