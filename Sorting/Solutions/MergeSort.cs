namespace Sorting.Solutions;

public static class MergeSortExtension
{
    private static int[] _t2;
    
    public static int[] MergeSort(this int[] input)
    {
        _t2 = new int[input.Length];
        MergeSort(input, 0, input.Length - 1);
        _t2 = null;
        return input;
    }

    private static void MergeSort(int[] array, int left, int right)
    {
        if (left < right)
        {
            int mid = (left + right) / 2;
            MergeSort(array, left, mid);
            MergeSort(array, mid + 1, right);
            Merge(array, left, mid, right);
        }
    }

    private static void Merge(int[] array, int left, int mid, int right)
    {
        int l1 = left, r1 = mid, l2 = mid + 1, r2 = right, i = left;
        while (l1<=r1 && l2<=r2)
        {
            if (array[l1] < array[l2])
            {
                _t2[i] = array[l1];
                l1++;
            }
            else
            {
                _t2[i] = array[l2];
                l2++;
            }
            i++;
        }
        while (l1 <= r1)
        {
            _t2[i] = array[l1];
            l1++;
            i++;
        }
        while (l2 <= r2)
        {
            _t2[i] = array[l2];
            l2++;
            i++;
        }
        for (i = left; i <= right; i++)
        {
            array[i] = _t2[i];
        }
    }
}