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

bool check_win(int* boards, int board, int row, int col) {
    // check row
    bool winner_found = true;
    for (int c = 0; c < 5; c++) {
        if (boards[get_index(board, row, c)] != -1) {
            winner_found = false;
            break;
        }
    }
    if (winner_found) return true;

    // check col
    winner_found = true;
    for (int r = 0; r < 5; r++) {
        if (boards[get_index(board, r, col)] != -1) {
            winner_found = false;
            break;
        }
    }
    
    return winner_found;
}

int get_sum(int* boards, int board) {
    int sum = 0;
    for (int r = 0; r < 5; r++) {
        for (int c = 0; c < 5; c++) {
            int index = get_index(board, r, c); 
            if (boards[index] != -1)
                sum += boards[index];
        }
    }
    return sum;
}

int get_solution(vector<int>& numbers, int* boards, int number_of_boards) {
    int solution = -1;
    for (int i = 0; i < numbers.size(); i++) {
        int n = numbers[i];
        for (int b = 0; b < number_of_boards; b++) {
            for (int r = 0; r < 5; r++) {
                for (int c = 0; c < 5; c++) {
                    if (boards[get_index(b, r, c)] == n) {
                        boards[get_index(b, r, c)] = -1; // mark field as found

                        // check for win
                        if (check_win(boards, b, r, c)) {
                            int sum = get_sum(boards, b);
                            cout << "sum: " << sum << endl;
                            cout << "n: " << n << endl;
                            return sum * n;
                        }
                    }
                }
            }
        }
    }
    throw domain_error("No solution was found D:");
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
    int solution1 = get_solution(numbers, boards, number_of_boards);

    cout << "Solution 1: " << solution1 << endl;

    delete boards;
}