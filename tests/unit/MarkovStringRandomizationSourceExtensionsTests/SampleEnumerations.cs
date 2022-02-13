using System.Collections.Generic;

namespace Jds.TestingUtils.Randomization.Tests.Unit.MarkovStringRandomizationSourceExtensionsTests;

internal static class SampleEnumerations
{
  public static IReadOnlyList<string> Fruit { get; } = new[]
  {
    "apple", "apricot", "avocado", "banana", "blackberry", "blackcurrant", "blueberry", "boysenberry", "cantaloupe",
    "caper", "cherry", "cranberry", "elderberry", "fig", "gooseberry", "grape", "grapefruit", "guava", "jujube",
    "kiwi", "kumquat", "lemon", "lime", "lychee", "mango", "mulberry", "olive", "orange", "papaya", "pear",
    "persimmon", "pineapple", "plantain", "plum", "pomegranate", "raspberry", "starfruit", "strawberry", "tangerine",
    "watermelon"
  };
}
