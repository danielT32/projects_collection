#pragma once
#include "Piece.h"
#include "Helper.h"
#include <iostream>
#include <string>

class Bishop : public Piece
{
public:
	Bishop(const bool color);
	~Bishop();
	virtual int checkValidMove(const std::string loc, const std::string board) ;
	virtual void fixPoan();
};