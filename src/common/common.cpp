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
    
    vector<int> splitLineInt(const string& _line, const string& delim) {
        vector<int> ints;
        string line{_line};

        size_t pos = 0;
        string token;
        while ((pos = line.find(delim)) != string::npos) {
            token = line.substr(0, pos);
            if (!token.empty())
                ints.push_back(stoi(token));
            line.erase(0, pos + delim.length());
        }
        if (!line.empty())
            ints.push_back(stoi(line));

        return ints;
    }
    
    vector<long long> splitLineLongLong(const string& _line, const string& delim) {
        vector<long long> long_longs;
        string line{_line};

        size_t pos = 0;
        string token;
        while ((pos = line.find(delim)) != string::npos) {
            token = line.substr(0, pos);
            if (!token.empty())
                long_longs.push_back(stoll(token));
            line.erase(0, pos + delim.length());
        }
        if (!line.empty())
            long_longs.push_back(stoll(line));

        return long_longs;
    }

    template<typename T>
    vector<T> splitLineT(const string& _line, function<T(const string&)> f, const string& delim) {
        vector<T> elements;
        string line{_line};

        size_t pos = 0;
        string token;
        while ((pos = line.find(delim)) != string::npos) {
            token = line.substr(0, pos);
            if (!token.empty())
                elements.push_back(f(token));
            line.erase(0, pos + delim.length());
        }
        if (!line.empty())
            elements.push_back(f(line));

        return elements;
    }
}