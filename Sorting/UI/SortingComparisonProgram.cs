using System.Diagnostics;
using Sorting.Solutions;

namespace Sorting.UI;

public class SortingComparisonProgram
{
    private readonly ISolution[] _sortingOptions =
    {
        new GenericSolution("Sort by insertion", array => array.SortByInsertion()),
        new GenericSolution("Bubble sort", array => array.BubbleSort()),
        new GenericSolution("Bubble sort (shake sort)", array => array.ShakerSort()),
        new GenericSolution("Quick sort", array => array.QuickSort()),
        new GenericSolution("Heap sort", array => array.HeapSort()),
        new GenericSolution("Merge sort", array => array.MergeSort()),
        new ComparingSolution()
    };

    private readonly int[] _arraySizesOptions = {10, 100, 1000, 10000, 20000, 50000, 100000};

    private bool hasStopped = false;

    private int[] sortedArray;

    public void Run()
    {
        do
        {
            int selectedAlgorithmIndex = HandleMenuSelection(_sortingOptions.Select(x => x.Name).ToArray(),
                () => { hasStopped = true; },
                "Comparing different popular sorting algorithms");
            if (hasStopped)
            {
                return;
            }

            int selectedArraySizeIndex =
                HandleMenuSelection(_arraySizesOptions, onExit: () => { hasStopped = true; }, "select array size");
            if (hasStopped)
            {
                return;
            }

            string selectedOptionName = _sortingOptions[selectedAlgorithmIndex].Name;
            Console.WriteLine($"Selected option: {selectedOptionName}");
            Console.WriteLine($"Selected array size: {_arraySizesOptions[selectedArraySizeIndex]}");
            int size = _arraySizesOptions[selectedArraySizeIndex];
            ResetArray(size, true);

            _sortingOptions[selectedAlgorithmIndex].Run(sortedArray);

            Console.WriteLine();
            Console.WriteLine($"Continue? (Y/N)");
            if (new[] {ConsoleKey.N, ConsoleKey.Escape}.Contains(Console.ReadKey().Key))
            {
                Console.Clear();
                hasStopped = true;
            }
        } while (!hasStopped);
    }

    // returns confirmed index
    private static int HandleMenuSelection<T>(IReadOnlyList<T> menu, Action onExit, string menuText = null)
    {
        var shouldCloseMenu = false;
        var selectedIndex = 0;
        do
        {
            Console.Clear();
            DrawMenu(menu, selectedIndex, menuText);
            var pressedKeyInfo = Console.ReadKey();
            switch (pressedKeyInfo.Key)
            {
                case ConsoleKey.DownArrow:
                    selectedIndex = selectedIndex >= menu.Count ? 0 : selectedIndex + 1;
                    break;
                case ConsoleKey.UpArrow:
                    selectedIndex = selectedIndex <= 0 ? menu.Count : selectedIndex - 1;
                    break;
                case ConsoleKey.Escape:
                    Console.Clear();
                    onExit();
                    return -1;
            }

            if (pressedKeyInfo.Key != ConsoleKey.Enter) continue;
            Console.Beep();
            Console.Clear();
            if (selectedIndex != menu.Count) return selectedIndex;
            onExit();
            return -1;
        } while (true);
    }

    private static void DrawMenu<T>(IReadOnlyList<T> menu, int selectedIndex, string topMessage = null)
    {
        if (!string.IsNullOrEmpty(topMessage))
        {
            Console.WriteLine(topMessage);
        }

        Console.WriteLine();
        for (var i = 0; i < menu.Count; i++)
        {
            Console.WriteLine($" {(selectedIndex == i ? ">" : ""),-2}{menu[i]}");
        }

        Console.WriteLine($" {(selectedIndex == menu.Count ? ">" : ""),-2}EXIT");
    }

    private void ResetArray(int size, bool random = false)
    {
        sortedArray = new int[size];
        Random rnd = new Random();
        for (int i = 0; i < size; i++)
        {
            sortedArray[i] = !random ? size - i : rnd.Next();
        }
    }

    private interface ISolution
    {
        string Name { get; }
        void Run(int[] array);
    }

    private class GenericSolution : ISolution
    {
        private readonly string _name;
        private readonly Action<int[]> _handler;
        public string Name => _name;

        public GenericSolution(string name, Action<int[]> handler)
        {
            _name = name;
            _handler = handler;
        }

        public void Run(int[] array)
        {
            Console.WriteLine();
            var stopwatch = Stopwatch.StartNew();
            _handler(array);
            stopwatch.Stop();
            var result = stopwatch.ElapsedMilliseconds;
            Console.WriteLine(
                $"{_name} took {result / (decimal) 1000:N1}s ({result} milliseconds) for an array of {array.Length} elements.");
            Console.WriteLine();
        }
    }

    private class ComparingSolution : ISolution
    {
        private readonly Dictionary<string, Action<int[]>> options = new Dictionary<string, Action<int[]>>()
        {
            {"Sort by insertion", array => array.SortByInsertion()},
            {"Bubble sort", array => array.BubbleSort()},
            {"Shake sort", array => array.ShakerSort()},
            {"Quick sort", array => array.QuickSort()},
            {"Heap sort", array => array.HeapSort()},
            {"Merge sort", array => array.MergeSort()}
        };

        public string Name => "Compare all";

        public void Run(int[] array)
        {
            long[] results = new long[options.Count];
            string[] keys = options.Keys.ToArray();
            int longestName = keys.Select(x => x.Length).OrderByDescending(x => x).First();
            string headerFormat = String.Join(string.Empty, keys.Select((x, i) => $"{{{i},{longestName + 3}}}"));
            string header = string.Format(headerFormat, keys.Select(x => x).ToArray());
            string resultsLine;
            var stopwatch = new Stopwatch();
            for (int i = 0; i < keys.Length; i++)
            {
                int[] tempArray = (int[]) array.Clone();
                Console.Clear();
                resultsLine = string.Format(headerFormat,
                    results.Select((x, j) => j < i ? $"{x} ms" : (j == i ? "processing" : "-")).ToArray());
                Console.WriteLine(header);
                Console.WriteLine(resultsLine);
                stopwatch.Restart();
                options[keys[i]](tempArray);
                results[i] = stopwatch.ElapsedMilliseconds;
            }

            Console.Clear();
            resultsLine = string.Format(headerFormat, results.Select(x => $"{x} ms").ToArray());
            Console.WriteLine(header);
            Console.WriteLine(resultsLine);
        }
    }
}
