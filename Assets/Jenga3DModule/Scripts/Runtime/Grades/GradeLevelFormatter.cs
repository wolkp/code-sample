using System.Text.RegularExpressions;

public static class GradeLevelFormatter
{
    // Format for the number with the superscript suffix
    private const string GradeLevelNumberFormat = "{0}<sup><size=110%>{1}</size></sup>";

    // Full format that includes the number format and the grade text
    // Note: It's composed dynamically in the method to avoid issues with string.Format
    private const string GradeLevelFullBaseFormat = "{0} {1}";

    /// <summary>
    /// Gets the shortened format(e.g., "6<sup><size=110%>TH</size></sup>")
    /// </summary>
    public static string GetParsedGradeLevelShortened(string gradeLevelText)
    {
        ParseGradeLevel(gradeLevelText, out string number, out string suffix, out _);
        return GetShortenedFormat(number, suffix);
    }

    /// <summary>
    /// Gets the the full format (e.g., "6<sup><size=110%>TH</size></sup> Grade")
    /// </summary>
    public static string GetParsedGradeLevelFull(string gradeLevelText)
    {
        ParseGradeLevel(gradeLevelText, out string number, out string suffix, out string grade);
        return GetFullFormat(number, suffix, grade);
    }

    private static string GetShortenedFormat(string numberText, string suffixText)
    {
        return string.Format(GradeLevelNumberFormat, numberText, suffixText);
    }

    private static string GetFullFormat(string numberText, string suffixText, string gradeText)
    {
        string numberWithSuffix = string.Format(GradeLevelNumberFormat, numberText, suffixText);
        return string.Format(GradeLevelFullBaseFormat, numberWithSuffix, gradeText);
    }

    /// <summary>
    /// Extracts number and suffix from grade level.
    /// For example "8th Grade" will be extracted to number:"8", suffix:"th", grade:"Grade"
    /// </summary>
    private static void ParseGradeLevel(string gradeLevel, out string number, out string suffix, out string grade)
    {
        grade = StringConstants.GradeString;
        gradeLevel = gradeLevel.ToLower().Replace($" {grade}", string.Empty);

        var match = Regex.Match(gradeLevel, @"(\d+)(\D+)");

        if (match.Success)
        {
            number = match.Groups[1].Value;
            suffix = match.Groups[2].Value.Trim();
        }
        else
        {
            number = string.Empty;
            suffix = string.Empty;
        }
    }
}