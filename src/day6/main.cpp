#include <iostream>
#include <fstream>
#include <string>
#include "../common/common.h"

using namespace std;

// Constants
const long long SHORT_BIRTH_CYCLE = 7;
const long long LONG_BIRTH_CYCLE = 9;
// Solution 1
// const long long NUMBER_OF_DAYS = 80;
// Solution 2
const long long NUMBER_OF_DAYS = 256;

void simulate_day(long long* fish) {
    long long spawning_fish = fish[0];
    for (long long i = 1; i < LONG_BIRTH_CYCLE; i++) {
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

    auto longs = aoc::splitLineT<long long>(line, [](auto s) -> long long {return stoll(s);}, ",");

    long long fish[LONG_BIRTH_CYCLE];
    for (long long i = 0; i < LONG_BIRTH_CYCLE; i++) fish[i] = 0;

    // Solution
    for (auto f : longs)
        fish[f]++;
    
    for (long long i = 0; i < NUMBER_OF_DAYS; i++)
        simulate_day(fish);
    
    long long count = 0;
    for (long long i = 0; i < LONG_BIRTH_CYCLE; i++)
        count += fish[i];
    
    printf("Number of fish after %u days: %llu\n", NUMBER_OF_DAYS, count);
}