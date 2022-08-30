namespace Sorting.Solutions;

public static class SortByInsertionExtension
{
    public static int[] SortByInsertion(this int[] input)
    {
        for (int i = 1; i < input.Length; i++)
        {
            int j = i;
            int temp = input[j];
            while (j > 0 && input[j - 1] > temp)
            {
                input[j] = input[j - 1];
                j--;
            }
            input[j] = temp;
        }

        return input;
    }
}