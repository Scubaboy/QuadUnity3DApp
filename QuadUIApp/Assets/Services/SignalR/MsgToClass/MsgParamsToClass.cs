using Assets.Services.SignalR.Interfaces;
using Newtonsoft.Json;
using System;

namespace Assets.Services.SignalR.MsgToClass
{
    public class MsgParamsToClass : ISignalRClientParamToClass
    {
        /// <summary>
        /// Converts the signalR method json string into the required class object
        /// Notes: only supports single class object method parameter.
        /// </summary>
        /// <param name="methodParams">Mathod parameters</param>
        /// <param name="classType">Class type to convert the method parameters to.</param>
        /// <returns>New class object from json string.</returns>
        public object Convert(string methodParams, Type classType)
        {
            //Remove leading and trailing [];
            var trimmed = methodParams.Trim('[',']');

            //Find first parameter set
            var firstParam = trimmed.Substring(0, trimmed.IndexOf('}') + 1);

            Console.WriteLine(firstParam);
            return JsonConvert.DeserializeObject(firstParam, classType);
        }
    }
}
