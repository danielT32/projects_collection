#pragma once
#include <iostream>

class Piece
{
protected:
	int _type;
	bool _color;
public:
	Piece(const bool color, const int type);
	~Piece();
	int getType() const;
	bool getColor() const;
	virtual void fixPoan() = 0; // was set the function here because of lack of time
	virtual int checkValidMove(const std::string loc, const std::string board) = 0;
	
};
