#include <iostream>
#include <string>
#include "../common/common.h"

using namespace std;

int main() {
    auto words = aoc::splitLine("I am doing the Advent-of-code-2021 challenge!");
    for (auto word : words)
        cout << word << " ";
    cout << endl;
}