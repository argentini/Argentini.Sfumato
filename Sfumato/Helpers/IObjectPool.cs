namespace Sfumato.Helpers;

public interface IObjectPool<T>
{
    T Get();
    void Return(T obj);
}