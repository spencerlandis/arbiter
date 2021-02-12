namespace Arbiter.DataFeed.Shared.Interfaces
{
    public interface IConverter<A, B>
    {
        A Convert(B input);
        B Convert(A input);
    }
}
