using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sudoku
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.SudokuGridValuesRaw = new int[9, 9];
            for (int i = 0; i < this.SudokuGridValuesRaw.GetLength(0); i++)
            {
                for (int j = 0; j < this.SudokuGridValuesRaw.GetLength(1); j++)
                {
                    this.SudokuGridValuesRaw[i, j] = i * 9 + j;
                }
            }

            this.SudokuGridValues = new List<List<List<List<Cell>>>>();
            for (int i = 0; i < 3; i++)
            {
                var currentBigRow = InitializeBigRow(i);
                this.SudokuGridValues.Add(currentBigRow);
            }
            SudokuGrid.ItemsSource = SudokuGridValues;
        }

        private List<List<List<Cell>>> InitializeBigRow(int bigRowIndex)
        {
            var result = new List<List<List<Cell>>>();
            for (int i = 0; i < 3; i++)
            {
                List<List<Cell>> bigRowCell = InitializeBigRowCell(bigRowIndex, i);
                result.Add(bigRowCell);
            }
            return result;
        }

        private List<List<Cell>> InitializeBigRowCell(int bigRowIndex, int bigCellIndex)
        {
            var result = new List<List<Cell>>();
            for (int i = 0; i < 3; i++)
            {
                List<Cell> smallRow = InitializeSmallRow(bigRowIndex, bigCellIndex, i);
                result.Add(smallRow);
            }
            return result;
        }

        private List<Cell> InitializeSmallRow(int bigRowIndex, int bigCellIndex, int smallRowIndex)
        {
            List<Cell> result = new List<Cell>();
            int startRow = bigRowIndex * 3 + smallRowIndex;
            int startColumn = bigCellIndex * 3;
            for (int i = 0; i < 3; i++)
            {
                Cell cell = new Cell()
                {
                    Row = startRow,
                    Column = startColumn + i,
                    Content = this.SudokuGridValuesRaw[startRow, startColumn + i].ToString()
                };
                result.Add(cell);
            }
            return result;
        }

        public int[,] SudokuGridValuesRaw { get; set; }

        public List<List<List<List<Cell>>>> SudokuGridValues { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button senderButton = (Button)sender;
            var context = (Cell)senderButton.DataContext;
            MessageBox.Show(string.Format("Clicked the button on row {0} column {1}.", context.Row, context.Column));
        }
    }

    public class Cell
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public string Content { get; set; }
    }
}
