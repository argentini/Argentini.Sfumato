using System.Collections.Concurrent;

namespace Sfumato.Tests;

public sealed class StringBuilderPoolTests
{
    [Fact]
    public void DuplicateReturnCannotLeaseSameBuilderTwice()
    {
        var pool = new StringBuilderPool(poolSize: 2);
        var builder = pool.Get();

        pool.Return(builder);
        pool.Return(builder);

        var first = pool.Get();
        var second = pool.Get();

        Assert.NotSame(first, second);
    }

    [Fact]
    public void ParallelLeasesRemainExclusive()
    {
        var pool = new StringBuilderPool(poolSize: 32);
        var active = new ConcurrentDictionary<StringBuilder, byte>();

        Parallel.For(0, 20_000, iteration =>
        {
            var builder = pool.Get();

            Assert.True(active.TryAdd(builder, 0));
            builder.Append('x');
            Assert.True(active.TryRemove(builder, out var removed));
            Assert.Equal(0, removed);

            pool.Return(builder);
        });
    }
}
