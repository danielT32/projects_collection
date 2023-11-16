#include "Knight.h"

Knight::Knight(const bool color) : Piece(color, 3)
{
}

Knight::~Knight()
{
}

int Knight::checkValidMove(const std::string loc, const std::string board) 
{
	std::string source = loc.substr(0, 2);
	std::string dest = loc.substr(2, 2);
	int srcLoc = locationToIndex(source);
	int dstLoc = locationToIndex(dest);
	if (dstLoc == srcLoc + 6 || dstLoc == srcLoc + 10 || dstLoc == srcLoc + 15 || dstLoc == srcLoc + 17 || dstLoc == srcLoc - 6 || dstLoc == srcLoc - 10 || dstLoc == srcLoc - 15 || dstLoc == srcLoc - 17)
	{
		return 0;
	}
	return 6;
}

void Knight::fixPoan()
{
}
