using System;
using System.Linq;
using System.Windows.Controls;

namespace WpfApp1;

public class GridGenerator
{
    public const int GridWidth = 10;
    public const int GridHeight = 10;
    private readonly RandomCharGenerator _randomCharGenerator;
    public ButtonInGrid[,] GridButtonsArray { get; private set; }
    
    public GridGenerator(RandomCharGenerator randomCharGenerator)
    {
        _randomCharGenerator = randomCharGenerator;
        GridButtonsArray = new ButtonInGrid[GridWidth, GridHeight];
    }
    
    public void GenerateGrid(string[] words)
    {
        for (int x = 0; x < GridWidth; x++)
        {
            for (int y = 0; y < GridHeight; y++)
            {
                GridButtonsArray[x, y] = new ButtonInGrid(_randomCharGenerator.GenerateRandomChar(), new Point(x, y));
            }
        }
        InsertWords(words);
    }

    private void InsertWords(string[] words)
    {
        words = RandomiseWords(words);
        var wordToInsert = words[0];
        var wordLength = wordToInsert.Length;

        for (int x = 0; x < wordLength; x++)
        {
            GridButtonsArray[x, 0].ChangeLetter(wordToInsert[x]);
        }
    }

    private string[] RandomiseWords(string[] words)
    {
        var wordIds = new Tuple<float, string>[words.Length];
            
        for (int i = 0; i < wordIds.Length; i++)
        {
            wordIds[i] = new Tuple<float, string>(new Random().NextSingle(), words[i]);
        }
            
        Array.Sort(wordIds);

        return wordIds.Select(x => x.Item2).ToArray();
    }
}