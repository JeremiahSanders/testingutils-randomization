namespace Jds.TestingUtils.Randomization;

public static class ReadOnlyDictionaryExtensions
{
  /// <inheritdoc
  ///   cref="SelectionRandomizationSourceExtensions.WeightedRandomKey{T}(Jds.TestingUtils.Randomization.IRandomizationSource,System.Collections.Generic.IReadOnlyDictionary{T,double})" />
  public static T GetWeightedRandomKey<T>(this IReadOnlyDictionary<T, double> weightedKeys,
    IRandomizationSource randomizationSource)
  {
    return randomizationSource.WeightedRandomKey(weightedKeys);
  }

  /// <inheritdoc
  ///   cref="SelectionRandomizationSourceExtensions.WeightedRandomKey{T}(Jds.TestingUtils.Randomization.IRandomizationSource,System.Collections.Generic.IReadOnlyDictionary{T,double})" />
  public static T GetWeightedRandomKey<T>(this IReadOnlyDictionary<T, double> weightedKeys)
  {
    return Randomizer.Shared.WeightedRandomKey(weightedKeys);
  }

  /// <inheritdoc
  ///   cref="SelectionRandomizationSourceExtensions.WeightedRandomKey{T}(Jds.TestingUtils.Randomization.IRandomizationSource,System.Collections.Generic.IReadOnlyDictionary{T,int})" />
  public static T GetWeightedRandomKey<T>(this IReadOnlyDictionary<T, int> weightedKeys,
    IRandomizationSource randomizationSource)
  {
    return randomizationSource.WeightedRandomKey(weightedKeys);
  }

  /// <inheritdoc
  ///   cref="SelectionRandomizationSourceExtensions.WeightedRandomKey{T}(Jds.TestingUtils.Randomization.IRandomizationSource,System.Collections.Generic.IReadOnlyDictionary{T,int})" />
  public static T GetWeightedRandomKey<T>(this IReadOnlyDictionary<T, int> weightedKeys)
  {
    return Randomizer.Shared.WeightedRandomKey(weightedKeys);
  }
}
