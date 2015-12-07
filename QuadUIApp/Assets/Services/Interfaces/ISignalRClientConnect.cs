﻿namespace Assets.Services.Interfaces
{
    using System;

    /// <summary>
    /// SignalR Client Connect Interface defines consumer access.
    /// </summary>
    public interface ISignalRClientConnect
    {
        /// <summary>
        /// Registers a callback with a signalR client hub method.
        /// </summary>
        /// <typeparam name="T">Callback parameter type.</typeparam>
        /// <param name="methodName">SignalR client hub method name.</param>
        /// <param name="callback">Callback method to attach.</param>
        void Register<T>(string methodName, ISignalRCallbackAction callback) where T : class;

        /// <summary>
        /// Sends a message to the server hub.
        /// </summary>
        /// <typeparam name="T">Message type.</typeparam>
        /// <param name="msg">Message parameter.</param>
        void Send<T>(T msg) where T : class;
    }
}