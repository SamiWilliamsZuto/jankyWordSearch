using System.Windows.Controls;

namespace WpfApp1;

public class ButtonInGrid
{
    public char Letter { get; private set; }
    public Point Point { get; private set; }

    public Button Button { get; private set; }

    public ButtonInGrid(char letter, Point point, Button button)
    {
        Letter = letter;
        Point = point;
        Button = button;
    }

}