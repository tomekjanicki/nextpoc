namespace Next.WTR.Logic.CQ.Customer.Delete.Interfaces
{
    using Common.Shared.TemplateMethods.Commands.Interfaces;
    using Types;

    public interface IRepository : IDeleteRepository
    {
        bool CanBeDeleted(PositiveInt id);
    }
}