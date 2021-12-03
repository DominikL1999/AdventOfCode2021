#include <functional>
#include <list>
#include <string>

using namespace std;

namespace aoc
{
    bool readInput(string const& fileName, function<void(const string&)> lineProcessor);

    list<string> splitLine(const string& _line, const char delim = ' ');
}