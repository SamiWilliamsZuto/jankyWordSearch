using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp1;

public class UiCreator
{
    public void CreateGridButtons(Canvas buttonCanvas, ButtonInGrid[,] buttonArray, RoutedEventHandler buttonClickFunction)
    {
        const int buttonWidth = 64;
        const int buttonHeight = 64;

        for (int x = 0; x < GridGenerator.GridWidth; x++)
        {
            for (int y = 0; y < GridGenerator.GridHeight; y++)
            {
                Button btn = new Button
                {
                    Name = $"button{x}{y}",
                    Tag = new Point(x, y),
                    Content = buttonArray[x, y].Letter,
                    Height = buttonHeight,
                    Width = buttonWidth,
                    Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                    FontSize = 12,
                    BorderThickness = new Thickness(4)
                };
                btn.Click += buttonClickFunction;

                buttonArray[x, y].AddButton(btn);

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
    
    public void CreateLabels(Canvas buttonCanvas, string[] words)
    {
        var wordIds = new Tuple<float, string>[words.Length];
            
        for (int i = 0; i < wordIds.Length; i++)
        {
            wordIds[i] = new Tuple<float, string>(new Random().NextSingle(), words[i]);
        }
            
        Array.Sort(wordIds);
            
        for (int i = 0; i < (wordIds.Length < MainWindow.MaxWords ? wordIds.Length : MainWindow.MaxWords); i++)
        {
            var label = new Label()
            {
                Name = wordIds[i].Item2,
                Content = wordIds[i].Item2
            };

            buttonCanvas.Children.Add(label);
            Canvas.SetLeft(label, 845);
            Canvas.SetTop(label, i * 14);
        }
    }
}