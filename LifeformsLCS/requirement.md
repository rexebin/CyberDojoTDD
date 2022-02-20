## Life Forms

You may have wondered why most extraterrestrial life forms resemble humans, differing by superficial traits such as height, colour, wrinkles, ears, eyebrows and the like. A few bear no human resemblance; these typically have geometric or amorphous shapes like cubes, oil slicks or clouds of dust.

The answer is given in the 146th episode of Star Trek - The Next Generation, titled The Chase. It turns out that in the vast majority of the quadrant’s life forms ended up with a large fragment of common DNA.

Given the DNA sequences of several life forms represented as strings of letters, you are to find the longest substring that is shared by more than half of them.

## Input
Standard input contains several test cases. Each test case begins with 1≤n≤100, the number of life forms. n lines follow; each contains a string of lower case letters representing the DNA sequence of a life form. Each DNA sequence contains at least one and not more than 1006 letters and consists solely of lower case ’a’-’z’. A line containing 0 follows the last test case.

## Output
For each test case, output the longest string or strings shared by more than half of the life forms. If there are many, output all of them in alphabetical order. If there is no solution with at least one letter, output “?”. Leave an empty line between test cases.

Test case: 

1. input = new string[]{"abcdefg", "bcdefgh", "cdefghi"}, output: new string[]{"bcdefg", "cdefgh"}
2. input = new string[]{"xxx", "yyy", "zzz"}, output: new string[]{"?"}


## Analysis
Longest common substrings amount more than half of the inputs, K = n/2+1

1. concatenate all strings:
   1. adding special character to the end of each string, $, to prevent suffix being prefix of others
   2. when concatenating, need to save the index word of input array to distinguish between different strings
2. two pointer sliding window to find out longest common substrings amount K elements in the input.
   1. if the distinct substring are less less than half, move the right pointer to the next string
   2. if the length of the window is greater than K, move the left pointer to the next string
   3. when window length and distinct substring are equal to K, we have a LCS in this window
   4. compare if the LCS in the window is the longest
      1. if yes, save it to the result, remove anything shorter
      2. if no, repeat above steps
   5. end when both pointer reach the end of the input array
3. return LCS result