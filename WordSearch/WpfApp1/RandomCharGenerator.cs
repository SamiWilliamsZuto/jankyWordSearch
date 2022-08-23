using System;

namespace WpfApp1;

public class RandomCharGenerator
{
    public char GenerateRandomChar()
    {
        var randomChar = new Random().Next(65, 90 + 1);
        return (char) randomChar;
    }
}