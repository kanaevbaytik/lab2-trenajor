using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baytik.ViewModels;
public class QuickSortMedianOfThreeViewModel: BaseSortArrayVm
{
    public override int[] Sort(int[] arr)
    {
        return HoareQuickSort(arr, 0, arr.Length - 1);
    }

    private int[] HoareQuickSort(int[] arr, int low, int high)
    {
        if (low < high)
        {
            var partitionIndex = Partition(arr, low, high);
            SortHistory.Add($"Опорный элемент: {arr[partitionIndex]}");
            SortHistory.Add($"Подмассив до опорного элемента: [{string.Join(", ", arr.Skip(low).Take(partitionIndex - low))}]");
            SortHistory.Add($"Подмассив после опорного элемента: [{string.Join(", ", arr.Skip(partitionIndex + 1).Take(high - partitionIndex))}]");

            if (low < partitionIndex)
                HoareQuickSort(arr, low, partitionIndex - 1);

            if (partitionIndex < high)
                HoareQuickSort(arr, partitionIndex + 1, high);
        }

        return arr;
    }

    private int Partition(int[] arr, int low, int high)
    {
        var medianIndex = FindMedianIndex(arr, low, high);
        Swap(arr, medianIndex, high); // Поменяем медиану с последним элементом для использования как опорного

        var pivot = arr[high];
        var i = (low - 1);

        for (var j = low; j < high; j++)
        {
            if (arr[j] <= pivot)
            {
                i++;
                Swap(arr, i, j);
            }
        }

        Swap(arr, i + 1, high);
        return i + 1;
    }

    private int FindMedianIndex(int[] arr, int low, int high)
    {
        var mid = low + (high - low) / 2;

        if (arr[low] > arr[mid])
        {
            if (arr[low] < arr[high])
                return low;
            return (arr[mid] > arr[high]) ? mid : high;
        }
        else
        {
            if (arr[mid] < arr[high])
                return mid;
            return (arr[low] > arr[high]) ? low : high;
        }
    }

    private static void Swap(int[] arr, int i, int j) => (arr[j], arr[i]) = (arr[i], arr[j]);
}
