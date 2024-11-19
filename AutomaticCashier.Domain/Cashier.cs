namespace AutomaticCashier.Domain;

public class Cashier
{

    public static Change? OptimalChange(long input)
    {
        if (input == 0 ||  input == 1) return null;

        if (input == Constants.Bill10Value)
            return new() { Bill10 = 1 };

        if (input == Constants.Bill5Value)
            return new() { Bill5 = 1 };

        if (input == Constants.Coin2Value)
            return new() { Coin2 = 1 };

        long remainder = 0;
        long bill10Number = input < 10
            ? 0
            : Math.DivRem(input, 10,out remainder);
        if (remainder == 0 && bill10Number != 0) return new() { Bill10 = bill10Number };

        long secondRemainder = 0;
        long bill5Number = remainder < 5 
            ? 0
            : Math.DivRem(remainder, 5, out secondRemainder);
        if(remainder == 1)
        {
            bill10Number--;
            bill5Number = Math.DivRem(remainder + 10, 5, out secondRemainder);
        }

        long leftToChange = 0;
        if (secondRemainder == 0 && remainder == 0)
            leftToChange = input;
        else if(secondRemainder == 0 && remainder > 1) 
            leftToChange = remainder;
        else if (secondRemainder == 1)
        {
            bill5Number--;
            leftToChange = secondRemainder + 5;
        }
        else
            leftToChange = secondRemainder;

        long coin2Number = Math.DivRem(leftToChange, 2, out long thirdRemainder);

        return thirdRemainder == 0
            ? new() { Bill10 = bill10Number, Bill5 = bill5Number, Coin2 = coin2Number}
            : null;
    }
}
