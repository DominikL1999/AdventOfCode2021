#include <iostream>
#include <fstream>
#include <map>
#include <cassert>
#include "../common/common.h"

// Solution 1
int get_cost1(int dist) {
    return dist;
}

// Solution 2
int get_cost2(int dist) {

}

int main() {
    // Input
    fstream input("day7_input.txt");
    string line;
    getline(input, line);
    cout << "line: " << line << endl;
    auto positions = aoc::splitLineT<int>(line, [](auto s) {return stoi(s);}, ",");

    assert(positions.size() != 0);
    
    map<int, int> counted_positions;
    int min_pos = INT_MAX;
    int max_pos = INT_MIN;
    for (auto pos : positions) {
        counted_positions[pos]++;
        min_pos = min(min_pos, pos);
        max_pos = max(max_pos, pos);
    }
    
    // Solution 1
    int length = max_pos - min_pos; // todo: rename
    int* costs = new int[length]; // todo: rename
    for (int i = 0; i < length; i++) costs[i] = 0;

    for (int i = 0; i < length; i++) {
        for (auto pair : counted_positions)
            costs[i] += (get_cost1(abs(pair.first - pair.second))) * pair.second;
    }

    int min_cost = INT_MAX;
    for (int i = 0; i < length; i++) {
        min_cost = min(min_cost, costs[i]);
    }

    cout << "Minimum costs: " << min_cost << endl;
}