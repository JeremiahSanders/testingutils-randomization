using System;
using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using LanguageExt.Common;
using Xunit;
using Xunit.Abstractions;

namespace Jds.TestingUtils.Randomization.Tests.Unit;

public static class DomainRandomizationSourceExtensionsTests
{
  public class DomainLabelTests
  {
    [Theory]
    [InlineData(Int32.MaxValue)]
    [InlineData(64)]
    [InlineData(100)]
    [InlineData(250)]
    public void GivenLengthOver63_ThrowsArgumentOutOfRangeException(int length)
    {
      Assert.Throws<ArgumentOutOfRangeException>(() =>
        DomainRandomizationSourceExtensions.DomainLabel(Randomizer.Shared, length)
      );
    }

    [Theory]
    [InlineData(Int32.MinValue)]
    [InlineData(-1)]
    [InlineData(0)]
    public void GivenLengthBelow1_ThrowsArgumentOutOfRangeException(int value)
    {
      Assert.Throws<ArgumentOutOfRangeException>(() =>
        DomainRandomizationSourceExtensions.DomainLabel(Randomizer.Shared, value)
      );
    }
  }

  public class DomainNameTests
  {
    public ITestOutputHelper TestOutputHelper { get; }
    public IRandomizationSource RandomizationSource { get; }

    public DomainNameTests(ITestOutputHelper testOutputHelper)
    {
      TestOutputHelper = testOutputHelper;
      RandomizationSource = Randomizer.Shared;
    }

    private static Lazy<IEnumerable<object[]>> CreateDomainLabelLengthsTestCasesLazy { get; } =
      new Lazy<IEnumerable<object[]>>(
        () =>
        {
          const int testCaseCount = 125;
          return Enumerable.Range(1, testCaseCount).Select(_ =>
          {
            var lengths = Randomizer.Shared.GenerateLabelLengths(Randomizer.Shared.IntInRange(1, 253));
            Result<string> result = Prelude.Try(() => Randomizer.Shared.DomainName(lengths)).Try();
            return new object[] { lengths, result };
          }).ToArray();
        });

    private static Lazy<IEnumerable<object[]>> CreateDomainLengthTestCasesLazy { get; } =
      new Lazy<IEnumerable<object[]>>(
        () =>
        {
          var results = Enumerable.Range(1, 253).Select(caseLength =>
          {
            Result<string> result = Prelude.Try(() => Randomizer.Shared.DomainName(caseLength)).Try();
            return new object[] { caseLength, result };
          }).ToArray();

          return results;
        });

    public static IEnumerable<object[]> CreateDomainLengthTestCases()
    {
      return CreateDomainLengthTestCasesLazy.Value;
    }

    public static IEnumerable<object[]> CreateDomainLabelLengthsTestCases()
    {
      return CreateDomainLabelLengthsTestCasesLazy.Value;
    }

    private static void AssertDomainNameAppearsValid(string result)
    {
      var splitOnDot = result.Split('.');

      Assert.True(result.Length <= 253);
      Assert.All(splitOnDot, domainLabel =>
      {
        Assert.True(domainLabel.Length <= 63);

        if (domainLabel.Length >= 4)
        {
          var thirdAndFourth = domainLabel.Substring(2, 2);
          Assert.NotEqual("--", thirdAndFourth);
        }

        var firstChar = domainLabel.First();
        Assert.Contains(firstChar, CharCollections.LatinAlpha);

        if (result.Length > 1)
        {
          var lastChar = domainLabel.Last();
          Assert.Contains(lastChar, CharCollections.LettersDigits);
        }
      });
    }

    [Theory]
    [MemberData(nameof(CreateDomainLengthTestCases))]
    public void GivenDomainNameLength_ReturnsExpectedLength(int requestedLength, Result<string> result)
    {
      TestOutputHelper.WriteLine($"Validating expected length. Requested: {requestedLength}");
      var actual = result.IfFail(exc => throw exc)!;
      TestOutputHelper.WriteLine(
        $"Expected length: {requestedLength}; Actual length: {actual.Length}; Actual domain: {actual}"
      );

      Assert.Equal(requestedLength, actual.Length);
    }

    [Theory]
    [MemberData(nameof(CreateDomainLengthTestCases))]
    public void GivenDomainNameLength_GeneratedDomainsAppearValid(int requestedLength, Result<string> result)
    {
      TestOutputHelper.WriteLine($"Validating domain validity (approximating RFC-1053). Requested: {requestedLength}");
      var actual = result.IfFail(exc => throw exc)!;
      TestOutputHelper.WriteLine($"Actual domain: {actual}");

      AssertDomainNameAppearsValid(actual);
    }

    [Theory]
    [MemberData(nameof(CreateDomainLabelLengthsTestCases))]
    public void GivenLabelLengths_ReturnsExpectedLength(IReadOnlyList<int> distribution, Result<string> result)
    {
      var expected = distribution.Sum() + (distribution.Count - 1);

      TestOutputHelper.WriteLine(
        $"Validating expected length. Expected: {expected} ({String.Join(",", distribution)})");
      var actual = result.IfFail(exc => throw exc)!;
      TestOutputHelper.WriteLine(
        $"Expected length: {expected}; Actual length: {actual.Length}; Actual domain: {actual}"
      );

      Assert.Equal(expected, actual.Length);
    }

    [Theory]
    [MemberData(nameof(CreateDomainLabelLengthsTestCases))]
    public void GivenLabelLengths_ReturnsExpectedLabelLengths(IReadOnlyList<int> distribution, Result<string> result)
    {
      TestOutputHelper.WriteLine($"Validating component lengths. Distribution: ({String.Join(",", distribution)})");
      var actual = result.IfFail(exc => throw exc)!.Split(".");
      TestOutputHelper.WriteLine(
        $"Expected: {String.Join(",", distribution)}; Actual: {String.Join(",", actual)}"
      );
      var resultLengths = actual.Select(item => item.Length).ToArray();

      Assert.Equal(distribution, resultLengths);
    }

    [Theory]
    [MemberData(nameof(CreateDomainLabelLengthsTestCases))]
    public void GivenLabelLengths_GeneratedDomainsAppearValid(IReadOnlyList<int> distribution, Result<string> result)
    {
      TestOutputHelper.WriteLine(
        $"Validating domain validity (approximating RFC-1053). Requested: {String.Join(",", distribution)}");
      var actual = result.IfFail(exc => throw exc)!;
      TestOutputHelper.WriteLine($"Actual domain: {actual}");

      AssertDomainNameAppearsValid(actual);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(int.MinValue)]
    public void GivenDomainLengthLessThan1_ThrowsArgumentOutOfRange(int length)
    {
      Assert.Throws<ArgumentOutOfRangeException>(() => Randomizer.Shared.DomainName(length));
    }

    [Theory]
    [InlineData(254)]
    [InlineData(int.MaxValue)]
    public void GivenDomainLengthGreaterThan253_ThrowsArgumentOutOfRange(int length)
    {
      Assert.Throws<ArgumentOutOfRangeException>(() => Randomizer.Shared.DomainName(length));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(int.MinValue)]
    public void GivenLabelLengthsLessThan1_ThrowsArgumentOutOfRange(int length)
    {
      Assert.Throws<ArgumentOutOfRangeException>(() => Randomizer.Shared.DomainName(new[] { length }));
    }

    [Theory]
    [InlineData(64)]
    [InlineData(int.MaxValue)]
    public void GivenLabelLengthsGreaterThan63_ThrowsArgumentOutOfRange(int length)
    {
      Assert.Throws<ArgumentOutOfRangeException>(() => Randomizer.Shared.DomainName(new[] { length }));
    }

    [Fact]
    public void GivenNoLabelLengths_ThrowsArgumentException()
    {
      Assert.Throws<ArgumentException>(() => Randomizer.Shared.DomainName(Array.Empty<int>()));
    }

    [Theory]
    [InlineData("a--bDEF")]
    [InlineData("Y--Zabc")]
    public void IsDomainLabelMiddleInvalid_RecognizesInvalidMiddles(string invalidMiddle)
    {
      Assert.True(DomainRandomizationSourceExtensions.IsDomainLabelMiddleInvalid(invalidMiddle));
    }

    [Theory]
    [InlineData("a--bDEF")]
    [InlineData("Y--Zabc")]
    public void RegenerateLabelMiddleWithAlphaIfInvalid_ReplacesInvalidMiddlesWithLetters(string invalidMiddle)
    {
      var result =
        DomainRandomizationSourceExtensions.RegenerateLabelMiddleWithAlphaIfInvalid(RandomRandomizationSource.Shared,
          invalidMiddle);

      var secondAndThird = result.Skip(1).Take(2);
      Assert.All(secondAndThird, character => Assert.NotEqual('-', character));
    }
  }
}
