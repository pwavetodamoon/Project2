using System;

namespace ObjectPool
{
    public interface IPooled<T>
    {
        Action<T> ReleaseCallback { get; set; }
        void Release();
    }
}