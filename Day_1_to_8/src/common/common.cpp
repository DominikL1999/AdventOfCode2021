#include "common.h"

#include <iostream> // todo: remove
#include <algorithm>
#include <fstream>

namespace aoc {
    bool readInput(string const& fileName, function<void(const string&)> lineProcessor) {
        auto in = ifstream(fileName);

        if (!in.is_open()) return false;

        string line;
        while (getline(in, line))
        {
            lineProcessor(line);
        }

        return true;
    }

    bool readInput(ifstream& istream, function<void(const string&)> lineProcessor) {
        string line;
        while (getline(istream, line))
            lineProcessor(line);
        
        return true;
    }
}