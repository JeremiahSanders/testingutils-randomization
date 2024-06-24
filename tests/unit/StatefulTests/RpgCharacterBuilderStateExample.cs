using System;
using System.Collections.Generic;
using System.Net.Http;
using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.StatefulTests;

/// <summary>
///   A comprehensive example of creating a state object useful for random generation,
///   implementing a domain-specific language for generating domain objects,
///   and testing the generator.
/// </summary>
public static class RpgCharacterBuilderStateExample
{
  /// <summary>
  ///   An example domain-specific language method which uses both the configuration stored in
  ///   <see cref="IStatefulRandomizationSource{TState}.State" /> and method parameters.
  /// </summary>
  /// <param name="statefulRandomizer">This randomizer instance.</param>
  /// <param name="maxNameComponentCountInclusive">
  ///   An inclusive maximum count of name components (<see cref="RpgCharacterOptions.CommonNameComponents" />)
  ///   which will be used to generate the character's <see cref="RpgCharacterOverview.Name" />.
  /// </param>
  public static string GenerateCharacterName(this IStatefulRandomizationSource<RpgCharacterOptions> statefulRandomizer,
    int maxNameComponentCountInclusive = 4)
  {
    // Note that we're using the randomizer methods ( .Enumerable() and .RandomListItem() ) exposed by
    //   the stateful randomizer which we are extending. (As compared to using Randomizer.Shared.)
    return string.Join(" ",
      statefulRandomizer.Enumerable(
        inclusiveMinCount: 1,
        exclusiveMaxCount: Math.Max(1, maxNameComponentCountInclusive + 1),
        factory: _ => statefulRandomizer.RandomListItem(statefulRandomizer.State.CommonNameComponents)
      )
    );
  }

  public static string RandomPrimaryCharacteristic(
    this IStatefulRandomizationSource<RpgCharacterOptions> statefulRandomizer)
  {
    var possibleCharacteristics = statefulRandomizer.State.AllCharacterStatistics;

    // Note that we're using the randomizer methods ( .RandomListItem() ) exposed by the stateful randomizer which
    //   we are extending. (As compared to using Randomizer.Shared.)
    return statefulRandomizer.RandomListItem(possibleCharacteristics);
  }

  public static string RandomType(this IStatefulRandomizationSource<RpgCharacterOptions> statefulRandomizer)
  {
    var possibleTypes = statefulRandomizer.State.AllCharacterTypes;

    // Note that we're using the randomizer methods ( .RandomListItem() ) exposed by the stateful randomizer which
    //   we are extending. (As compared to using Randomizer.Shared.)
    return statefulRandomizer.RandomListItem(possibleTypes);
  }

  /// <summary>
  ///   An example of using a <see cref="IStatefulRandomizationSource{TState}" /> to create complex generated output
  ///   using the contents of its state, notably by composing other methods in the same domain-specific language.
  /// </summary>
  /// <param name="statefulRandomizer">This randomizer instance.</param>
  /// <param name="maxNameComponentCountInclusive">
  ///   An inclusive maximum count of name components (<see cref="RpgCharacterOptions.CommonNameComponents" />)
  ///   which will be used to generate the character's <see cref="RpgCharacterOverview.Name" />.
  /// </param>
  public static RpgCharacterOverview GenerateRpgCharacterOverview(
    this IStatefulRandomizationSource<RpgCharacterOptions> statefulRandomizer, int maxNameComponentCountInclusive = 4)
  {
    return new RpgCharacterOverview
    {
      Name = statefulRandomizer.GenerateCharacterName(maxNameComponentCountInclusive),
      PrimaryStatistic = statefulRandomizer.RandomPrimaryCharacteristic(),
      Type = statefulRandomizer.RandomType()
    };
  }

  /// <summary>
  ///   An example xUnit test class which verifies the functionality of the example bespoke domain-specific language
  ///   (implemented in this example as <see cref="RpgCharacterBuilderStateExample.GenerateRpgCharacterOverview" />).
  /// </summary>
  public class GenerateRpgCharacterOverviewAssertions
  {
    [Fact]
    public void GivenDefaultOptions_GeneratesCharactersWithAllPropertiesPopulated()
    {
      var randomizer = Randomizer.OfState(new RpgCharacterOptions());

      var character = randomizer.GenerateRpgCharacterOverview();

      Assert.False(string.IsNullOrWhiteSpace(character.Name));
      Assert.False(string.IsNullOrWhiteSpace(character.PrimaryStatistic));
      Assert.False(string.IsNullOrWhiteSpace(character.Type));
    }

    [Fact]
    public void GivenConstrainedNameChoices_GeneratesExpectedName()
    {
      const string onlyNameOption = "Bob";
      var randomizer = Randomizer.OfState(new RpgCharacterOptions { CommonNameComponents = new[] { onlyNameOption } });

      var character = randomizer.GenerateRpgCharacterOverview();

      Assert.Contains(onlyNameOption, character.Name);
    }

    [Fact]
    public void GivenConstrainedNameChoicesAndCount_GeneratesExpectedName()
    {
      const string onlyNameOption = "Bob";
      var randomizer = Randomizer.OfState(new RpgCharacterOptions { CommonNameComponents = new[] { onlyNameOption } });

      var character = randomizer.GenerateRpgCharacterOverview(1);

      Assert.Equal(onlyNameOption, character.Name);
    }
  }

  /// <summary>
  ///   An example state object which contains parameters used by a bespoke domain-specific language (implemented in this
  ///   example as <see cref="RpgCharacterBuilderStateExample.GenerateRpgCharacterOverview" />).
  /// </summary>
  public record RpgCharacterOptions
  {
    public IReadOnlyList<string> AllCharacterTypes { get; init; } = new[] { "Offense", "Defense", "Support" };

    public IReadOnlyList<string> AllCharacterStatistics { get; init; } = new[] { "Strength", "Agility", "Mind" };

    public IReadOnlyList<string> CommonNameComponents { get; init; } =
      new[] { "Blink", "Fog", "Hyper", "Musk", "Tea", "Yawn" };
  }

  /// <summary>
  ///   A simple value object showing a custom randomization output.
  /// </summary>
  /// <remarks>
  ///   <para>
  ///     In a real-world scenario, this might instead be any type which you wish to generate
  ///     from your domain-specific language.
  ///   </para>
  ///   <para>Examples:</para>
  ///   <para>You might generate a <see cref="HttpRequestMessage" /> for use in an ASP.NET Core HTTP API.</para>
  ///   <para>You might generate a <see cref="string" /> or other primitive.</para>
  /// </remarks>
  public record RpgCharacterOverview
  {
    public string Name { get; init; } = string.Empty;
    public string PrimaryStatistic { get; init; } = string.Empty;
    public string Type { get; init; } = string.Empty;
  }
}
