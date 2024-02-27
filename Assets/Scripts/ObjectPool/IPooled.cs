using System;

namespace ObjectPool
{
    public interface IPooled<T>
    {
        void Release();

        Action<T> ReleaseCallback { get; set; }
    }
}