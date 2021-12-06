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

    vector<long long> splitLineLongLong(const string& _line, const string& delim);

    template<typename T>
    vector<T> splitLineT(const string& _line, function<T(const string&)> f, const string& delim);
}