namespace CyberDojo.Autocomplete;

public class SuffixArrayItem
{
    public SuffixArrayItem(int termIndex, int wordIndex)
    {
        WordIndex = wordIndex;
        TermIndex = termIndex;
    }

    public int WordIndex { get; private set; }
    public int TermIndex { get; private set; }
}