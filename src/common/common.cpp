#include "common.h"

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

    vector<string> splitLine(const string& line, const char delim) {
        vector<string> words;

        size_t start = 0;
        size_t pos = line.find(delim);
        while (pos <= line.size()) {
            string word = line.substr(start, pos - start);
            words.emplace_back(word);
            start = pos + 1;
            pos = line.find(delim, start);
        }

        words.emplace_back(line.substr(start));

        return words;
    }
}