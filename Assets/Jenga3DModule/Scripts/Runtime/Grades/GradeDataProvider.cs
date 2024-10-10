using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using System.Linq;

public class GradeDataProvider : IDataProvider
{
    private readonly GradeDataProviderInstaller.Settings _settings;

    public GradeDataProvider(GradeDataProviderInstaller.Settings settings)
    {
        _settings = settings;
    }

    /// <summary>
    /// Fetches grade data from the specified API URL asynchronously.
    /// </summary>
    /// <param name="cancellationToken">Optional token to cancel the request if necessary.</param>
    /// <returns>A UniTask that resolves to an array of GradeData.</returns>
    public async UniTask<GradeData[]> FetchGradeDataAsync(CancellationToken cancellationToken = default)
    {
        using (var request = UnityWebRequest.Get(_settings.ApiUrl))
        {
            try
            {
                // Send the request and wait for it to complete
                await request.SendWebRequest().WithCancellation(cancellationToken);

                if (request.result == UnityWebRequest.Result.ConnectionError ||
                    request.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError($"Request error: {request.error}");
                    return null;
                }

                // Parse the JSON response
                string json = request.downloadHandler.text;
                if (string.IsNullOrEmpty(json))
                {
                    Debug.LogWarning("Empty JSON response.");
                    return null;
                }

                // Convert JSON to an array of GradeData
                var gradeDataArray = JsonHelper.FromJson<GradeData>(json);

                // Gets rid of invalid grades
                gradeDataArray = GetValidGradeEntries(gradeDataArray);

                return gradeDataArray;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error during request: {ex.Message}");
                return null;
            }
        }
    }

    private GradeData[] GetValidGradeEntries(GradeData[] data)
    {
        if (data == null || data.Length == 0)
        {
            Debug.LogWarning("No grade data to validate.");
            return new GradeData[0]; // Return an empty array if there's nothing to validate
        }

        var validEntries = data
            .Where(entry => IsGradeValid(entry.grade))
            .ToArray();

        return validEntries;
    }

    /// <summary>
    /// Api json contains invalid entry with "grade": "Algebra 1".
    /// This function determines if the grade string is correct (i.e. contains a proper substring)
    /// </summary>
    private bool IsGradeValid(string grade)
    {
        return grade.ToLower().Contains(StringConstants.GradeString);
    }
}
