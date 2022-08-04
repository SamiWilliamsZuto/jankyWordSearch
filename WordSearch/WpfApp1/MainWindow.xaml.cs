using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            CreateButtons();
        }

        private void CreateButtons()
        {
            const int buttonWidth = 64;
            const int buttonHeight = 64;

            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    Button btn = new Button
                    {
                        Name = $"button{x}{y}",
                        Tag = $"button {x},{y}",
                        Content = "A",
                        Height = buttonHeight,
                        Width = buttonWidth,
                        Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                        FontSize = 12,
                        BorderThickness = new Thickness(4)
                    };
                    btn.Click += button_Click;

                    ButtonCanvas.Children.Add(btn);
                    Canvas.SetLeft(btn, x * buttonWidth);
                    Canvas.SetTop(btn, y * buttonHeight);
                }
            }
        }
        void button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine($"You clicked on the {(sender as Button)?.Tag}. button.");

            if (sender is Button button) 
                button.Background = new SolidColorBrush(Color.FromRgb(0, 255, 0));
        }
    }
}
