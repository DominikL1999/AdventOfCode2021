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

    // solution
    assert(depths.size() >= 3);
    int count = 0;
    for (int i = 3; i < depths.size(); i++) {
        if (depths[i - 3] < depths[i])
            count++;
    }

    cout << "Number of increases: " << count << endl;
}