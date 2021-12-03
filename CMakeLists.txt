cmake_minimum_required(VERSION 3.20.1)

project(AdventOfCode2021)

add_subdirectory(src/common)

add_executable(day1 src/day1/main.cpp)
target_link_libraries(day1 PUBLIC Common)
target_include_directories(day1 PUBLIC "${PROJECT_SOURCE_DIR}/src/common")
configure_file(src/day1/day1_input.txt day1_input.txt)

add_executable(day2 src/day2/main.cpp)
target_link_libraries(day2 PUBLIC Common)
target_include_directories(day2 PUBLIC "${PROJECT_SOURCE_DIR}/src/common")
configure_file(src/day2/day2_input.txt day2_input.txt)

add_executable(day3 src/day3/main.cpp)
target_link_libraries(day3 PUBLIC Common)
target_include_directories(day3 PUBLIC "${PROJECT_SOURCE_DIR}/src/common")
configure_file(src/day3/day3_input.txt day3_input.txt)