

## Autocomplete

Given a list of terms, provide autocomplete suggestions for the current input.

### input: 
a list of terms, a search string

### output: 
return all terms containing the search string

### Approach
Build a suffix array from the terms, and do binary search on the suffix array.
Question: do we need to do something with space?

1. build a suffix array
   1. concatenate all terms, with $ at the end of each term, to prevent suffix being prefix to other terms
   2. get all suffixes from the concatenated string
   3. sort the suffixes, remove suffixes starting with $
   4. build a suffix array from the sorted suffixes
2. binary search the suffix array
   1. start in the middle, compare search term with the middle suffix
   2. if smaller, search in the left half
   3. if larger, search in the right half
   4. until find the search terms
      1. slide window to left and right to get all possible matches
   5. return all matches

