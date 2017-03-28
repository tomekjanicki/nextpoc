namespace Next.WTR.Common.CQ
{
    using System;
    using Next.WTR.Types.FunctionalExtensions;

    public abstract class BaseCommandQuery<T> : ValueObject<T>
        where T : BaseCommandQuery<T>
    {
        protected BaseCommandQuery(Guid commandId)
        {
            CommandId = commandId;
        }

        public Guid CommandId { get; }

        protected override bool EqualsCore(T other)
        {
            return CommandId == other.CommandId;
        }

        protected override int GetHashCodeCore()
        {
            return CommandId.GetHashCode();
        }
    }
}
