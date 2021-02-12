namespace Arbiter.Core.Interfaces
{
    public interface IConverter<A, B>
    {
        A Convert(B input);
        B Convert(A input);
    }
}
