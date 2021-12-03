cmake_minimum_required(VERSION 3.20.1)

project(AdventOfCode2021)

add_executable(Day1 src/day1/main.cpp)
configure_file(src/day1/day1_input.txt day1_input.txt)

add_executable(Day2 src/day2/main.cpp)
configure_file(src/day2/day2_input.txt day2_input.txt)

add_executable(Day3 src/day3/main.cpp)
configure_file(src/day3/day3_input.txt day3_input.txt)