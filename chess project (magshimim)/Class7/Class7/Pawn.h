#pragma once
#include "Piece.h"
#include "Helper.h"
#include <iostream>
#include <string>

class Pawn : public Piece
{
private:
	bool _firstMove;
public:
	Pawn(const bool color);
	~Pawn();
	virtual int checkValidMove(const std::string loc, const std::string board);
	virtual void fixPoan();
};
