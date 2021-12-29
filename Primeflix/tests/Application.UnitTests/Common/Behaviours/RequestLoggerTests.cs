using Primeflix.Application.Common.Behaviours;
using Primeflix.Application.Common.Interfaces;
using Moq;
using NUnit.Framework;

namespace Primeflix.Application.UnitTests.Common.Behaviours;

public class RequestLoggerTests
{
    private Mock<ICurrentUserService> _currentUserService = null!;
    private Mock<IIdentityService> _identityService = null!;

    [SetUp]
    public void Setup()
    {
        _currentUserService = new Mock<ICurrentUserService>();
        _identityService = new Mock<IIdentityService>();
    }
}
