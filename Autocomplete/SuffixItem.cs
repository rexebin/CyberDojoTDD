namespace CyberDojo.Autocomplete;

public class SuffixItem
{
    public SuffixItem(string suffix, int termIndex, int wordIndex)
    {
        Suffix = suffix;
        TermIndex = termIndex;
        WordIndex = wordIndex;
    }

    public int TermIndex { get; private set; }
    public int WordIndex { get; set; }
    public string Suffix { get; private set; }
}