#pragma once
#include <string>
#include <iostream>

//Turn location in string to index in array
void locationToArr(int* arr, std::string location);

//Turn location string to index in string (board)
int locationToIndex(std::string location);

//turn index (like loop) to arr index
int* indexToArr(int indx);

//Turn loop index to board string
std::string arrToString(int i, int j);
