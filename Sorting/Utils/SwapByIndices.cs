namespace Sorting.Utils;

public static class SwapByIndicesExtension
{
    public static void SwapByIndices(this int[] array, int i, int j)
    {
        if (i == j)
        {
            return;
        }
        array[i] ^= array[j];
        array[j] = array[i] ^ array[j];
        array[i] ^= array[j];
    }
}