using System.Text.RegularExpressions;

namespace Jds.TestingUtils.Randomization;

internal static class MarkovSourceHelpers
{
  private static readonly Regex LowerAndWhitespaceOnly = new("(([a-z])|(\\ ))*", RegexOptions.Compiled);

  public static IEnumerable<string> ParseIntoWords(string source)
  {
    return LowerAndWhitespaceOnly
      .Matches(source)
      .SelectMany(item => item.Value.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries))
      .Where(word => !string.IsNullOrWhiteSpace(word));
  }
}
