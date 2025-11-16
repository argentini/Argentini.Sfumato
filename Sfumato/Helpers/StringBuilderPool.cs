// ReSharper disable PublicConstructorInAbstractClass
namespace Sfumato.Helpers;

public sealed class StringBuilderPool : IObjectPool<StringBuilder>
{
    private readonly StringBuilder?[] _items;
    private readonly int _initialCapacity;
    private readonly int _maxRetainedCapacity;
    private int _index = -1;

    public StringBuilderPool(
        int poolSize = 64,
        int initialCapacity = 256,
        int maxRetainedCapacity = 16 * 1024)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(poolSize);

        _items = new StringBuilder?[poolSize];
        _initialCapacity = initialCapacity;
        _maxRetainedCapacity = maxRetainedCapacity;
    }

    public StringBuilder Get()
    {
        while (true)
        {
            var current = _index;
            
            if (current < 0)
                break;

            var newIndex = current - 1;

            if (Interlocked.CompareExchange(ref _index, newIndex, current) != current)
                continue;
            
            // Successfully popped
            var sb = _items[current];

            _items[current] = null;
            
            if (sb is not null)
            {
                sb.Clear();
                return sb;
            }
            
            break;
        }

        // Nothing in the pool, allocate new
        return new StringBuilder(_initialCapacity);
    }

    public void Return(StringBuilder? sb)
    {
        if (sb is null)
            return;

        // If it grew too big, just drop it and let GC collect
        if (sb.Capacity > _maxRetainedCapacity)
            return;

        sb.Clear();

        while (true)
        {
            var current = _index;
            var newIndex = current + 1;

            if (newIndex >= _items.Length)
            {
                // Pool is full, drop it
                return;
            }

            if (Interlocked.CompareExchange(ref _index, newIndex, current) != current)
                continue;

            _items[newIndex] = sb;

            return;
        }
    }
}