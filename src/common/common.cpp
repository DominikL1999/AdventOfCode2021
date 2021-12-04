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

    vector<string> splitLine(const string& _line, const string& delim) {
        vector<string> words;
        string line{_line};

        size_t pos = 0;
        string token;
        while ((pos = line.find(delim)) != string::npos) {
            token = line.substr(0, pos);
            if (!token.empty())
                words.push_back(token);
            line.erase(0, pos + delim.length());
        }
        if (!line.empty())
            words.push_back(line);

        return words;
    }

    // vector<string> splitLine(const string& line, const char delim) {
    //     vector<string> words;

    //     size_t start = 0;
    //     size_t pos = line.find(delim);
    //     while (pos <= line.size()) {
    //         string word = line.substr(start, pos - start);
    //         words.emplace_back(word);
    //         start = pos + 1;
    //         pos = line.find(delim, start);
    //     }

    //     words.emplace_back(line.substr(start));

    //     return words;
    // }
}