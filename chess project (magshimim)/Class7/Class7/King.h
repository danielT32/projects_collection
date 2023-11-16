#pragma once
#include "Piece.h"
#include "Helper.h"
#include <iostream>
#include <string>

class King : public Piece
{
public:
	King(const bool color);
	~King();
	virtual int checkValidMove(const std::string loc, const std::string board);
	virtual void fixPoan();

};