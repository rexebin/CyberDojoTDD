namespace CyberDojo.Autocomplete;

public class SuffixArrayResult
{
    public SuffixArrayResult(SuffixArrayItem[] suffixArray, string concatenatedString)
    {
        SuffixArray = suffixArray;
        ConcatenatedString = concatenatedString;
    }

    public SuffixArrayItem[] SuffixArray { get; private set; }
    public string ConcatenatedString { get; private set; }
}