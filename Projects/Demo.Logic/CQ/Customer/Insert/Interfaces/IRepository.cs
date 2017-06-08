namespace Next.WTR.Logic.CQ.Customer.Insert.Interfaces
{
    using Types;

    public interface IRepository
    {
        PositiveInt Insert(Command command);
    }
}