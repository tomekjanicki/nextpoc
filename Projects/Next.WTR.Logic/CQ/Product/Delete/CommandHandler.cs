namespace Next.WTR.Logic.CQ.Product.Delete
{
    using Next.WTR.Common.Shared.TemplateMethods.Commands;
    using Next.WTR.Logic.CQ.Product.Delete.Interfaces;
    using Next.WTR.Types;
    using Next.WTR.Types.FunctionalExtensions;

    public sealed class CommandHandler : DeleteCommandHandlerTemplate<Command, IRepository>
    {
        public CommandHandler(IRepository repository)
            : base(repository)
        {
        }

        protected override IResult<NonEmptyString> BeforeDelete(Command message)
        {
            var id = message.IdVersion.Id;

            var canBeDeleted = DeleteRepository.CanBeDeleted(id);

            return !canBeDeleted ? ((NonEmptyString)"Can't delete because there are rows dependent on that item").GetFailResult() : base.BeforeDelete(message);
        }
    }
}
