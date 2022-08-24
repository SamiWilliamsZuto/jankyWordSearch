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
        public const int MaxWords = 4;
        
        private readonly Color _grey = Color.FromRgb(221, 221, 221);
        
        private readonly string[] _words = { "Cat", "Dog", "Worm", "Sausage", "Potato", "Porridge", "Towel", "Rotisserie", "Gym", "Tinder" };
        private List<ButtonInGrid> _userClickedButtons;
        private UiCreator _uiCreator;
        private GridGenerator _gridGenerator;

        public MainWindow()
        {
            InitializeComponent();
            _words = _words.Select(x => x.ToUpper()).ToArray();
            _userClickedButtons = new List<ButtonInGrid>();
            _uiCreator = new UiCreator();
            _gridGenerator = new GridGenerator(new RandomCharGenerator());
            _gridGenerator.GenerateGrid(_words);

            _uiCreator.CreateLabels(ButtonCanvas, _words);
            _uiCreator.CreateGridButtons(ButtonCanvas, _gridGenerator.GridButtonsArray, grid_Button_Click);
            _uiCreator.CreateClearButton(ButtonCanvas, clear_Button_Click);
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
                    _userClickedButtons.Add(_gridGenerator.GridButtonsArray[buttonLocation.X, buttonLocation.Y]);
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
                foreach (var gridButton in _gridGenerator.GridButtonsArray)
                {
                    gridButton.Button.Background = new SolidColorBrush(_grey);
                    
                }
            }

            Console.WriteLine(JsonSerializer.Serialize(_userClickedButtons));
        }
    }
}
