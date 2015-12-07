using Assets.Services.SignalR.Models;
using Assets.Services.SignalR.MsgToClass;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.UnitTests.SignalRClients.Editor
{
    [TestFixture]
    internal class SignalRHubToClientJsonToClassTests
    {
        [Test]
        public void ActiveQuadMsgConvertedToActiveQuadClass()
        {
            //Arrange
            var jsonMsgToClass = new MsgParamsToClass();
            var paramString = "[{\"QuadId\":\"1901\",\"SupportedComms\":\"0\",\"SupportedIMU\":\"0\",\"SupportGPS\":\"0\",\"SupportedAlt\":\"0\",\"InUse\":\"False\"}]";

            //Act
            var theClass = jsonMsgToClass.Convert(paramString, typeof(ActiveQuad));
            
            //Asert
            Assert.AreEqual(theClass.GetType(), typeof(ActiveQuad));
        }
    }
}
