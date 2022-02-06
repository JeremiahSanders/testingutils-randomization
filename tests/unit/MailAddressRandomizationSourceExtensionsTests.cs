using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using LanguageExt;
using LanguageExt.Common;
using Xunit;
using Xunit.Abstractions;

namespace Jds.TestingUtils.Randomization.Tests.Unit;

public static class MailAddressRandomizationSourceExtensionsTests
{
  public class MailAddressAddrSpecLocalPartTests
  {
    [Theory]
    [InlineData(int.MinValue)]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(65)]
    [InlineData(66)]
    [InlineData(100)]
    [InlineData(254)]
    [InlineData(255)]
    [InlineData(256)]
    [InlineData(int.MaxValue)]
    public void GivenLength_OutOfRange_ThrowsArgumentOutOfRangeException(int length)
    {
      Assert.Throws<ArgumentOutOfRangeException>(() =>
        Randomizer.Shared.MailAddressAddrSpecLocalPart(length)
      );
    }
  }

  public class MailAddressAddrSpecDomainTests
  {
    [Theory]
    [InlineData(int.MinValue)]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(254)]
    [InlineData(255)]
    [InlineData(256)]
    [InlineData(int.MaxValue)]
    public void GivenLength_OutOfRange_ThrowsArgumentOutOfRangeException(int length)
    {
      Assert.Throws<ArgumentOutOfRangeException>(() =>
        Randomizer.Shared.MailAddressAddrSpecDomain(length)
      );
    }
  }

  public class MailAddressAddrSpecTests
  {
    public ITestOutputHelper TestOutputHelper { get; }

    private static Lazy<IEnumerable<object[]>> CreatePartLengthsCases { get; } = new Lazy<IEnumerable<object[]>>(() =>
    {
      var localPartLengths = new[] { 1, 5, 10, 20, 25, 64 };
      var hostDomainLengths = new[] { 1, 5, 10, 15, 20, 60, 100, 253 };
      var cases = localPartLengths.SelectMany(localPart =>
          hostDomainLengths.Select(hostDomain => (LocalPart: localPart, Domain: hostDomain)))
        .Where(pair => pair.LocalPart + pair.Domain + 1 <= 254)
        .ToArray();
      var results = cases.Select(caseValues =>
        {
          var result = Prelude.Try(() =>
              Randomizer.Shared.MailAddressAddrSpec((caseValues.LocalPart, caseValues.Domain))
            )
            .Try();
          return new object[] { caseValues, result };
        })
        .ToArray();
      return results;
    });

    private static Lazy<IEnumerable<object[]>> CreateAddressLengthCases { get; } = new Lazy<IEnumerable<object[]>>(() =>
    {
      var lengths = new[] { 3, 4, 5, 8, 10, 12, 15, 20, 25, 30, 60, 100, 253 };
      var results = lengths.Select(length =>
        {
          var result = Prelude.Try(() =>
              Randomizer.Shared.MailAddressAddrSpec((length))
            )
            .Try();
          return new object[] { length, result };
        })
        .ToArray();
      return results;
    });

    public MailAddressAddrSpecTests(ITestOutputHelper testOutputHelper)
    {
      TestOutputHelper = testOutputHelper;
    }

    public static IEnumerable<object[]> GetPartLengthsCases()
    {
      return CreatePartLengthsCases.Value;
    }

    public static IEnumerable<object[]> GetAddressLengthCases()
    {
      return CreateAddressLengthCases.Value;
    }

    [Theory]
    [MemberData(nameof(GetAddressLengthCases))]
    public void GivenAddressLength_ReturnsExpectedLength(int length, Result<string> result)
    {
      var expected = length;

      TestOutputHelper.WriteLine($"Testing length from requested address length. Length: {length}");

      var actual = result.Map(address => address.Length).IfFail(ex => throw ex);

      Assert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(GetPartLengthsCases))]
    public void GivenPartLengths_ReturnsExpectedLength((int LocalPartLength, int DomainLength) request,
      Result<string> result)
    {
      var expected = request.DomainLength + 1 + request.LocalPartLength;

      TestOutputHelper.WriteLine(
        $"Testing length from requested part lengths. Local part: {request.LocalPartLength}; Host domain: {request.DomainLength}; Total expected: {expected}");

      var actual = result.Map(address => address.Length).IfFail(ex => throw ex);

      Assert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(GetPartLengthsCases))]
    public void GivenPartLengths_ReturnsExpectedPartLengths((int LocalPartLength, int DomainLength) request,
      Result<string> result)
    {
      TestOutputHelper.WriteLine(
        $"Testing length from requested part lengths. Local part: {request.LocalPartLength}; Host domain: {request.DomainLength}");

      var parts = result.IfFail(ex => throw ex).Split('@');
      var localPart = parts[0];
      var domain = parts[1];

      Assert.Equal(request.LocalPartLength, localPart.Length);
      Assert.Equal(request.DomainLength, domain.Length);
    }

    [Theory]
    [MemberData(nameof(GetPartLengthsCases))]
    public void GivenPartLengths_CanBeConvertedToMailAddress((int LocalPartLength, int DomainLength) request,
      Result<string> result)
    {
      TestOutputHelper.WriteLine(
        $"Testing length from requested part lengths. Local part: {request.LocalPartLength}; Host domain: {request.DomainLength}");

      var addressValue = result.IfFail(ex => throw ex)!;

      TestOutputHelper.WriteLine($"Received address: {addressValue}");

      var actual = new MailAddress(addressValue);

      Assert.Equal(addressValue, actual.Address);
    }

    [Theory]
    [InlineData(-1, 1)]
    [InlineData(1, -1)]
    [InlineData(MailAddressRandomizationSourceExtensions.MailAddressConstants.SmtpLocalPartMaxLength,
      MailAddressRandomizationSourceExtensions.MailAddressConstants.SmtpDomainMaxLength)]
    public void GivenPartLengths_OutOfRange_ThrowsArgumentOutOfRangeException(int localPartLength, int domainLength)
    {
      Assert.Throws<ArgumentOutOfRangeException>(() =>
        Randomizer.Shared.MailAddressAddrSpec((localPartLength, domainLength))
      );
    }

    [Theory]
    [InlineData(int.MinValue)]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(255)]
    [InlineData(256)]
    [InlineData(int.MaxValue)]
    public void GivenAddressLength_OutOfRange_ThrowsArgumentOutOfRangeException(int length)
    {
      Assert.Throws<ArgumentOutOfRangeException>(() =>
        Randomizer.Shared.MailAddressAddrSpec((length))
      );
    }
  }

  public class MailAddressTests
  {
    public MailAddressTests(ITestOutputHelper testOutputHelper)
    {
      TestOutputHelper = testOutputHelper;
    }

    public ITestOutputHelper TestOutputHelper { get; }

    public static IEnumerable<object[]> GetAddressLengthCases()
    {
      return CreateAddressLengthCases.Value;
    }

    public static IEnumerable<object[]> GetParameterlessCases()
    {
      return CreateParameterlessCases.Value;
    }

    private static Lazy<IEnumerable<object[]>> CreateAddressLengthCases { get; } = new Lazy<IEnumerable<object[]>>(() =>
    {
      var lengths = new[] { 3, 4, 5, 8, 10, 12, 15, 20, 25, 30, 60, 100, 253 };
      var results = lengths.Select(length =>
        {
          var result = Prelude.Try(() =>
              Randomizer.Shared.MailAddress(length)
            )
            .Try();
          return new object[] { length, result };
        })
        .ToArray();
      return results;
    });

    private static Lazy<IEnumerable<object[]>> CreateParameterlessCases { get; } = new Lazy<IEnumerable<object[]>>(() =>
    {
      const int testCount = 100;
      var results = Enumerable.Range(1, testCount).Select(_ =>
        {
          var result = Prelude.Try(() =>
              Randomizer.Shared.MailAddress()
            )
            .Try();
          return new object[] { result };
        })
        .ToArray();
      return results;
    });

    [Theory]
    [MemberData(nameof(GetParameterlessCases))]
    public void GivenNoLength_ReturnsNonNull(Result<MailAddress> result)
    {
      var actual = result.IfFail(ex => throw ex);

      Assert.NotNull(actual);
    }

    [Theory]
    [MemberData(nameof(GetAddressLengthCases))]
    public void GivenAddressLength_ReturnsExpectedLength(int length, Result<MailAddress> result)
    {
      var expected = length;

      TestOutputHelper.WriteLine($"Testing length from requested address length. Length: {length}");

      var actual = result.Map(mailAddress => mailAddress.Address.Length).IfFail(ex => throw ex);

      Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(int.MinValue)]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(255)]
    [InlineData(256)]
    [InlineData(int.MaxValue)]
    public void GivenAddressLength_OutOfRange_ThrowsArgumentOutOfRangeException(int length)
    {
      Assert.Throws<ArgumentOutOfRangeException>(() =>
        Randomizer.Shared.MailAddress((length))
      );
    }
  }
}
