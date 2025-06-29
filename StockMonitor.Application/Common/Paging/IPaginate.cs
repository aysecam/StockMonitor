namespace StockMonitor.Application.Common.Paging;

public interface IPaginate<T>
{
    int From { get; }
    int Index { get; }
    int Size { get; }
    int Count { get; }
    int Pages { get; }
    IList<T> Items { get; }
    public bool HasPrevious => Index > 0; // İlk sayfada değilse true
    public bool HasNext => Index + 1 < Pages; // Son sayfada değilse true
}
