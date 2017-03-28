﻿namespace Next.WTR.Common.Tests.Shared.TemplateMethods.Commands
{
    using Next.WTR.Common.Handlers.Interfaces;
    using Next.WTR.Common.Shared;
    using Next.WTR.Common.Shared.TemplateMethods.Commands;
    using Next.WTR.Common.Shared.TemplateMethods.Commands.Interfaces;
    using Next.WTR.Common.ValueObjects;
    using Next.WTR.Types;
    using Next.WTR.Types.FunctionalExtensions;
    using NSubstitute;
    using NUnit.Framework;
    using Shouldly;

    public class DeleteCommandHandlerTemplateTests
    {
        private IRepository _repository;
        private Handler _handler;
        private ICommand _command;

        public interface ICommand : IIdVersion, IRequest<IResult<Error>>
        {
        }

        public interface IRepository : IDeleteRepository
        {
        }

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<IRepository>();
            _command = Substitute.For<ICommand>();
            _handler = new Handler(_repository);
        }

        [Test]
        public void ShouldSucceed()
        {
            var idVersion = GetValidIdVersion();
            _command.IdVersion.Returns(idVersion);
            _repository.ExistsById(idVersion.Id).Returns(true);
            _repository.GetRowVersionById(idVersion.Id).Returns(idVersion.Version);
            var r = _handler.Handle(_command);
            r.IsSuccess.ShouldBeTrue();
        }

        [Test]
        public void NotExists_ShouldFail()
        {
            var idVersion = GetValidIdVersion();
            _command.IdVersion.Returns(idVersion);
            _repository.ExistsById(idVersion.Id).Returns(false);
            var r = _handler.Handle(_command);
            r.IsFailure.ShouldBeTrue();
            r.Error.ErrorType.ShouldBe(ErrorType.NotFound);
        }

        [Test]
        public void WrongRowVersion_ShouldFail()
        {
            var idVersion = GetValidIdVersion();
            _command.IdVersion.Returns(idVersion);
            _repository.ExistsById(idVersion.Id).Returns(true);
            _repository.GetRowVersionById(idVersion.Id).Returns((NonEmptyString)$"{idVersion.Version}X");
            var r = _handler.Handle(_command);
            r.IsFailure.ShouldBeTrue();
            r.Error.ErrorType.ShouldBe(ErrorType.RowVersionMismatch);
        }

        [Test]
        public void NoRowVersion_ShouldFail()
        {
            var error = _handler.GetRowVersionIsEmptyMessage();
            var idVersion = GetValidIdVersion();
            _command.IdVersion.Returns(idVersion);
            _repository.ExistsById(idVersion.Id).Returns(true);
            _repository.GetRowVersionById(idVersion.Id).Returns((Maybe<NonEmptyString>)null);
            var r = _handler.Handle(_command);
            r.IsFailure.ShouldBeTrue();
            r.Error.ErrorType.ShouldBe(ErrorType.Generic);
            r.Error.Message.ShouldBe(error);
        }

        private static IdVersion GetValidIdVersion()
        {
            return Extensions.GetValue(() => IdVersion.TryCreate(1, "v1", (NonEmptyString)nameof(IdVersion.Id), (NonEmptyString)nameof(IdVersion.Version)));
        }

        public class Handler : DeleteCommandHandlerTemplate<ICommand, IRepository>
        {
            public Handler(IRepository repository)
                : base(repository)
            {
            }
        }
    }
}
