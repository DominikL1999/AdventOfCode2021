#include <functional>
#include <vector>
#include <string>

using namespace std;

namespace aoc
{
    bool readInput(string const& fileName, function<void(const string&)> lineProcessor);

    vector<string> splitLine(const string& _line, const char delim = ' ');
}