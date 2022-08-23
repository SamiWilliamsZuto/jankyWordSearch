using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp1;

public class UiCreator
{
    private readonly RandomCharGenerator _randomCharGenerator;

    public UiCreator(RandomCharGenerator randomCharGenerator)
    {
        _randomCharGenerator = randomCharGenerator;
    }

    public void CreateGridButtons(Canvas buttonCanvas, ButtonInGrid[,] buttonArray, RoutedEventHandler buttonClickFunction)
    {
        const int buttonWidth = 64;
        const int buttonHeight = 64;

        for (int x = 0; x < MainWindow.GridWidth; x++)
        {
            for (int y = 0; y < MainWindow.GridHeight; y++)
            {
                Button btn = new Button
                {
                    Name = $"button{x}{y}",
                    Tag = new Point(x, y),
                    Content = _randomCharGenerator.GenerateRandomChar(),
                    Height = buttonHeight,
                    Width = buttonWidth,
                    Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                    FontSize = 12,
                    BorderThickness = new Thickness(4)
                };
                btn.Click += buttonClickFunction;

                buttonArray[x, y] = new ButtonInGrid((char)btn.Content, new Point(x, y), btn);

                buttonCanvas.Children.Add(btn);
                Canvas.SetLeft(btn, x * buttonWidth + 10);
                Canvas.SetTop(btn, y * buttonHeight + 10);
            }
        }
    }
    
    public void CreateClearButton(Canvas buttonCanvas, RoutedEventHandler buttonClickFunction)
    {
        Button btn = new Button
        {
            Name = "clearButton",
            Content = "Clear Selection",
            Height = 64,
            Width = 128,
            Background = new SolidColorBrush(Color.FromRgb(255, 51, 51)),
            FontSize = 12,
            BorderThickness = new Thickness(4)
        };
            
        btn.Click += buttonClickFunction;
            
        buttonCanvas.Children.Add(btn);
        Canvas.SetLeft(btn, 10);
        Canvas.SetTop(btn, 700);
    }
}