namespace Next.WTR.Logic.CQ.Product.Insert.Interfaces
{
    using Types;
    using ValueObjects;

    public interface IRepository
    {
        bool CodeExists(Code code);

        PositiveInt Insert(Command command);
    }
}