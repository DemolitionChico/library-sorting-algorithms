using Sorting.Utils;

namespace Sorting.Solutions;

public static class BubbleSortExtension
{
    public static int[] BubbleSort(this int[] input)
    {
        for (int i = 1; i < input.Length; i++)
        {
            for (int j = input.Length - 1; j >= i; j--)
            {
                if (input[j] < input[j - 1])
                {
                    input.SwapByIndices(j, j-1);
                }
            }
        }

        return input;
    }

    // Enhanced version of bubble sort
    public static int[] ShakerSort(this int[] input)
    {
        int left = 1, right = input.Length - 1;
        int lastSwapIndex = right;
        do
        {
            for (int i = right; i >= left; i--)
            {
                if (input[i] < input[i - 1])
                {
                    input.SwapByIndices(i, i - 1);
                    lastSwapIndex = i;
                }
            }
            left = lastSwapIndex + 1;
            for (int i = left; i <= right; i++)
            {
                if (input[i] < input[i - 1])
                {
                    input.SwapByIndices(i, i-1);
                    lastSwapIndex = i;
                }
            }
            right = lastSwapIndex - 1;
        } while (left <= right);
        
        return input;
    }
}