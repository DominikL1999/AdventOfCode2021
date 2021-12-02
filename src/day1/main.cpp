#include <iostream>
#include <fstream>
#include <vector>
#include <cassert>

using std::cout;
using std::endl;
using std::ifstream;
using std::vector;

int main() {
    // input
    ifstream input("day1_input.txt");

    vector<int> depths;
    int x;
    while (input >> x) {
        depths.push_back(x);
    }

    // solution 1
    assert(depths.size() >= 1);
    int count = 0;
    for (int i = 1; i < depths.size(); i++) {
        if (depths[i - 1] < depths[i])
            count++;
    }

    cout << "Number of increases: " << count << endl;

    // solution 2
    assert(depths.size() >= 3);
    count = 0;
    for (int i = 3; i < depths.size(); i++) {
        if (depths[i - 3] < depths[i])
            count++;
    }

    cout << "Number of increases: " << count << endl;
}