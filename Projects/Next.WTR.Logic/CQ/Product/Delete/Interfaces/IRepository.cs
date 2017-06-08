namespace Next.WTR.Logic.CQ.Product.Delete.Interfaces
{
    using Common.Shared.TemplateMethods.Commands.Interfaces;
    using Types;

    public interface IRepository : IDeleteRepository
    {
        bool CanBeDeleted(PositiveInt id);
    }
}