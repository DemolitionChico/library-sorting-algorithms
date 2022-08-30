using NUnit.Framework;
using Sorting.Solutions;

namespace SortingTests;

public class BubbleSortTest
{
    [TestCase(new int[] { 10, 9, 8, 6, 7 }, ExpectedResult = new int[] { 6, 7, 8, 9, 10 })]
    [TestCase(new int[] { 0, 1, 2, 3, 4 }, ExpectedResult = new int[] { 0, 1, 2, 3, 4 })]
    [TestCase(new int[] { 0, 100, 2, 95, 4 }, ExpectedResult = new int[] { 0, 2, 4, 95, 100 })]
    public int[] ShouldBubbleSortArray(int[] array)
    {
        return array.BubbleSort();
    }
    
    [TestCase(new int[] { 10, 9, 8, 6, 7 }, ExpectedResult = new int[] { 6, 7, 8, 9, 10 })]
    [TestCase(new int[] { 0, 1, 2, 3, 4 }, ExpectedResult = new int[] { 0, 1, 2, 3, 4 })]
    [TestCase(new int[] { 0, 100, 2, 95, 4 }, ExpectedResult = new int[] { 0, 2, 4, 95, 100 })]
    [TestCase(new int[] { 0, 1, 2, 3, 4, 5, 6, 8, 7 }, ExpectedResult = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 })]
    public int[] ShouldShakeSortArray(int[] array)
    {
        return array.ShakerSort();
    }
}