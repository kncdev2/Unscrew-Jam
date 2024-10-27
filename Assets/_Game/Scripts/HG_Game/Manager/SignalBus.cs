using UnityEngine;
using System;
using System.Collections.Generic;

namespace HG
{
    public class SignalBus : PersistentSingleton<SignalBus>
    {
        // Event delegate type
        public delegate void SignalHandler<T>(T signal)
            where T : Signal;

        // Signal dictionary
        private Dictionary<Type, Delegate> signals = new Dictionary<Type, Delegate>();

        // Add a signal to the bus
        public void Register<T>(SignalHandler<T> handler)
            where T : Signal
        {
            Type signalType = typeof(T);
            if (!signals.ContainsKey(signalType))
            {
                signals[signalType] = handler;
            }
            else
            {
                var currentHandler = signals[signalType] as SignalHandler<T>;
                signals[signalType] = currentHandler + handler;
            }
        }

        // Remove a signal from the bus
        public void Unregister<T>(SignalHandler<T> handler)
            where T : Signal
        {
            Type signalType = typeof(T);
            if (signals.ContainsKey(signalType))
            {
                var currentHandler = signals[signalType] as SignalHandler<T>;
                signals[signalType] = currentHandler - handler;
            }
        }

        // Fire a signal
        public void FireSignal<T>(T signal)
            where T : Signal
        {
            Type signalType = typeof(T);
            if (signals.ContainsKey(signalType))
            {
                var currentHandler = signals[signalType] as SignalHandler<T>;
                currentHandler?.Invoke(signal);
            }
        }
    }

    public abstract class Signal { }
}
