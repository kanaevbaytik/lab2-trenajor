using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;

namespace Baytik.ViewModels;
public class BaseSortArrayVm : ViewModelBase
{
    public ICommand SortCommand
    {
        get;
    }

    public string ErrorText
    {
        get => _errorText;
        set => this.RaiseAndSetIfChanged(ref _errorText, value);
    }
    private string _errorText;

    public string ArrayString
    {
        get => _arrayString;
        set => this.RaiseAndSetIfChanged(ref _arrayString, value);
    }
    private string _arrayString;

    public ObservableCollection<string> SortHistory
    {
        get;
    } = new ObservableCollection<string>()
    {
        "Сортировка не проходила"
    };

    public BaseSortArrayVm()
    {
        SortCommand = ReactiveCommand.Create(() =>
        {
            try
            {
                var array = ArrayString.Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(x => int.Parse(x)).ToArray();
                SortHistory.Clear();
                var arr = Sort(array);
                SortHistory.Add($"Отсортированный массив: {string.Join(", ", arr)}");
            }
            catch (Exception ex)
            {
                ErrorText = ex.ToString();
            }
        });

        _errorText = string.Empty;
        _arrayString = "11, 2, 3";
    }

    public virtual int[] Sort(int[] arr)
    {
        return Array.Empty<int>();
    }
}
