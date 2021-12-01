cmake_minimum_required(VERSION 3.20.1)

project(AdventOfCode2021)

add_executable(Day1_1 src/day1/main1.cpp)
add_executable(Day1_2 src/day1/main2.cpp)
configure_file(src/day1/day1_input.txt day1_input.txt)