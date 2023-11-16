#pragma once
#include "King.h"
#include "Helper.h"

King::King(const bool color) : Piece(color, 0)
{
}

King::~King()
{
}

int King::checkValidMove(const std::string loc, const std::string board) 
{
	std::string source = loc.substr(0, 2);
	std::string dest = loc.substr(2, 2);
	int srcLoc = locationToIndex(source);
	int dstLoc = locationToIndex(dest);
	if (dstLoc == srcLoc + 1 || dstLoc == srcLoc - 1 || dstLoc == srcLoc + 7 || dstLoc == srcLoc + 8 || dstLoc == srcLoc + 9 || dstLoc == srcLoc - 7 || dstLoc == srcLoc - 8 || dstLoc == srcLoc - 9)
	{
		return 0;
	}
	return 6;
}

void King::fixPoan()
{
}


