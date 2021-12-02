cmake_minimum_required(VERSION 3.20.1)

project(AdventOfCode2021)

add_executable(Day1 src/day1/main.cpp)
configure_file(src/day1/day1_input.txt day1_input.txt)