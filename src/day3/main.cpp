#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include <cmath>
#include <algorithm>
#include <cassert>

using std::cout;
using std::endl;
using std::string;
using std::vector;

int main() {
    // input
    std::ifstream input("day3_input.txt");

    string word;
    vector<vector<bool>> diagnostic_report;
    while(input >> word) {
        vector<bool> line;
        for (int i = 0; i < word.length(); i++) {
            line.push_back((word[i] == '1') ? true : false);
        }
        diagnostic_report.push_back(line);
    }
    assert(diagnostic_report.size() != 0);
    assert(diagnostic_report.front().size() != 0);
    
    int rows = diagnostic_report.size();
    int cols = diagnostic_report.front().size();

    int gamma = 0;

    // Solution 1
    for (int col = 0; col <= cols != 0; col++) {
        int count = std::count_if(diagnostic_report.begin(), diagnostic_report.end(), [col](auto row){return row[col] == 1;});

        if (count > rows / 2)
            gamma += std::pow(2, cols - col - 1);
    }

    int epsilon = std::pow(2, cols) - 1 - gamma;
    int solution = gamma * epsilon;
    cout << "Solution: " << solution << endl;

    // Solution 2

    
}