### Requirement
String analysis often arises in applications from biology and chemistry, such as the study of DNA and protein molecules. One interesting problem is to find how many substrings are repeated (at least twice) in a long string.

In this problem, you will write a program to find the total number of repeated substrings in a string of at most 100000 alphabetic characters. Any unique substring that occurs more than once is counted. As an example, if the string is “aabaab”, there are 5 repeated substrings: “a”, “aa”, “aab”, “ab”, “b”. If the string is “aaaaa”, the repeated substrings are “a”, “aa”, “aaa”, “aaaa”. Note that repeated occurrences of a substring may overlap (e.g. “aaaa” in the second case).

Input
The input consists of at most 10 cases. The first line contains a positive integer, specifying the number of cases to follow. Each of the following line contains a nonempty string of up to 100000 alphabetic characters.

Output
For each line of input, output one line containing the number of unique substrings that are repeated. You may assume that the correct answer fits in a signed 32-bit integer.

Sample Input 1	Sample Output 1
3               5
aabaab          4
aaaaa           5
AaAaA



### Approach:

1. create a longest substring of the given string
   1. get all substrings: 
   3. sort it SA, stored as suffice array
   4. generate LCP array
2. result = n(n+1)/2(total substrings) - sum(LCP)// repeated substrings

