using System.Collections.Generic;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Jds.TestingUtils.Randomization.Tests.Unit.StringRandomizationSourceExtensionsTests;

public class RandomStringLatinTests
{
  private readonly ITestOutputHelper _outputHelper;

  public RandomStringLatinTests(ITestOutputHelper outputHelper)
  {
    _outputHelper = outputHelper;
  }

  public static IEnumerable<object[]> CreateTestCases()
  {
    object[] GenerateTestCase(bool uppercase, bool alphanumeric)
    {
      return new object[] {uppercase, alphanumeric};
    }

    return new[]
    {
      GenerateTestCase(false, false), GenerateTestCase(false, true), GenerateTestCase(true, false),
      GenerateTestCase(true, true)
    };
  }

  [Theory]
  [MemberData(nameof(CreateTestCases))]
  public void GeneratesExpectedStringTypes(bool uppercase, bool alphanumeric)
  {
    var value = RandomRandomizationSource.Shared.RandomStringLatin(
      Randomizer.Shared.IntInRange(100, 1000),
      uppercase,
      alphanumeric
    );

    _outputHelper.WriteLine($"Checking for {value}");
    CheckResult(value);
    return;

    void CheckResult(string actual)
    {
      if (uppercase)
      {
        if (alphanumeric)
        {
          actual.Should().NotBeLowerCased();
        }
        else
        {
          actual.Should().BeUpperCased();
        }
      }
      else
      {
        if (alphanumeric)
        {
          actual.Should().NotBeUpperCased();
        }
        else
        {
          actual.Should().BeLowerCased();
        }
      }
    }
  }
}
