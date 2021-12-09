#include <iostream>
#include <fstream>
#include <vector>
#include <list>
#include <tuple>
#include <cassert>
#include "../common/common.h"

using namespace std;

int get_index(int x, int y, int max_x) {
    return y * (max_x + 1) + x;
}

int main() {
    FILE* input = fopen("day5_input.txt", "r");
    assert(input != nullptr);

    list<tuple<int, int, int, int>> lines;

    int p1x, p1y, p2x, p2y;
    int max_x = 0;
    int max_y = 0;
    while (fscanf(input, "%d,%d -> %d,%d", &p1x, &p1y, &p2x, &p2y) != EOF) {
        max_x = max(max_x, p1x);
        max_x = max(max_x, p2x);
        max_y = max(max_y, p1y);
        max_y = max(max_y, p2y);
        lines.push_back({p1x, p1y, p2x, p2y});
    }

    // Solution 1
    int* diagram = new int[(max_x + 1) * (max_y + 1)];

    for (int x = 0; x <= max_x; x++) {
        for (int y = 0; y <= max_y; y++) {
            diagram[get_index(x, y, max_x)] = 0;
        }
    }

    for (auto line : lines) {
        int x1 = get<0>(line);
        int y1 = get<1>(line);
        int x2 = get<2>(line);
        int y2 = get<3>(line);

        if (x1 == x2) {
            int abs_y_diff = abs(y2 - y1);
            int parity = (y2 - y1) / abs_y_diff; // either -1 or +1
            for (int d = 0; d <= abs_y_diff; d++) {
                int y = y1 + d * parity;
                diagram[get_index(x1, y, max_x)]++;
            }
        }
        else if (y1 == y2) {
            int abs_x_diff = abs(x2 - x1);
            int parity = (x2 - x1) / abs_x_diff; // either -1 or +1
            for (int d = 0; d <= abs_x_diff; d++) {
                int x = x1 + d * parity;
                diagram[get_index(x, y1, max_x)]++;
            }
        }
        else { // For Part 2
            int abs_diff = abs(x2 - x1);
            tuple<int, int> diff{(x2 - x1) / abs_diff, (y2 - y1) / abs_diff}; // unit vector
            for (int d = 0; d <= abs_diff; d++) {
                int x = x1 + get<0>(diff) * d;
                int y = y1 + get<1>(diff) * d;
                diagram[get_index(x, y, max_x)]++;
            }
        }
    }

    int count = 0;
    for (int x = 0; x <= max_x; x++) {
        for (int y = 0; y <= max_y; y++) {
            if (diagram[get_index(x, y, max_x)] > 1)
                count++;
        }
    }

    printf("Solution: %d", count);

    delete[] diagram;
}