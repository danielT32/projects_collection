#include "Pawn.h"

Pawn::Pawn(const bool color) : Piece(color, 5), _firstMove(true)
{
}

Pawn::~Pawn()
{
}

int Pawn::checkValidMove(const std::string loc, const std::string board)
{
	std::string source = loc.substr(0, 2);
	std::string dest = loc.substr(2, 2);
	int srcLoc = locationToIndex(source);
	int dstLoc = locationToIndex(dest);
	//Add case where pawn reaches other end of table and ask user which new piece he wants to be
	if ((dstLoc == srcLoc + 7 || dstLoc == srcLoc + 9) && board[dstLoc] != '#' && isupper(board[srcLoc]))
	{
		return 0;
	}
	else if ((dstLoc == srcLoc - 7 || dstLoc == srcLoc - 9) && board[dstLoc] != '#' && islower(board[srcLoc]))
	{
		return 0;
	}
	else if ((dstLoc == srcLoc + 16 || dstLoc == srcLoc - 16) && this->_firstMove)
	{
		if (board[srcLoc + 8] == '#' || board[srcLoc - 8] == '#')
		{
			this->_firstMove = false;
			return 0;
		}
	}
	else if (dstLoc == srcLoc + 8 && board[dstLoc] == '#' && isupper(board[srcLoc]))
	{
		this->_firstMove = false;
		return 0;
	}
	else if (dstLoc == srcLoc - 8 && board[dstLoc] == '#' && islower(board[srcLoc]))
	{
		this->_firstMove = false;
		return 0;
	}	
	return 6;
}

void Pawn::fixPoan()
{
	this->_firstMove = true;
}
