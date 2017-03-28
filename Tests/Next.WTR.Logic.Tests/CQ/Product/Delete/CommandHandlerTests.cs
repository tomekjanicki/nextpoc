namespace Next.WTR.Logic.Tests.CQ.Product.Delete
{
    using Next.WTR.Common.Shared;
    using Next.WTR.Logic.CQ.Product.Delete;
    using Next.WTR.Logic.CQ.Product.Delete.Interfaces;
    using Next.WTR.Types;
    using Next.WTR.Types.FunctionalExtensions;
    using NSubstitute;
    using NUnit.Framework;
    using Shouldly;

    public class CommandHandlerTests
    {
        private CommandHandler _commandHandler;
        private IRepository _repository;

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<IRepository>();
            _commandHandler = new CommandHandler(_repository);
        }

        [Test]
        public void CantDelete_ShouldBeFaliure()
        {
            var command = GetValidCommand();
            _repository.ExistsById(Arg.Any<PositiveInt>()).Returns(true);
            _repository.GetRowVersionById(Arg.Any<PositiveInt>()).Returns(command.IdVersion.Version);
            _repository.CanBeDeleted(Arg.Any<PositiveInt>()).Returns(false);

            var result = _commandHandler.Handle(command);

            result.IsFailure.ShouldBeTrue();
            result.Error.ErrorType.ShouldBe(ErrorType.Generic);
        }

        private static Command GetValidCommand()
        {
            return Extensions.GetValue(() => Command.TryCreate(1, "x"));
        }
    }
}
