using System;
using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit;

public class RandomRandomizationSourceTests
{
  [Fact]
  public void CanConstructWithoutParameter()
  {
    _ = new RandomRandomizationSource();
  }

  [Fact]
  public void CanConstructWithParameter()
  {
    var random = new Random();
    _ = new RandomRandomizationSource(random);
  }
}
