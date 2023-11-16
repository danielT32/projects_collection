#pragma once
#include "Piece.h"
#include "Helper.h"
#include <iostream>
#include <string>

class Rook : public Piece
{
public:
	Rook(const bool color);
	~Rook();
	virtual int checkValidMove(const std::string loc, const std::string board);
	virtual void fixPoan();

};