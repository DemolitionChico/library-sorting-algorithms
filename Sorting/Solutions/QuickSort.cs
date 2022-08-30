using Sorting.Utils;

namespace Sorting.Solutions;

public static class QuickSortExtension
{
    public static int[] QuickSort(this int[] input)
    {
        QuickSort(input, 0, input.Length - 1);
        return input;
    }

    private static void QuickSort(int[] array, int left, int right)
    {
        if (left < right)
        {
            int pivotPlacementIndex = left;
            for (int i = left + 1; i <= right; i++)
            {
                // left is pivot's index
                if (array[i] < array[left])
                {
                    pivotPlacementIndex++;
                    array.SwapByIndices(i, pivotPlacementIndex);
                }
            }
            array.SwapByIndices(left, pivotPlacementIndex);
            QuickSort(array, left, pivotPlacementIndex - 1);
            QuickSort(array, pivotPlacementIndex + 1, right);
        }
    }
}