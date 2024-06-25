namespace Jds.TestingUtils.Randomization;

/// <summary>
///   A <see cref="IRandomizationSource" /> which contains state.
/// </summary>
/// <remarks>
///   <para>
///     A stateful randomization source is useful for creating a randomization domain-specific language
///     which is often implemented using &quot;fluent&quot; syntax (i.e., extension methods).
///   </para>
/// </remarks>
/// <typeparam name="TState">
///   A state type. This object generally contains parameters or objects which will be used by
///   extension methods (which you define).
/// </typeparam>
public interface IStatefulRandomizationSource<out TState> : IRandomizationSource where TState : notnull
{
  /// <summary>
  ///   Gets the current randomization source state.
  /// </summary>
  TState State { get; }
}
