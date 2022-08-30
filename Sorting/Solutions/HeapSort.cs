using Sorting.Utils;

namespace Sorting.Solutions;

public static class HeapSortExtension
{
    public static int[] HeapSort(this int[] input)
    {
        int n = input.Length, swap;
        for (int i = n/2; i > 0; i--)
        {
            RestoreHeap(input, i, n);
        }

        do
        {
            input.SwapByIndices(0,--n);
            RestoreHeap(input, 1, n);
        } while (n > 1);

        return input;
    }

    private static void RestoreHeap(int[] brokenHeap, int start, int end)
    {
        int temp = brokenHeap[start - 1];
        while (start <= end / 2)
        {
            int index = 2 * start;
            if (index < end && brokenHeap[index - 1] < brokenHeap[index])
            {
                // move to the next child's index
                index++;
            }
            if (temp >= brokenHeap[index - 1])
            {
                break;
            }
            brokenHeap[start - 1] = brokenHeap[index - 1];
            start = index;
        }

        brokenHeap[start - 1] = temp;
    }
}