#include <iostream>
#include <fstream>
#include <vector>
#include <list>
#include <map>
#include <algorithm>
#include <cassert>
#include "../common/common.h"

string intersect (string& s1, string& s2) {
    string s = "";
    for (auto c : s1)
        if (s2.find(c) != string::npos)
            s += c;
    return s;
}

string difference(string& s1, string& s2) {
    string s = "";
    for (auto c : s1)
        if (s2.find(c) == string::npos)
            s += c;
    return s;
}

bool compareStrings(string s1, string s2)
{
    return s1.size() <= s2.size();
}

list<string> list_from_range(vector<string>& v, int start, int count) {
    list<string> ls;
    for (int i = start; i < start + count; i++) {
        ls.emplace_front(v[i]);
    }
    return ls;
}

vector<string> vector_from_range(vector<string>& v, int start, int count) {
    vector<string> v2;
    for (int i = start; i < start + count; i++) {
        v2.push_back(v[i]);
    }
    return v2;
}

string find_intersection_of_size(vector<string>& v, int size) {
    for (int i = 0; i < v.size(); i++)
        for (int j = i + 1; j < v.size(); j++) {
            string intersection = intersect(v[i], v[j]);
            if (intersection.size() == size)
                return intersection;
        }
    throw new domain_error("No intersection found D:");
}

string find_intersection_of_size(string s1, list<string> s2, int size) {
    for (auto s : s2) {
        string intersection = intersect(s1, s);
        if (intersection.size() == size)
            return intersection;
    }
    throw domain_error("No intersection found D:");
}

string find_singleton_intersection(string s1, list<string> s2) {
    return find_intersection_of_size(s1, s2, 1);
}

string find_difference_of_size(string s1, list<string> s2, int size) {
    for (auto s : s2) {
        string diff = difference(s, s1);
        if (diff.size() == size)
            return diff;
    }
    throw domain_error("No difference of correct size found D:");
}

string find_singleton_difference(string s1, list<string> s2) {
    return find_difference_of_size(s1, s2, 1);
}

bool equal_letters(string& s1, string& s2) {
    if (s1.size() != s2.size()) return false; // optimization
    for (int i = 0; i < s1.size(); i++)
        if (s2.find(s1[i]) == string::npos) return false;
    for (int i = 0; i < s2.size(); i++)
        if (s1.find(s2[i]) == string::npos) return false;
    return true;
}

string swap_letters(string s, map<char, char>& m) {
    string new_s = "";
    for (int i = 0; i < s.size(); i++) {
        new_s += m[s[i]];
    }
    return new_s;
}

// todo: remove
const string EIGHT = "abcdefg";
const string FIVE = "abdfg";
const string TWO = "acdeg";
const string THREE = "acdfg";
const string SEVEN = "acf";
const string NINE = "abcdfg";
const string SIX = "abdefg";
const string FOUR = "bcdf";
const string ZERO = "abcefg";
const string ONE = "cf";

int get_num(string& s, map<char, char>& m) {
    string x_8 = swap_letters(EIGHT, m);
    string x_5 = swap_letters(FIVE, m);
    string x_2 = swap_letters(TWO, m);
    string x_3 = swap_letters(THREE, m);
    string x_7 = swap_letters(SEVEN, m);
    string x_9 = swap_letters(NINE, m);
    string x_6 = swap_letters(SIX, m);
    string x_4 = swap_letters(FOUR, m);
    string x_0 = swap_letters(ZERO, m);
    string x_1 = swap_letters(ONE, m);
    if (equal_letters(s, x_1)) return 1;
    else if (equal_letters(s, x_2)) return 2;
    else if (equal_letters(s, x_3)) return 3;
    else if (equal_letters(s, x_4)) return 4;
    else if (equal_letters(s, x_5)) return 5;
    else if (equal_letters(s, x_6)) return 6;
    else if (equal_letters(s, x_7)) return 7;
    else if (equal_letters(s, x_8)) return 8;
    else if (equal_letters(s, x_9)) return 9;
    else if (equal_letters(s, x_0)) return 0;

    throw new domain_error("This is not a number unfortunately D:");
}

map<char, char> get_map(vector<string>& numbers, vector<string>& output) {
    sort(numbers.begin(), numbers.end(), compareStrings);

    list<string> numbers_with_5_segments = list_from_range(numbers, 3, 3);
    list<string> numbers_with_6_segments = list_from_range(numbers, 6, 3);

    string a = difference(numbers[1], numbers[2]); // intersect 1 and 7
    string bd = difference(numbers[2], numbers[1]); // intersect 1 and 4
    string b = find_singleton_intersection(bd, numbers_with_6_segments);
    string d = difference(bd, b);
    string abd = a + b + d;
    string g = find_singleton_difference(abd + numbers[0], numbers_with_5_segments);
    string abdg = abd + g;
    string f = find_singleton_difference(abdg, numbers_with_5_segments);
    string abdfg = abdg + f;
    string c = difference(numbers[0], f);
    string abcdfg = abdfg + c;
    string e = find_singleton_difference(abcdfg, numbers_with_5_segments);
    string abcdefg = abcdfg + e;

    map<char, char> m;
    m['a'] = a[0];
    m['b'] = b[0];
    m['c'] = c[0];
    m['d'] = d[0];
    m['e'] = e[0];
    m['f'] = f[0];
    m['g'] = g[0];

    return m;
}

int myPow(int x, unsigned int p)
{
  if (p == 0) return 1;
  if (p == 1) return x;
  
  int tmp = myPow(x, p/2);
  if (p%2 == 0) return tmp * tmp;
  else return x * tmp * tmp;
}

int main() {
    ifstream input("day8_input.txt");

    int sum = 0;
    aoc::readInput(input, [&sum](auto s){
        auto parts = aoc::splitLineT<string>(s, [](auto s){return s;}, " | ");
        vector<string> numbers = aoc::splitLineT<string>(parts[0], [](auto s){return s;});
        vector<string> output = aoc::splitLineT<string>(parts[1], [](auto s){return s;});

        map<char, char> m = get_map(numbers, output); // todo: Remove
        int value = 0;
        for (int i = 0; i < output.size(); i++) {
            string word = output[output.size() - i - 1];
            value = get_num(word, m);
            sum += value * myPow(10, i);
        }
    });

    cout << "Solution: " << sum << endl;
}