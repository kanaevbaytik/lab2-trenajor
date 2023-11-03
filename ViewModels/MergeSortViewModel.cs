using System;
using System.Collections.Generic;
using System.Linq;
using ReactiveUI;

namespace Baytik.ViewModels;

public class MergeSortViewModel : BaseSortArrayVm
{
    public MergeSortViewModel()
    {
        ArrayString = "11, 2, 3";
    }

    public override int[] Sort(int[] arr)
    {
        return BitMaskMergeSort(arr);
    }

    private int[] BitMaskMergeSort(int[] arr)
    {
        if (arr.Length <= 1)
        {
            return arr;
        }

        var n = arr.Length;
        var temp = new int[n];
        var maxElement = arr.Max();
        var shift = 1;

        while (maxElement >> shift > 0)
        {
            var bit = 1 << shift;
            var mask = bit - 1;

            var count = new int[bit];
            var prefixSum = new int[bit];

            for (var i = 0; i < n; i++)
            {
                var index = (arr[i] >> shift) & mask;
                count[index]++;
            }

            prefixSum[0] = count[0];
            for (var i = 1; i < bit; i++)
            {
                prefixSum[i] = prefixSum[i - 1] + count[i];
            }

            for (var i = n - 1; i >= 0; i--)
            {
                var index = (arr[i] >> shift) & mask;
                temp[--prefixSum[index]] = arr[i];
            }

            for (var i = 0; i < n; i++)
            {
                arr[i] = temp[i];
                SortHistory.Add($"Сортировка на разряде {shift}, с основанием {bit}, и маской {mask}:\n{string.Join(" ", arr)}");
            }

            shift++;
        }

        return arr;
    }
}
