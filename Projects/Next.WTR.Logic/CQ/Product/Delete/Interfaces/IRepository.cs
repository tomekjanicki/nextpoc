namespace Next.WTR.Logic.CQ.Product.Delete.Interfaces
{
    using Next.WTR.Common.Shared.TemplateMethods.Commands.Interfaces;
    using Next.WTR.Types;

    public interface IRepository : IDeleteRepository
    {
        bool CanBeDeleted(PositiveInt id);
    }
}