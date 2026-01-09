namespace Jds.TestingUtils.Randomization;

/// <summary>
///   Methods extending <see cref="IRandomizationSource" /> supporting the generation of date and time related values.
/// </summary>
public static class DateTimeRandomizationSourceExtensions
{
  /// <summary>
  ///   Generate a pseudo-random <see cref="System.DateOnly" />.
  /// </summary>
  /// <param name="randomizationSource">This randomization source.</param>
  /// <param name="year">
  ///   The year (<c>1</c> through <c>9999</c>). Default: Pseudo-random year within the last <c>20</c> years.
  /// </param>
  /// <param name="month">The month (<c>1</c> through <c>12</c>). Default: Pseudo-random month.</param>
  /// <param name="day">
  ///   The day (<c>1</c> through the number of days in month). Default: Pseudo-random day between <c>1</c>
  ///   and the number of days in <paramref name="month" />.
  /// </param>
  public static DateOnly DateOnly(
    this IRandomizationSource randomizationSource,
    int? year = null,
    int? month = null,
    int? day = null
  )
  {
    return WithFallback(randomizationSource,
      month ?? randomizationSource.Month(),
      year,
      day
    );

    static DateOnly WithFallback(IRandomizationSource randomizationSource,
      int month,
      int? year = null,
      int? day = null)
    {
      return new DateOnly(year ?? randomizationSource.Year(), month,
        day ?? randomizationSource.Day(month));
    }
  }

  /// <summary>
  ///   Generate a pseudo-random <see cref="System.TimeOnly" />.
  /// </summary>
  /// <param name="randomizationSource">This randomization source.</param>
  /// <param name="hour">The hour (<c>0</c> through <c>23</c>). Default: Pseudo-random hour.</param>
  /// <param name="minute">The minutes (<c>0</c> through <c>59</c>). Default: Pseudo-random minutes.</param>
  /// <param name="second">The seconds (<c>0</c> through <c>59</c>). Default: Pseudo-random seconds.</param>
  public static TimeOnly TimeOnly(
    this IRandomizationSource randomizationSource,
    int? hour = null,
    int? minute = null,
    int? second = null
  )
  {
    return new TimeOnly(hour ?? randomizationSource.Hour(), minute ?? randomizationSource.Minute(),
      second ?? randomizationSource.Second());
  }

  /// <summary>
  ///   Generate a pseudo-random <see cref="System.DateTime" /> in UTC.
  /// </summary>
  /// <param name="randomizationSource">This randomization source.</param>
  /// <param name="year">
  ///   The year (<c>1</c> through <c>9999</c>). Default: Pseudo-random year within the last <c>20</c>
  ///   years.
  /// </param>
  /// <param name="month">The month (<c>1</c> through <c>12</c>). Default: Pseudo-random month.</param>
  /// <param name="day">
  ///   The day (<c>1</c> through the number of days in month). Default: Pseudo-random day between <c>1</c>
  ///   and the number of days in <paramref name="month" />.
  /// </param>
  /// <param name="hour">The hour (<c>0</c> through <c>23</c>). Default: Pseudo-random hour.</param>
  /// <param name="minute">The minutes (<c>0</c> through <c>59</c>). Default: Pseudo-random minutes.</param>
  /// <param name="second">The seconds (<c>0</c> through <c>59</c>). Default: Pseudo-random seconds.</param>
  public static DateTime DateTimeUtc(
    this IRandomizationSource randomizationSource,
    int? year = null,
    int? month = null,
    int? day = null,
    int? hour = null,
    int? minute = null,
    int? second = null
  )
  {
    return randomizationSource.DateTime(DateTimeKind.Utc, year, month, day, hour, minute, second);
  }

  private static DateTime DateTime(
    this IRandomizationSource randomizationSource,
    DateTimeKind kind,
    int? year = null,
    int? month = null,
    int? day = null,
    int? hour = null,
    int? minute = null,
    int? second = null
  )
  {
    return WithFallback(
      randomizationSource,
      kind,
      month ?? randomizationSource.Month(),
      year,
      day,
      hour,
      minute,
      second);

    static DateTime WithFallback(
      IRandomizationSource randomizationSource,
      DateTimeKind kind,
      int month,
      int? year = null,
      int? day = null,
      int? hour = null,
      int? minute = null,
      int? second = null)
    {
      return new DateTime(
        year ?? randomizationSource.Year(),
        month,
        day ?? randomizationSource.Day(month),
        hour ?? randomizationSource.Hour(),
        minute ?? randomizationSource.Minute(),
        second ?? randomizationSource.Second(),
        kind
      );
    }
  }

  private static int Hour(this IRandomizationSource randomizationSource)
  {
    return randomizationSource.IntInRange(0, 24);
  }

  private static int Minute(this IRandomizationSource randomizationSource)
  {
    return randomizationSource.IntInRange(0, 60);
  }

  private static int Second(this IRandomizationSource randomizationSource)
  {
    return randomizationSource.IntInRange(0, 60);
  }

  private static int Year(this IRandomizationSource randomizationSource)
  {
    return System.DateTime.Today.Year + randomizationSource.IntInRange(-20, 0);
  }

  private static int Month(this IRandomizationSource randomizationSource)
  {
    return randomizationSource.IntInRange(1, 13);
  }

  private static int Day(this IRandomizationSource randomizationSource, int month)
  {
    return randomizationSource.IntInRange(1, GetExclusiveMaxDaysInMonth(month));
  }

  internal static int GetExclusiveMaxDaysInMonth(int month)
  {
    const int ex28 = 29;
    const int ex30 = 31;
    const int ex31 = 32;
    return month switch
    {
      1 => ex31,
      2 => ex28,
      3 => ex31,
      4 => ex30,
      5 => ex31,
      6 => ex30,
      7 => ex31,
      8 => ex31,
      9 => ex30,
      10 => ex31,
      11 => ex30,
      12 => ex31,
      _ => ex28
    };
  }
}
