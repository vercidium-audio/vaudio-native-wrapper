// AI-created

using System;
using System.Collections.Concurrent;

namespace AOT
{
    // Unity's IL2CPP matches this attribute by name, so the wrapper doesn't need to reference UnityEngine
    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class MonoPInvokeCallbackAttribute : Attribute
    {
        public MonoPInvokeCallbackAttribute(Type type)
        {
        }
    }
}

namespace vaudionativewrapper.managed
{
    // IL2CPP can only pass static methods to native code, so each native callback is a static trampoline that looks up its managed handler by the native pointer it receives
    internal sealed class CallbackTable<T> where T : class
    {
        readonly ConcurrentDictionary<IntPtr, T> handlers = new ConcurrentDictionary<IntPtr, T>();

        // Returns true if a handler is now registered
        public bool Set(IntPtr owner, T handler)
        {
            if (handler == null)
            {
                handlers.TryRemove(owner, out _);
                return false;
            }

            handlers[owner] = handler;
            return true;
        }

        public T Get(IntPtr owner) => handlers.TryGetValue(owner, out var handler) ? handler : null;

        public T Take(IntPtr owner) => handlers.TryRemove(owner, out var handler) ? handler : null;

        public void Remove(IntPtr owner) => handlers.TryRemove(owner, out _);
    }
}
