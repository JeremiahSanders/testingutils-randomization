using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json;
using LanguageExt;
using LanguageExt.Common;
using Xunit;
using Xunit.Abstractions;

namespace Jds.TestingUtils.Randomization.Tests.Unit;

public static class UrlRandomizationSourceExtensionsTests
{
  public class RandomUrlTests
  {
    public RandomUrlTests(ITestOutputHelper testOutputHelper)
    {
      TestOutputHelper = testOutputHelper;
    }

    public ITestOutputHelper TestOutputHelper { get; }

    private static Lazy<IEnumerable<object[]>> HostLengthOnlyArrangements { get; } = new(
      () =>
      {
        return new[] { 1, 5, 63, 253 }.Select(hostLength =>
        {
          var result = Prelude.Try(() => Randomizer.Shared.RandomUrl(hostLength)).Try();
          var resultUri = result.Map(url => new Uri(url, UriKind.Absolute));

          return new object[] { hostLength, result, resultUri };
        });
      });

    private static Lazy<IEnumerable<object[]>> HostPathOnlyArrangements { get; } = new(
      () =>
      {
        var hostLengths = new[] { 1, 5, 15, 25 };
        var pathLengths = new[] { 1, 5, 10, 25 };
        return hostLengths.SelectMany(hostLength => pathLengths.Select(pathLength =>
        {
          var result = Prelude.Try(() => Randomizer.Shared.RandomUrl(hostLength, pathLength)).Try();
          var resultUri = result.Map(url => new Uri(url, UriKind.Absolute));

          return new object[] { hostLength, pathLength, result, resultUri };
        }));
      });

    private static Lazy<IEnumerable<object[]>> HostPathQueryArrangements { get; } = new(
      () =>
      {
        var hostLengths = new[] { 1, 5, 15, 253 };
        var pathLengths = new[] { 1, 5, 15, 25 };
        var queryLengths = new[] { 1, 5, 15, 30 };
        return hostLengths.SelectMany(hostLength => pathLengths.SelectMany(pathLength =>
          queryLengths.Select(queryLength =>
          {
            var result = Prelude.Try(() => Randomizer.Shared.RandomUrl(hostLength, pathLength, queryLength)).Try();
            var resultUri = result.Map(url => new Uri(url, UriKind.Absolute));

            return new object[] { hostLength, pathLength, queryLength, result, resultUri };
          })));
      });

    private static Lazy<IEnumerable<object[]>> FullySpecifiedArrangements { get; } = new(
      () =>
      {
        var hostLengths = new[] { 1, 3, 5 };
        var pathLengths = new[] { 0, 5 };
        var queryLengths = new[] { 0, 5 };
        var fragmentLengths = new[] { 0, 15 };
        var schemes = new[] { Randomizer.Shared.RandomStringLatin(5) };
        var ports = new int?[] { null, 1 };

        var arrangements =
          from hostLength in hostLengths
          from pathLength in pathLengths
          from queryLength in queryLengths
          from fragmentLength in fragmentLengths
          from port in ports
          from scheme in schemes
          select (
            hostLength,
            pathLength,
            queryLength,
            fragmentLength,
            port,
            scheme
          );

        return arrangements.Select(props =>
        {
          var result = Prelude.Try(() => Randomizer.Shared.RandomUrl(props.hostLength, props.pathLength,
            props.queryLength, props.fragmentLength, props.scheme, props.port)).Try();
          var resultUri = result.Map(url => new Uri(url, UriKind.Absolute));

          return new object[] { props, result, resultUri };
        });
      });

    public static IEnumerable<object[]> HostLengthOnly => HostLengthOnlyArrangements.Value;
    public static IEnumerable<object[]> HostPathOnly => HostPathOnlyArrangements.Value;
    public static IEnumerable<object[]> HostPathQuery => HostPathQueryArrangements.Value;
    public static IEnumerable<object[]> FullySpecified => FullySpecifiedArrangements.Value;

    [Theory]
    [MemberData(memberName: nameof(FullySpecified))]
    public void GivenFullySpecified_ReturnsExpectedLengths(
      (int HostLength, int PathLength, int QueryLength, int FragmentLength, int? Port, string Scheme) arrangement,
      Result<string> result, Result<Uri> resultUri)
    {
      var expectedPathLength =
        arrangement.PathLength == 0 ? 1 : arrangement.PathLength; // Uri path, when empty, is "/" (1 char)
      var expected =
      (
        arrangement.HostLength, PathLength: expectedPathLength, arrangement.QueryLength, arrangement.FragmentLength
      );
      result.IfSucc(TestOutputHelper.WriteLine);
      resultUri.IfSucc(uri => WriteUri(TestOutputHelper, uri));

      var actual = resultUri.Map(uri => (HostLength: uri.Host.Length,
          PathLength: uri.LocalPath.Length,
          QueryLength: uri.Query.Length,
          FragmentLength: uri.Fragment.Length
        )).IfFail(ex => throw ex);

      Assert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(memberName: nameof(FullySpecified))]
    public void GivenFullySpecified_ReturnsExpectedScheme(
      (int HostLength, int PathLength, int QueryLength, int FragmentLength, int? Port, string Scheme) arrangement,
      Result<string> result, Result<Uri> resultUri)
    {
      result.IfSucc(TestOutputHelper.WriteLine);

      var actual = resultUri.Map(uri => uri.Scheme).IfFail(ex => throw ex);

      Assert.Equal(arrangement.Scheme, actual);
    }

    [Theory]
    [MemberData(memberName: nameof(FullySpecified))]
    public void GivenFullySpecified_ReturnsExpectedPort(
      (int HostLength, int PathLength, int QueryLength, int FragmentLength, int? Port, string Scheme) arrangement,
      Result<string> result, Result<Uri> resultUri)
    {
      result.IfSucc(TestOutputHelper.WriteLine);

      var actual = resultUri.Map(uri => uri.Port).IfFail(ex => throw ex);

      if (arrangement.Port != null)
      {
        Assert.Equal(arrangement.Port, actual);
      }
      else
      {
        Assert.Equal(-1, actual);
      }
    }

    [Theory]
    [MemberData(memberName: nameof(HostLengthOnly))]
    public void GivenHostLengthOnly_ReturnsExpectedLength(int hostLength, Result<string> result, Result<Uri> resultUri)
    {
      result.IfSucc(TestOutputHelper.WriteLine);

      var actual = resultUri.Map(uri => uri.Host.Length).IfFail(ex => throw ex);

      Assert.Equal(hostLength, actual);
    }

    [Theory]
    [MemberData(memberName: nameof(HostPathOnly))]
    [SuppressMessage("Usage", "xUnit1026:Theory methods should use all of their parameters", Justification = "MemberData serves multiple cases.")]
    public void GivenHostPathOnly_ReturnsExpectedHostLength(int hostLength, int pathLength, Result<string> result,
      Result<Uri> resultUri)
    {
      result.IfSucc(TestOutputHelper.WriteLine);

      var actual = resultUri.Map(uri => uri.Host.Length).IfFail(ex => throw ex);

      actual.Should().Be(hostLength);
    }

    [Theory]
    [MemberData(memberName: nameof(HostPathOnly))]
    [SuppressMessage("Usage", "xUnit1026:Theory methods should use all of their parameters", Justification = "MemberData serves multiple cases.")]
    public void GivenHostPathOnly_ReturnsExpectedPathLength(int hostLength, int pathLength, Result<string> result,
      Result<Uri> resultUri)
    {
      result.IfSucc(TestOutputHelper.WriteLine);
      resultUri.IfSucc(uri => WriteUri(TestOutputHelper, uri));
      var actual = resultUri.Map(uri => uri.LocalPath.Length).IfFail(ex => throw ex);

      actual.Should().Be(pathLength);
    }

    [Theory]
    [MemberData(memberName: nameof(HostPathQuery))]
    public void GivenHostPathQuery_ReturnsExpectedLengths(int hostLength, int pathLength, int queryLength,
      Result<string> result,
      Result<Uri> resultUri)
    {
      result.IfSucc(TestOutputHelper.WriteLine);
      resultUri.IfSucc(uri => WriteUri(TestOutputHelper, uri));

      var expectedPathLength = pathLength == 0 ? 1 : pathLength; // Uri path, when empty, is "/" (1 char)
      var expected =
      (
        HostLength: hostLength, PathLength: expectedPathLength, QueryLength: queryLength, FragmentLength: 0
      );

      var actual = resultUri.Map(uri => (HostLength: uri.Host.Length,
          PathLength: uri.LocalPath.Length,
          QueryLength: uri.Query.Length,
          FragmentLength: uri.Fragment.Length
        )).IfFail(ex => throw ex);

      actual.Should().Be(expected);
    }

    internal static void WriteUri(ITestOutputHelper testOutputHelper, Uri uri)
    {
      testOutputHelper.WriteLine(message: JsonSerializer.Serialize(value: new
      {
        uri.Authority,
        uri.Fragment,
        uri.Host,
        uri.Port,
        uri.Query,
        uri.Scheme,
        uri.AbsolutePath,
        uri.LocalPath,
        uri.Segments
      }));
    }

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData("\t")]
    [InlineData("\n")]
    [InlineData("\r\n \r\n")]
    public void GivenMissingScheme_ThrowsArgumentException(string scheme)
    {
      Assert.Throws<ArgumentException>(() =>
        Randomizer.Shared.RandomUrl(hostLength: Randomizer.Shared.IntInRange(2, 6), scheme: scheme)
      );
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(int.MinValue)]
    public void GivenHostLengthBelow1_ThrowsArgumentOutOfRangeException(int length)
    {
      Assert.Throws<ArgumentOutOfRangeException>(() =>
        Randomizer.Shared.RandomUrl(length)
      );
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(int.MinValue)]
    public void GivenPortBelow0_ThrowsArgumentOutOfRangeException(int port)
    {
      Assert.Throws<ArgumentOutOfRangeException>(() =>
        Randomizer.Shared.RandomUrl(10, port: port)
      );
    }
  }
}
