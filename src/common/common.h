#include <functional>
#include <vector>
#include <string>

using namespace std;

namespace aoc
{
    bool readInput(string const& fileName, function<void(const string&)> lineProcessor);

    bool readInput(ifstream& istream, function<void(const string&)> lineProcessor);

    vector<string> splitLine(const string& _line, const string& delim = " ");

    vector<int> splitLineInt(const string& _line, const string& delim = " ");
}