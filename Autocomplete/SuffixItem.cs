namespace CyberDojo.Autocomplete;

public class SuffixItem
{
    public SuffixItem(string suffix, int termIndex, int index)
    {
        Suffix = suffix;
        TermIndex = termIndex;
        Index = index;
    }

    public int TermIndex { get; private set; }
    public int Index { get; set; }
    public string Suffix { get; private set; }
}