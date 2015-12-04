namespace Assets.UnitTests.SignalRClient.Editor
{
    using Services.SignalR.MsgParser;
    using NUnit.Framework;
    using System;

    [TestFixture]
    internal class SignalRHubToClientMsgParserTests
    {
        private string hubToClientMsg = "{\"C\":\"B,6 | O,0 | P,0 | Q,0\",\"M\":[{\"H\":\"hubNane\",\"M\":\"clientMethodUpdate\",\"A\":[\"Param1\",\"Param2\"]}]}";

        [Test]
        public void ExtractsHubnameFromCorrectlyFormedHubToClientMsg()
        {
            //Arrange
            var msgParser = new HubToClientMsgParser();

            //Act
            var parsedResult = msgParser.ParseHubToClientMsg(hubToClientMsg);

            //Assert
            Assert.AreEqual(parsedResult.Hubname, "hubNane");
        }

        [Test]
        public void ExtractsClientMethodFromCorrectlyFormedHubToClientMsg()
        {
            //Arrange
            var msgParser = new HubToClientMsgParser();

            //Act
            var parsedResult = msgParser.ParseHubToClientMsg(hubToClientMsg);

            //Assert
            Assert.AreEqual(parsedResult.ClientMethodName, "clientMethodUpdate");
        }

        [Test]
        public void ExtractsClientMethodParametersFromCorrectlyFormedHubToClientMsg()
        {
            //Arrange
            var msgParser = new HubToClientMsgParser();

            //Act
            var parsedResult = msgParser.ParseHubToClientMsg(hubToClientMsg);

            //Assert
            Console.WriteLine(parsedResult.ClientMethodParameters);
            Assert.AreEqual(parsedResult.ClientMethodParameters, "[\"Param1\",\"Param2\"]");
        }
    }
}
