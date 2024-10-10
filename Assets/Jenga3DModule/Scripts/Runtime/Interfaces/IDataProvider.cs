using Cysharp.Threading.Tasks;
using System.Threading;

public interface IDataProvider
{
    /// <summary>
    /// Fetches grade data asynchronously from the API.
    /// </summary>
    /// <param name="cancellationToken">Optional token to cancel the request if necessary.</param>
    /// <returns>A UniTask that resolves to an array of GradeData.</returns>
    UniTask<GradeData[]> FetchGradeDataAsync(CancellationToken cancellationToken = default);
}
