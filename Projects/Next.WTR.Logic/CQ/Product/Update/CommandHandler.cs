namespace Next.WTR.Logic.CQ.Product.Update
{
    using Next.WTR.Common.Shared.TemplateMethods.Commands;
    using Next.WTR.Common.Shared.TemplateMethods.Commands.Interfaces;

    public sealed class CommandHandler : UpdateCommandHandlerTemplate<Command, IUpdateRepository<Command>>
    {
        public CommandHandler(IUpdateRepository<Command> repository)
            : base(repository)
        {
        }
    }
}
