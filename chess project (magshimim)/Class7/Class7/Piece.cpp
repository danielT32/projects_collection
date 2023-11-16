#include "Piece.h"

Piece::Piece( const bool color, const int type) : _color(color), _type(type)
{
}

Piece::~Piece()
{
}

int Piece::getType() const
{
	return this->_type;
}

bool Piece::getColor() const
{
	return this->_color;
}




