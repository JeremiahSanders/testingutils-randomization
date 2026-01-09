using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Jds.TestingUtils.Randomization.Tests.Unit.ConditionalGenerationExtensionsTests;

public class GenerateUntilAsyncTests
{
  [Fact]
  public async Task ReturnsValue()
  {
    var multipleOfThree = Randomizer.Shared.GenerateUntilAsync(
      static async source =>
      {
        await Task.Delay(1);
        return source.IntPositive();
      },
      static async value =>
      {
        await Task.Delay(1);
        return value > 0 && value % 3 == 0;
      });

    var remainderOfThree = await multipleOfThree % 3;
    Assert.Equal(0, remainderOfThree);
  }

  [Fact]
  public async Task RetriesOnFalse_AsyncPredicate()
  {
    var callCount = 0;
    const int plannedFailures = 3;
    const int expectedMiniumCalls = plannedFailures + 1;
    var multipleOfThree = await Randomizer.Shared.GenerateUntilAsync(
      static async source =>
      {
        await Task.Delay(1);
        return source.IntPositive();
      },
      async value =>
      {
        await Task.Delay(1);
        callCount++;
        var isMatch = callCount > plannedFailures && value > 0 && value % 3 == 0;
        return isMatch;
      });

    var remainderOfThree = multipleOfThree % 3;
    Assert.Equal(0, remainderOfThree);
    Assert.True(callCount >= expectedMiniumCalls);
  }

  [Fact]
  public async Task RetriesOnFalse_SyncPredicate()
  {
    var callCount = 0;
    const int plannedFailures = 3;
    const int expectedMiniumCalls = plannedFailures + 1;
    var multipleOfThree = await Randomizer.Shared.GenerateUntilAsync(
      static async source =>
      {
        await Task.Delay(1);
        return source.IntPositive();
      },
      value =>
      {
        callCount++;
        var isMatch = callCount > plannedFailures && value > 0 && value % 3 == 0;
        return isMatch;
      });

    var remainderOfThree = multipleOfThree % 3;
    Assert.Equal(0, remainderOfThree);
    Assert.True(callCount >= expectedMiniumCalls);
  }

  [Fact]
  public async Task ThrowsWhenCancelled_AsyncPredicate()
  {
    var cxlSource = new CancellationTokenSource();
    await cxlSource.CancelAsync();

    await Assert.ThrowsAsync<OperationCanceledException>(ActAsync);
    return;

    Task<int> ActAsync()
    {
      return Randomizer.Shared.GenerateUntilAsync(
        static async source =>
        {
          await Task.Delay(1);
          return source.IntPositive();
        },
        static async value =>
        {
          await Task.Delay(1);
          return value > 0 && value % 3 == 0;
        }, cxlSource.Token);
    }
  }

  [Fact]
  public async Task ThrowsWhenCancelled_SyncPredicate()
  {
    var cxlSource = new CancellationTokenSource();
    await cxlSource.CancelAsync();

    await Assert.ThrowsAsync<OperationCanceledException>(ActAsync);
    return;

    Task<int> ActAsync()
    {
      return Randomizer.Shared.GenerateUntilAsync(
        static async source =>
        {
          await Task.Delay(1);
          return source.IntPositive();
        },
        static value => value > 0 && value % 3 == 0, cxlSource.Token);
    }
  }
}
