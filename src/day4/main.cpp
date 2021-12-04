#include <iostream>
#include <fstream>
#include <vector>
#include <tuple>
#include <cassert>
#include "../common/common.h"

using namespace std;

int get_index(int board, int row, int col) {
    return board * 25 + row * 5 + col;
}

tuple<int, int, int> get_tuple(int index) {
    int board = index % 25;
    int row = (index - board * 25) % 5;
    int col = index % 5;

    return {board, row, col};
}

int main() {
    ifstream input("day4_input.txt");

    int count = 0;
    aoc::readInput(input, [&count](auto s){count++;});
    int number_of_boards = (count - 1) / 6;
    int* boards = new int(number_of_boards * 5 * 5);

    input.clear();
    input.seekg(0);
    
    string line;
    getline(input, line);
    auto words = aoc::splitLine(line, ",");
    vector<int> numbers;
    numbers.reserve(words.size());
    for (auto word : words) {
        try {
            numbers.push_back(stoi(word));
        } catch (const std::exception& e) {
            cout << "exception was thrown for word \"" << word << "\": ";
            throw e;
        }
    }

    for (int b = 0; b < number_of_boards; b++) {
        string line;
        getline(input, line); // skip empty line

        for (int r = 0; r < 5; r++) {
            getline(input, line);
            auto words = aoc::splitLine(line);
            for (int c = 0; c < 5; c++) {
                int n = stoi(words[c]);
                boards[get_index(b, r, c)] = n;
            }
        }
    }

    // Solution 1

    delete boards;
}