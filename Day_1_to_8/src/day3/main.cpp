#include <iostream>
#include <fstream>
#include <vector>
#include <list>
#include <tuple>
#include <cmath>
#include <algorithm>
#include <cassert>
#include "../common/common.h"

using namespace std;

int convert_to_decimal(vector<bool>& bin_number) {
    int n = 0;
    for (int i = 0; i < bin_number.size(); i++) {
        if (bin_number[i])
            n += std::pow(2, bin_number.size() - i - 1);
    }
    return n;
}

int get_saturation(vector<vector<bool>>& diagnostic_report, bool bit_criteria) {
    int n_cols = diagnostic_report.front().size();
    std::list<int> rows;
    for (int i = 0; i < diagnostic_report.size(); i++) {
        rows.push_front(i);
    }

    for (int col = 0; col < n_cols; col++) {
        int count = std::count_if(rows.begin(), rows.end(), [col, diagnostic_report](auto row){return diagnostic_report[row][col] == 1;});

        bool many_1s = count * 2 >= rows.size();

        rows.remove_if(
            [col, diagnostic_report, bit_criteria, many_1s]
            (auto row)
            {return (diagnostic_report[row][col] + many_1s + bit_criteria) % 2 == 0;});

        if (rows.size() == 1) break;
    }

    return convert_to_decimal(diagnostic_report[rows.front()]);
}

int main() {
    vector<vector<bool>> diagnostic_report;
    aoc::readInput("day3_input.txt", [&diagnostic_report](const std::string & line){
        vector<bool> row;
        for (auto c : line)
            row.push_back(c == '1' ? true : false);
        diagnostic_report.push_back(row);
    });

    assert(diagnostic_report.size() != 0);
    assert(diagnostic_report.front().size() != 0);
    
    int n_cols = diagnostic_report.front().size();

    // Solution 1
    int gamma = 0;
    for (int col = 0; col <= n_cols != 0; col++) {
        int count = std::count_if(diagnostic_report.begin(), diagnostic_report.end(), [col](auto row){return row[col] == 1;});

        if (count > diagnostic_report.size() / 2)
            gamma += std::pow(2, n_cols - col - 1);
    }

    int epsilon = std::pow(2, n_cols) - 1 - gamma;
    int solution = gamma * epsilon;
    cout << "gamma: " << gamma << endl;
    cout << "epsilon: " << epsilon << endl;
    cout << "Solution 1: " << solution << endl;

    // Solution 2
    int oxygen_generator_rating = get_saturation(diagnostic_report, false);
    int co2_scrubber_rating = get_saturation(diagnostic_report, true);

    cout << "Oxygen Generator Rating: " << oxygen_generator_rating << endl;
    cout << "CO2 Scrubber Rating: " << co2_scrubber_rating << endl;
    cout << "Solution 2: " << oxygen_generator_rating * co2_scrubber_rating << endl;
}