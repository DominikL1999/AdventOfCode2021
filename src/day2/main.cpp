#include <iostream>
#include <sstream>
#include <fstream>
#include <string>
#include <vector>
#include <tuple>
#include <cassert>

using std::cout;
using std::endl;
using std::string;
using std::vector;

int main() {
    // input
    std::ifstream input("day2_input.txt");

    string word;
    int x;
    vector<std::tuple<string, int>> commands;
    while (input >> word) {
        input >> x;

        commands.push_back({word, x});
    }

    // Solution 1
    int horizontal_pos = 0;
    int depth = 0;
    for (int i = 0; i < commands.size(); i++) {
        word = std::get<0>(commands[i]);
        x = std::get<1>(commands[i]);
        if (word == "forward")
            horizontal_pos += x;
        else if (word == "down")
            depth += x;
        else if (word == "up")
            depth -= x;
    }

    cout << "Horizontal depth: " << horizontal_pos << endl;
    cout << "Vertical depth: " << depth << endl;
    cout << "Solution 1: " << horizontal_pos * depth << endl;

    // Solution 2
    horizontal_pos = 0;
    depth = 0;
    int aim = 0;
    for (int i = 0; i < commands.size(); i++) {
        word = std::get<0>(commands[i]);
        x = std::get<1>(commands[i]);
        if (word == "forward"){
            horizontal_pos += x;
            depth += aim * x;
        }
        else if (word == "down")
            aim += x;
        else if (word == "up")
            aim -= x;
    }

    cout << "Horizontal depth: " << horizontal_pos << endl;
    cout << "Vertical depth: " << depth << endl;
    cout << "Solution 2: " << horizontal_pos * depth << endl;
}