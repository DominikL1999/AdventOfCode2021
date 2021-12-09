#include <iostream>
#include <fstream>
#include <map>
#include <cassert>
#include "../common/common.h"

int main() {
    // Input
    fstream input("day7_input.txt");
    string line;
    getline(input, line);
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
    
    // Solution
    
    // Calculate step_cost
    int max_step = max_pos - min_pos + 1;
    int* step_cost = new int[max_step];
    step_cost[0] = 0; // cost of a step of size 0 is 0
    // Note: step_cost[i] == (i + 1) nCr 2
    for (int i = 1; i < max_step; i++) {
        step_cost[i] = step_cost[i - 1] + i;
    }

    int* costs = new int[max_step];
    for (int i = 0; i < max_step; i++)
        costs[i] = 0;

    for (int i = 0; i < max_step; i++) {
        for (auto pair : counted_positions) {
            // // Solution 1
            // costs[i] += (get_cost1(abs(pair.first - i))) * pair.second;

            // Solution 2
            costs[i] += (step_cost[abs(pair.first - i)]) * pair.second;
        }
    }

    int min_cost = INT_MAX;
    for (int i = 0; i < max_step; i++) {
        min_cost = min(min_cost, costs[i]);
    }

    cout << "Minimum costs: " << min_cost << endl;

    delete[] costs;
    delete[] step_cost;
}