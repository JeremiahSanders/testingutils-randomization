using System.Diagnostics.CodeAnalysis;

namespace Jds.TestingUtils.Randomization;

/// <summary>
///   Facade for using <see cref="Jds.TestingUtils.Randomization" /> and accessing pseudo random providers.
/// </summary>
[SuppressMessage("ReSharper", "IdentifierTypo")]
public static class Randomizer
{
  /// <summary>
  ///   Gets a thread-safe, shared randomization source, which uses <see cref="System.Random.Shared" /> to provide values.
  /// </summary>
  public static IRandomizationSource Shared => RandomRandomizationSource.Shared;

  /// <summary>
  ///   Creates a <see cref="IStatefulRandomizationSource{TState}" /> using <paramref name="randomizationSource" />.
  /// </summary>
  /// <param name="randomizationSource">A <see cref="IRandomizationSource" />.</param>
  /// <param name="state">An initial <see cref="IStatefulRandomizationSource{TState}.State" />.</param>
  /// <typeparam name="TState"></typeparam>
  /// <returns></returns>
  public static IStatefulRandomizationSource<TState> WithState<TState>(IRandomizationSource randomizationSource,
    TState state) where TState : notnull
  {
    return new StatefulSource<TState>(randomizationSource, state);
  }

  /// <summary>
  ///   Creates a <see cref="IStatefulRandomizationSource{TState}" /> using the static, thread-safe
  ///   <see cref="Shared" /> source to provide random values.
  /// </summary>
  /// <param name="state">An initial <see cref="IStatefulRandomizationSource{TState}.State" />.</param>
  /// <typeparam name="TState"></typeparam>
  /// <returns></returns>
  public static IStatefulRandomizationSource<TState> WithState<TState>(TState state) where TState : notnull
  {
    return WithState(Shared, state);
  }
}
