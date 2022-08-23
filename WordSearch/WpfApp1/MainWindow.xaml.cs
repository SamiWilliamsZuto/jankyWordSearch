using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
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
        private const int GridWidth = 10;
        private const int GridHeight = 10;
        private readonly Color _grey = Color.FromRgb(221, 221, 221);
        
        private readonly string[] _words = { "Cat", "Dog", "Worm", "Sausage", "Potato", "Porridge", "Towel", "Rotisserie", "Gym", "Tinder" };
        private List<ButtonInGrid> _userClickedButtons;
        private ButtonInGrid[,] _gridButtonsArray; 

        public MainWindow()
        {
            InitializeComponent();
            
            _userClickedButtons = new List<ButtonInGrid>();
            _gridButtonsArray = new ButtonInGrid[GridWidth, GridHeight];
            CreateLabels();
            CreateGridButtons();
            CreateClearButton();
        }

        private void CreateGridButtons()
        {
            const int buttonWidth = 64;
            const int buttonHeight = 64;
            
            for (int x = 0; x < GridWidth; x++)
            {
                for (int y = 0; y < GridHeight; y++)
                {
                    Button btn = new Button
                    {
                        Name = $"button{x}{y}",
                        Tag = new Point(x, y),
                        Content = GenerateRandomChar(),
                        Height = buttonHeight,
                        Width = buttonWidth,
                        Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                        FontSize = 12,
                        BorderThickness = new Thickness(4)
                    };
                    btn.Click += grid_Button_Click;

                    _gridButtonsArray[x, y] = new ButtonInGrid((char)btn.Content, new Point(x, y), btn);

                    ButtonCanvas.Children.Add(btn);
                    Canvas.SetLeft(btn, x * buttonWidth + 10);
                    Canvas.SetTop(btn, y * buttonHeight + 10);
                }
            }
        }

        private void CreateClearButton()
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
            
            btn.Click += clear_Button_Click;
            
            ButtonCanvas.Children.Add(btn);
            Canvas.SetLeft(btn, 10);
            Canvas.SetTop(btn, 700);
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

        void grid_Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine($"You clicked on the {(sender as Button)?.Tag}. button.");

            if (sender is Button button)
            {
                Point buttonLocation = new Point((button.Tag as Point)!.X, (button.Tag as Point)!.Y);
                var isButtonClicked = _userClickedButtons.FirstOrDefault(x => x.Point == buttonLocation);
                if (isButtonClicked != null)
                {
                    
                    button.Background = new SolidColorBrush(_grey);
                    _userClickedButtons.Remove(isButtonClicked);
                }
                else
                {
                    button.Background = new SolidColorBrush(Color.FromRgb(0, 255, 0));
                    _userClickedButtons.Add(_gridButtonsArray[buttonLocation.X, buttonLocation.Y]);
                }
            }

            Console.WriteLine(JsonSerializer.Serialize(_userClickedButtons.Select(x => x.Point)));
        }
        
        void clear_Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("You clicked on the clear button.");

            if (sender is Button button)
            {
                _userClickedButtons.Clear();
                foreach (var gridButton in _gridButtonsArray)
                {
                    gridButton.Button.Background = new SolidColorBrush(_grey);
                    
                }
            }

            Console.WriteLine(JsonSerializer.Serialize(_userClickedButtons));
        }
    }
}
