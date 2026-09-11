// ReSharper disable PublicConstructorInAbstractClass
namespace Sfumato.Helpers;

public sealed class StringBuilderPool : IObjectPool<StringBuilder>
{
    private readonly StringBuilder?[] _items;
    private readonly int _initialCapacity;
    private readonly int _maxRetainedCapacity;
    private readonly Lock _lock = new();
    private int _count;

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
        lock (_lock)
        {
            if (_count > 0)
            {
                var index = --_count;
                var sb = _items[index]!;

                _items[index] = null;
                sb.Clear();

                return sb;
            }
        }

        return new StringBuilder(_initialCapacity);
    }

    public void Return(StringBuilder? sb)
    {
        if (sb is null)
            return;

        if (sb.Capacity > _maxRetainedCapacity)
            return;

        sb.Clear();

        lock (_lock)
        {
            if (_count == _items.Length)
                return;

            for (var index = 0; index < _count; index++)
            {
                if (ReferenceEquals(_items[index], sb))
                    return;
            }

            _items[_count++] = sb;
        }
    }
}
