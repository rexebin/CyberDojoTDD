namespace CyberDojo.Autocomplete;

public class SuffixArrayItem
{
    public SuffixArrayItem(int wordIndex, int termIndex)
    {
        WordIndex = wordIndex;
        TermIndex = termIndex;
    }

    public int WordIndex { get; set; }
    public int TermIndex { get; set; }
}