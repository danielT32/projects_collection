#pragma once
#include "Piece.h"
#include "Helper.h"
#include <iostream>
#include <string>

class Knight : public Piece
{
public:
	Knight(const bool color);
	~Knight();
	virtual int checkValidMove(const std::string loc, const std::string board);
	virtual void fixPoan();

};