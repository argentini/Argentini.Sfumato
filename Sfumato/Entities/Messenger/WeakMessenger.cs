using System.Reflection;

namespace Sfumato.Entities.Messenger;

/// <summary>
/// <example>
/// <code>
/// class Program
/// {
///     static WeakMessenger Messenger = new WeakMessenger();
/// 
///     static void Main()
///     {
///         Messenger.Register&lt;string&gt;(msg => {
///             msg.WriteToOutput();
///         });
///     }
///
///     static void Test()
///     {
///         Messenger.Send("Hello from thread!");
///     }
/// }
/// </code>
/// </example>
/// </summary>
public sealed class WeakMessenger
{
    private readonly ConcurrentDictionary<Type, List<WeakDelegate>> _handlers = new();

    public void Register<T>(Action<T> handler)
    {
        var list = _handlers.GetOrAdd(typeof(T), _ => []);

        lock (list)
            list.Add(new WeakDelegate(handler));
    }

    public void Unregister<T>(Action<T> handler)
    {
        if (_handlers.TryGetValue(typeof(T), out var list) == false)
            return;
        
        lock (list)
            list.RemoveAll(wd => wd.IsMatch(handler));
    }

    public void Send<T>(T message)
    {
        if (_handlers.TryGetValue(typeof(T), out var list) == false)
            return;
        
        List<WeakDelegate> snapshot;

        lock (list)
            snapshot = [..list];

        foreach (var weakDelegate in snapshot.Where(weakDelegate => weakDelegate.TryInvoke(message) == false))
            lock (list)
                list.Remove(weakDelegate);
    }

    private sealed class WeakDelegate(Delegate handler)
    {
        private readonly WeakReference? _targetRef = handler.Target is not null ? new WeakReference(handler.Target) : null;
        private readonly MethodInfo _method = handler.Method;
        private readonly Type _delegateType = handler.GetType();

        public bool TryInvoke<T>(T arg)
        {
            var target = _targetRef?.Target;

            if (_targetRef is not null && target is null)
                return false;

            var del = Delegate.CreateDelegate(_delegateType, target, _method);
            
            ((Action<T>)del)(arg);

            return true;
        }

        public bool IsMatch(Delegate other)
        {
            return other.Method == _method && (_targetRef is null || _targetRef.Target == other.Target);
        }
    }
}
