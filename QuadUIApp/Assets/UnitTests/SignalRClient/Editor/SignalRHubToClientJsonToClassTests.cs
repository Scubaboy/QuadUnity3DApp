using Assets.Services.SignalR.Models;
using Assets.Services.SignalR.MsgToClass;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.UnitTests.SignalRClient.Editor
{
    [TestFixture]
    internal class SignalRHubToClientJsonToClassTests
    {
        [Test]
        public void ActiveQuadMsgConvertedToActiveQuadClass()
        {
            //Arrange
            var jsonMsgToClass = new MsgParamsToClass();
            var paramString = "[\"activeQuad\"{\"QuadId\":\"1901\",\"SupportedComms\":\"0\",\"day\":\"30\"}]";

            //Act
            var theClass = jsonMsgToClass.Convert(paramString, typeof(ActiveQuad));
        }
    }
}
