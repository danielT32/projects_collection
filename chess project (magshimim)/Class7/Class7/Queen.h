#pragma once
#include "Piece.h"
#include "Helper.h"
#include <iostream>
#include <string>

class Queen : public Piece
{
public:
	Queen(const bool color);
	~Queen();
	virtual int checkValidMove(const std::string loc, const std::string board);
	virtual void fixPoan();

};
