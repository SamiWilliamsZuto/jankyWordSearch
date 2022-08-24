using System.Windows.Controls;

namespace WpfApp1;

public class ButtonInGrid
{
    public char Letter { get; private set; }
    public Point Point { get; private set; }
    public Button Button { get; private set; }

    public ButtonInGrid(char letter, Point point)
    {
        Letter = letter;
        Point = point;
    }

    public void AddButton(Button button)
    {
        Button = button;
    }

    public void ChangeLetter(char letter)
    {
        Letter = letter;
    }
}