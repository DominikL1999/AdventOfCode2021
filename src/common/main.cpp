#include <iostream>
#include <vector>
#include <cassert>
#include "../common/common.h"

#include <functional>

using namespace std;

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

int main() {
    string line = "1,2,3,4,5,6,7,8,9,10";
    
    auto numbers = aoc::splitLineT<long long>("1,2,3,4,5,6,7,8,9,10", [](const string& s) -> long long {return stoll(s);}, ",");

    for (auto number : numbers)
        cout << number << ",";
    cout << endl;

    for (auto number : numbers)
        printf("%llu,", number);
    printf("\n");
}