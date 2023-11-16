#include "Bishop.h"

Bishop::Bishop(const bool color) : Piece(color, 4)
{
}

Bishop::~Bishop()
{
}

int Bishop::checkValidMove(const std::string loc, const std::string board) 
{
	std::string source = loc.substr(0, 2);
	std::string dest = loc.substr(2, 2);
	int srcLoc = locationToIndex(source);
	int dstLoc = locationToIndex(dest);
	if ((dstLoc - srcLoc) >= 7)
	{
		if ((dstLoc - srcLoc) % 7 == 0)
		{
			for (int i = 7; i < (dstLoc - srcLoc); i += 7)
			{
				if ((srcLoc + i) % 8 == 0)
				{
					return 6;
				}
				if (board[srcLoc + i] != '#')
					return 6;
			}
			return 0;
		}
		if ((dstLoc - srcLoc) % 9 == 0)
		{
			for (int i = 9; i < (dstLoc - srcLoc); i += 9)
			{
				if ((srcLoc + i) % 8 == 7)
				{
					return 6;
				}
				if (board[srcLoc + i] != '#')
					return 6;
			}
			return 0;
		}
	}
	if ((dstLoc - srcLoc) <= 7)
	{
		if ((dstLoc - srcLoc) % 7 == 0)
		{
			for (int i = -7; i > (dstLoc - srcLoc); i -= 7)
			{
				if ((srcLoc + i) % 8 == 0)
				{
					return 6;
				}
				if (board[srcLoc + i] != '#')
					return 6;
			}
			return 0;
		}
		if ((dstLoc - srcLoc) % 9 == 0)
		{
			for (int i = -9; i > (dstLoc - srcLoc); i -= 9)
			{
				if ((srcLoc + i) % 8 == 7)
				{
					return 6;
				}
				if (board[srcLoc + i] != '#')
					return 6;
			}
			return 0;
		}
	}
	return 6;
}

void Bishop::fixPoan()
{
}
