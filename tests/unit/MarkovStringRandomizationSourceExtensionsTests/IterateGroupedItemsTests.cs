using System.Linq;
using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.MarkovStringRandomizationSourceExtensionsTests;

public class IterateGroupedItemsTests
{
  [Fact]
  public void ReturnsExpectedResults()
  {
    var items = new[] { 1, 2, 3, 4, 5 };
    var itemsPerGroup = 3;
    var expected = new[]
    {
      (Items: new[] { 1, 2, 3 }, Index: 0), (Items: new[] { 2, 3, 4 }, Index: 1), (Items: new[] { 3, 4, 5 }, Index: 2)
    };

    var actual = MarkovStringRandomizationSourceExtensions.IterateGroupedItems(items, itemsPerGroup).ToArray();

    Assert.Equal(expected.Length, actual.Length);
    foreach (var ((expectedItems, expectedIndex), (actualItems, actualIndex)) in expected.Zip(actual))
    {
      Assert.Equal(expectedIndex, actualIndex);
      Assert.True(condition: expectedItems.SequenceEqual(actualItems));
    }
  }
}
