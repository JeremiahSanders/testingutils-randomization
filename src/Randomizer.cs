using System.Diagnostics.CodeAnalysis;

namespace Jds.TestingUtils.Randomization;

/// <summary>
///   Facade for using <see cref="Jds.TestingUtils.Randomization" /> and accessing pseudo random providers.
/// </summary>
[SuppressMessage(category: "ReSharper", checkId: "IdentifierTypo")]
public static class Randomizer
{
  /// <summary>
  ///   Gets a thread-safe, shared randomization source, which uses <see cref="System.Random.Shared" /> to provide values.
  /// </summary>
  public static IRandomizationSource Shared => RandomRandomizationSource.Shared;
}
