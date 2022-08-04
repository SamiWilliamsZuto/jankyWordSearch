using System;
using System.Linq;
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
        private const int MaxWords = 4;
        private readonly string[] _words = { "Cat", "Dog", "Worm", "Sausage", "Potato", "Porridge", "Towel", "Rotisserie", "Gym", "Tinder" };
        
        public MainWindow()
        {
            InitializeComponent();

            CreateLabels();
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
                        Content = GenerateRandomChar(),
                        Height = buttonHeight,
                        Width = buttonWidth,
                        Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                        FontSize = 12,
                        BorderThickness = new Thickness(4)
                    };
                    btn.Click += button_Click;

                    ButtonCanvas.Children.Add(btn);
                    Canvas.SetLeft(btn, x * buttonWidth + 10);
                    Canvas.SetTop(btn, y * buttonHeight + 10);
                }
            }
        }

        public void CreateLabels()
        {
            var wordIds = new Tuple<float, string>[_words.Length];
            
            for (int i = 0; i < wordIds.Length; i++)
            {
                wordIds[i] = new Tuple<float, string>(new Random().NextSingle(), _words[i]);
            }
            
            Array.Sort(wordIds);
            
            for (int i = 0; i < (wordIds.Length < MaxWords ? wordIds.Length : MaxWords); i++)
            {
                var label = new Label()
                {
                    Name = wordIds[i].Item2.ToUpper(),
                    Content = wordIds[i].Item2.ToUpper()
                };

                ButtonCanvas.Children.Add(label);
                Canvas.SetLeft(label, 845);
                Canvas.SetTop(label, i * 14);
            }
        }
        

        private char GenerateRandomChar()
        {
            var randomChar = new Random().Next(65, 90 + 1);
            return (char) randomChar;
        }

        void button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine($"You clicked on the {(sender as Button)?.Tag}. button.");

            if (sender is Button button) 
                button.Background = new SolidColorBrush(Color.FromRgb(0, 255, 0));
        }
    }
}
