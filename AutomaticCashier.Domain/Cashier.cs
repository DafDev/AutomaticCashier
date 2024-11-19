namespace AutomaticCashier.Domain;

public class Cashier
{

    public static Change? OptimalChange(long input) => input switch
    {
        0 or 1 => null,
        Constants.Coin2Value => new() { Coin2 = 1 },
        Constants.Bill5Value => new() { Bill5 = 1 },
        Constants.Bill10Value => new() { Bill10 = 1 },
        _ => ComputeChange(input)
    };

    private static Change? ComputeChange(long input)
    {
        long remainder = 0;
        long bill10Number = input < Constants.Bill10Value
            ? 0
            : Math.DivRem(input, Constants.Bill10Value, out remainder);
        if (remainder == 0 && bill10Number != 0) return new() { Bill10 = bill10Number };

        long secondRemainder = 0;
        long bill5Number = remainder < Constants.Bill5Value
            ? 0
            : Math.DivRem(remainder, Constants.Bill5Value, out secondRemainder);
        if (remainder == 1)
        {
            bill10Number--;
            bill5Number = Math.DivRem(remainder + Constants.Bill10Value, Constants.Bill5Value, out secondRemainder);
        }

        long leftToChange;
        if (secondRemainder == 0 && remainder == 0)
            leftToChange = input;
        else if (secondRemainder == 0 && remainder > 1)
            leftToChange = remainder;
        else if (secondRemainder == 1)
        {
            bill5Number--;
            leftToChange = secondRemainder + Constants.Bill5Value;
        }
        else
            leftToChange = secondRemainder;

        long coin2Number = Math.DivRem(leftToChange, Constants.Coin2Value, out long thirdRemainder);

        return thirdRemainder == 0
            ? new() { Bill10 = bill10Number, Bill5 = bill5Number, Coin2 = coin2Number }
            : null;
    }
}
