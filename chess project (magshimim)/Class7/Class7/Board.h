#pragma once
#include "Piece.h"
#include "King.h"
#include "Queen.h"
#include "Rook.h"
#include "Knight.h"
#include "Bishop.h"
#include "Pawn.h"
#include "Helper.h"

#define PINDX_TO_STR {"Kk", "Qq", "Rr", "Nn", "Bb", "Pp", "##"}
#define PINDX_LEN 7
#include <iostream>

class Board
{
private:
	Piece*  _board[8][8];
	bool _player;
public:
	Board(bool player);
	~Board();
	std::string boardToStr() const;
	int move(const std::string loc);
	void printBoard();

};
