namespace Next.WTR.Logic.CQ.Product.Insert.Interfaces
{
    using Next.WTR.Logic.CQ.Product.ValueObjects;
    using Next.WTR.Types;

    public interface IRepository
    {
        bool CodeExists(Code code);

        PositiveInt Insert(Command command);
    }
}