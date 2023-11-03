using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baytik.ViewModels;
public class BeadSortViewModel: BaseSortArrayVm
{
    public override int[] Sort(int[] arr)
    {
        return RadixSort(arr);
    }

    private int[] RadixSort(int[] arr)
    {
        if (arr.Length <= 1)
        {
            return arr;
        }

        var maxElement = arr.Max();
        var digitPosition = 1;

        while (maxElement / digitPosition > 0)
        {
            arr = CountingSortByDigit(arr, digitPosition);
            digitPosition *= 10; // Увеличиваем разрядность

            SortHistory.Add($"Сортировка по разряду {digitPosition}:\n{string.Join(" ", arr)}");
        }

        return arr;
    }

    private int[] CountingSortByDigit(int[] arr, int digitPosition)
    {
        var n = arr.Length;
        var output = new int[n];
        var count = new int[19]; // Изменен размер массива для учета отрицательных чисел

        for (var i = 0; i < n; i++)
        {
            var digit = (arr[i] / digitPosition) % 10 + 9; // Сдвигаем для учета отрицательных чисел
            count[digit]++;
        }

        for (var i = 1; i < 19; i++)
        {
            count[i] += count[i - 1];
        }

        for (var i = n - 1; i >= 0; i--)
        {
            var digit = (arr[i] / digitPosition) % 10 + 9; // Сдвигаем для учета отрицательных чисел
            output[count[digit] - 1] = arr[i];
            count[digit]--;
        }

        for (var i = 0; i < n; i++)
        {
            arr[i] = output[i];
        }

        return arr;
    }
}
