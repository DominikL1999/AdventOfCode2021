#include <iostream>
#include <vector>
#include <cassert>
#include "../common/common.h"

#include <functional>

using namespace std;

void testSplitLine(const string& line, int supposed_size) {
    vector<string> words = aoc::splitLine(line, ",");
    if (supposed_size != words.size()) cout << "WRONG (" << words.size() << " instead of " << supposed_size << ")";
    else cout << "CORRECT ";
    for (auto word : words)
        cout << word << ",";
    cout << endl;
}

int main() {
    string line1 = "7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1";
    string line2 = "";
    string line3 = "hello";
    string line4 = ",1,2,3";
    string line5 = "1,2,3,";
    string line6 = "7,4,9,,5,,,7";
    testSplitLine(line1, 27);
    testSplitLine(line2, 0);
    testSplitLine(line3, 1);
    testSplitLine(line4, 3);
    testSplitLine(line5, 3);
    testSplitLine(line6, 5);
}