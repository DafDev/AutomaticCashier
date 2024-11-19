using FluentAssertions;

namespace AutomaticCashier.Domain.Test;

public class ChangeTest
{
    [Theory]
    [MemberData(nameof(Data))]
    public void ChangeShouldGetValue(long input, Change? expected)
    {
           
        // Act
        var actual = Cashier.OptimalChange(input);

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    public static TheoryData<long, Change?> Data()
    {
        return new()
        {
            { 0, null },
            { 1, null },
            { 10, new Change{ Bill10 = 1 } },
            { 5, new Change{ Bill5 = 1 } },
            { 2, new Change{ Coin2 = 1 } },
            { 6, new Change{ Coin2 = 3 } },
            { 11, new Change{ Coin2 = 3, Bill5=1 } },
            { 21, new Change{ Coin2 = 3, Bill5=1 , Bill10 = 1} },
            { 22, new Change{ Coin2 = 1, Bill10 = 2} },
        };
    }
}