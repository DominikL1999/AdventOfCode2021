#include <iostream>
#include <fstream>
#include <vector>
#include <list>
#include <tuple>
#include <cassert>
#include "../common/common.h"

using namespace std;

// todo: try using multi-dimensional array
int get_index(int x, int y, int max_x) {
    return y * (max_x + 1) + x;
}

void print_diagram(int* diagram, int max_x, int max_y) {
    cout << "==============================" << endl;
    for (int x = 0; x <= max_x; x++) {
        for (int y = 0; y < max_y; y++) {
            printf("%d ", diagram[get_index(x, y, max_x)]);
        }
        printf("%d\n", diagram[get_index(x, max_y, max_x)]);
    }
    cout << "==============================" << endl;
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
        cout << flush;
        if (x1 == x2) {
            // x is the same
            if (y1 <= y2) {
                // todo: simplify this
                for (int y = y1; y <= y2; y++) {
                    diagram[get_index(x1, y, max_x)]++;
                }
            }
            else {
                for (int y = y1; y >= y2; y--) {
                    diagram[get_index(x1, y, max_x)]++;
                }
            }
        }
        else if (y1 == y2) {
            // y is the same
            if (x1 <= x2) {
                // todo: simplify this
                for (int x = x1; x <= x2; x++) {
                    diagram[get_index(x, y1, max_x)]++;
                }
            }
            else {
                for (int x = x1; x >= x2; x--) {
                    diagram[get_index(x, y1, max_x)]++;
                }
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

    printf("Solution 1: %d", count);

    delete[] diagram;
}