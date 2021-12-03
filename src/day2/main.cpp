#include <iostream>
#include <sstream>
#include <fstream>
#include <string>
#include <vector>
#include <tuple>
#include <cassert>
#include "../common/linereader.h"

using namespace std;

int main() {
    vector<std::tuple<string, int>> commands;
    aoc::readInput("day2_input.txt", [&commands](const std::string & line){
        string direction;
        int value;
        int pos = line.find(' ');
        direction = line.substr(0, pos);
        value = stoi(line.substr(pos + 1, line.size() - pos - 1));
        commands.push_back({direction, value});
    });

    // Solution 1
    int horizontal_pos = 0;
    int depth = 0;
    for (int i = 0; i < commands.size(); i++) {
        string direction = std::get<0>(commands[i]);
        int value = std::get<1>(commands[i]);
        if (direction == "forward")
            horizontal_pos += value;
        else if (direction == "down")
            depth += value;
        else if (direction == "up")
            depth -= value;
    }

    cout << "Horizontal depth: " << horizontal_pos << endl;
    cout << "Vertical depth: " << depth << endl;
    cout << "Solution 1: " << horizontal_pos * depth << endl;

    // Solution 2
    horizontal_pos = 0;
    depth = 0;
    int aim = 0;
    for (int i = 0; i < commands.size(); i++) {
        string direction = std::get<0>(commands[i]);
        int value = std::get<1>(commands[i]);
        if (direction == "forward"){
            horizontal_pos += value;
            depth += aim * value;
        }
        else if (direction == "down")
            aim += value;
        else if (direction == "up")
            aim -= value;
    }

    cout << "Horizontal depth: " << horizontal_pos << endl;
    cout << "Vertical depth: " << depth << endl;
    cout << "Solution 2: " << horizontal_pos * depth << endl;
}