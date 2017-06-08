namespace Next.WTR.Logic.CQ.Customer.Delete
{
    using Common.Shared.TemplateMethods.Commands;
    using Interfaces;
    using Types;
    using Types.FunctionalExtensions;

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
