#include <iostream>
#include <fstream>
#include <string>
#include "../common/common.h"

using namespace std;

// Constants
const long NUMBER_OF_DAYS = 80;
const long SHORT_BIRTH_CYCLE = 7;
const long LONG_BIRTH_CYCLE = 9;

void simulate_day(long* fish) {
    long spawning_fish = fish[0];
    for (long i = 1; i < LONG_BIRTH_CYCLE; i++) {
        fish[i - 1] = fish[i];
    }
    fish[LONG_BIRTH_CYCLE - 1] = spawning_fish;
    fish[SHORT_BIRTH_CYCLE - 1] += spawning_fish;
}

int main() {

    // Input
    fstream input("day6_input.txt");

    string line;
    getline(input, line);

    auto ints = aoc::splitLineInt(line, ",");
    long fish[LONG_BIRTH_CYCLE];
    for (long i = 0; i < LONG_BIRTH_CYCLE; i++) fish[i] = 0;

    // Solution 1
    for (auto f : ints)
        fish[f]++;
    
    for (long i = 0; i < NUMBER_OF_DAYS; i++)
        simulate_day(fish);
    
    long count = 0;
    for (long i = 0; i < LONG_BIRTH_CYCLE; i++)
        count += fish[i];
    
    printf("Number of fish after %d days: %d\n", NUMBER_OF_DAYS, count);
}