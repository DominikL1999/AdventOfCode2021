#include <iostream>
#include <fstream>
#include <vector>
#include <cassert>
#include "../common/linereader.h"

using std::cout;
using std::endl;
using std::ifstream;
using std::vector;

int main() {
    vector<int> depths;
    aoc::readInput("day1_input.txt", [&depths](const std::string & line){depths.push_back(stoi(line)); });

    // solution 1
    assert(depths.size() >= 1);
    int count = 0;
    for (int i = 1; i < depths.size(); i++) {
        if (depths[i - 1] < depths[i])
            count++;
    }

    cout << "Solution 1: " << count << endl;

    // solution 2
    assert(depths.size() >= 3);
    count = 0;
    for (int i = 3; i < depths.size(); i++) {
        if (depths[i - 3] < depths[i])
            count++;
    }

    cout << "Solution 2: " << count << endl;
}