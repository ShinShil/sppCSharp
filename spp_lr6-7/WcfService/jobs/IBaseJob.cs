namespace WcfService
{
    public interface IBaseJob<T>
    {
        void Perform(T data);
    }
}
