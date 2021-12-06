#include <functional>
#include <vector>
#include <string>

using namespace std;

namespace aoc
{
    bool readInput(string const& fileName, function<void(const string&)> lineProcessor);

    bool readInput(ifstream& istream, function<void(const string&)> lineProcessor);

    template<typename T>
    vector<T> splitLineT(const string& _line, function<T(const string& s)> f, const string& delim = " ") {
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