#pragma once
#include "Board.h"

Board::Board(bool player) : _player(player)
{
	for (int i = 0; i < 64; i++)
		this->_board[0][i] = nullptr;
	int* location = new int[2];

	King* k1 = new King(0);
	King* k2 = new King(1);
	locationToArr(location, "a4");
	this->_board[location[0]][location[1]] = k1;
	locationToArr(location, "h4");
	this->_board[location[0]][location[1]] = k2;

	Queen* q1 = new Queen(0);
	Queen* q2 = new Queen(1);
	locationToArr(location, "a5");
	this->_board[location[0]][location[1]] = q1;
	
	locationToArr(location, "h5");
	this->_board[location[0]][location[1]] = q2;

	Rook* r1 = new Rook(0);
	Rook* r2 = new Rook(0);
	Rook* r3 = new Rook(1);
	Rook* r4 = new Rook(1);	
	locationToArr(location, "a1");
	this->_board[location[0]][location[1]] = r1;	
	locationToArr(location, "a8");
	this->_board[location[0]][location[1]] = r2;	
	locationToArr(location, "h1");
	this->_board[location[0]][location[1]] = r3;	
	locationToArr(location, "h8");
	this->_board[location[0]][location[1]] = r4;

	Knight* n1 = new Knight( 0);
	Knight* n2 = new Knight(0);
	Knight* n3 = new Knight( 1);
	Knight* n4 = new Knight(1);	
	locationToArr(location, "a2");
	this->_board[location[0]][location[1]] = n1;	
	locationToArr(location, "a7");
	this->_board[location[0]][location[1]] = n2;	
	locationToArr(location, "h2");
	this->_board[location[0]][location[1]] = n3;	
	locationToArr(location, "h7");
	this->_board[location[0]][location[1]] = n4;

	Bishop* b1 = new Bishop(0);
	Bishop* b2 = new Bishop(0);
	Bishop* b3 = new Bishop(1);
	Bishop* b4 = new Bishop(1);	
	locationToArr(location, "a3");
	this->_board[location[0]][location[1]] = b1;	
	locationToArr(location, "a6");
	this->_board[location[0]][location[1]] = b2;	
	locationToArr(location, "h3");
	this->_board[location[0]][location[1]] = b3;	
	locationToArr(location, "h6");
	this->_board[location[0]][location[1]] = b4;

	Pawn* p1 = new Pawn(0); 
	Pawn* p2 = new Pawn(0);
	Pawn* p3 = new Pawn(0);
	Pawn* p4 = new Pawn(0);
	Pawn* p5 = new Pawn(0);
	Pawn* p6 = new Pawn(0);
	Pawn* p7 = new Pawn(0);
	Pawn* p8 = new Pawn(0);	
	locationToArr(location, "b1");
	this->_board[location[0]][location[1]] = p1;	
	locationToArr(location, "b2");
	this->_board[location[0]][location[1]] = p2;	
	locationToArr(location, "b3");
	this->_board[location[0]][location[1]] = p3;	
	locationToArr(location, "b4");
	this->_board[location[0]][location[1]] = p4;	
	locationToArr(location, "b5");
	this->_board[location[0]][location[1]] = p5;	
	locationToArr(location, "b6");
	this->_board[location[0]][location[1]] = p6;	
	locationToArr(location, "b7");
	this->_board[location[0]][location[1]] = p7;	
	locationToArr(location, "b8");
	this->_board[location[0]][location[1]] = p8;
	
	Pawn* P1 = new Pawn(1);
	Pawn* P2 = new Pawn(1);
	Pawn* P3 = new Pawn(1);
	Pawn* P4 = new Pawn(1);
	Pawn* P5 = new Pawn(1);
	Pawn* P6 = new Pawn(1);
	Pawn* P7 = new Pawn(1);
	Pawn* P8 = new Pawn(1);
	locationToArr(location, "g1");
	this->_board[location[0]][location[1]] = P1;	
	locationToArr(location, "g2");
	this->_board[location[0]][location[1]] = P2;	
	locationToArr(location, "g3");
	this->_board[location[0]][location[1]] = P3;	
	locationToArr(location, "g4");
	this->_board[location[0]][location[1]] = P4;	
	locationToArr(location, "g5");
	this->_board[location[0]][location[1]] = P5;	
	locationToArr(location, "g6");
	this->_board[location[0]][location[1]] = P6;
	locationToArr(location, "g7");
	this->_board[location[0]][location[1]] = P7;
	locationToArr(location, "g8");
	this->_board[location[0]][location[1]] = P8;
	
	delete[] location;
}

Board::~Board()
{
	int i = 0, j = 0;

	for (i = 0; i < 8; i++)
	{
		for (j = 0; j < 8; j++)
		{
			if (this->_board[i][j] != nullptr)
			{
				delete this->_board[i][j];
			}
		}
	}
	delete[] this->_board;
}

std::string Board::boardToStr() const
{
	std::string def_Arr[] = PINDX_TO_STR;
	int* arrIndx;
	int type;
	bool color;
	std::string newBoard = "";
	for (int i = 0; i < 64; i++)
	{
		arrIndx = indexToArr(i);
		if (this->_board[arrIndx[0]][arrIndx[1]] != nullptr)
		{
			type = this->_board[arrIndx[0]][arrIndx[1]]->getType();
			color = this->_board[arrIndx[0]][arrIndx[1]]->getColor(); 
			if (color == false) // color = 0
			{
				newBoard += def_Arr[type][0];//CAP letter
			}
			else
			{
				newBoard += def_Arr[type][1];//small letter
			}
		}
		else//case there is no piece in that place
		{
			newBoard += '#';
		}
	}
	delete[] arrIndx;
	return newBoard; 
}

int Board::move(const std::string loc)
{
	bool couldEatBefore = false;
	int result = -1, otherKingLocArr;
	int* src = new int[2];
	int* dst = new int[2];
	int* kingLocation1 = new int[2];
	std::string myKingLoc = "", otherKingLoc = "";
	Piece* tmp;
	bool kingRule = false;//King can't be by king

	locationToArr(src, loc.substr(0, 2));
	std::string dstStr = loc.substr(2, 2);
	locationToArr(dst, dstStr);


	if (loc.substr(0, 2).compare(loc.substr(2, 2)) == 0)
	{
		result = 7; // same square
	}

	else if (this->_board[src[1]][src[0]] == nullptr)
	{
		result = 6; //Source has no piece
	}
	else if (this->_player != this->_board[src[1]][src[0]]->getColor())
	{
		result = 2;// wrong player, not this player's turn.
	}
	else if (this->_board[dst[1]][dst[0]] != nullptr)
	{
		if (this->_board[src[1]][src[0]]->getColor() == this->_board[dst[1]][dst[0]]->getColor())
		{
			result = 3; //Dest has piece of same player 
		}
	}

	if(result == -1)
	{
		result = this->_board[src[1]][src[0]]->checkValidMove(loc, boardToStr());
	}

	tmp = this->_board[dst[1]][dst[0]]; // save destanation for chess failure backup

	
	if (result == 0)
	{
		for (int i = 0; i < 8; i++) //check if check is happening
		{
			for (int j = 0; j < 8; j++)
			{
				if (this->_board[i][j] != nullptr && this->_board[i][j]->getType() == 0)
				{
					if (this->_board[i][j]->getColor() == this->_player)
						myKingLoc = arrToString(i, j);
					else
						otherKingLoc = arrToString(i, j);
				}
			}
		}
		for (int i = 0; i < 8; i++) //Black and white king testing at same time of check is made
		{
			for (int j = 0; j < 8; j++)
			{
				if (this->_board[i][j] != nullptr && this->_board[i][j]->getColor() != this->_player && this->_board[i][j]->checkValidMove(arrToString(i, j) + myKingLoc, boardToStr()) == 0)
				{
					couldEatBefore = true;
					i = 9;
					break;
				}
			}
		}
	}
	if (result == 0) 
	{
		if (this->_board[dst[1]][dst[0]] != nullptr && this->_board[dst[1]][dst[0]]->getType() == 0)
		{
			result = 8;
		}
		if (this->_board[dst[1]][dst[0]] != nullptr)
		{
			this->_board[dst[1]][dst[0]] = nullptr;
		}
		this->_board[dst[1]][dst[0]] = this->_board[src[1]][src[0]];
		this->_board[src[1]][src[0]] = nullptr;
	}
	if (result == 0)
	{
		for (int i = 0; i < 8; i++) //check if check is happening
		{
			for (int j = 0; j < 8; j++)
			{
				if(this->_board[i][j] != nullptr && this->_board[i][j]->getType() == 0)
				{
					if (this->_board[i][j]->getColor() == this->_player)
						myKingLoc = arrToString(i, j);
					else
						otherKingLoc = arrToString(i, j);
				}
			}
		}
		for (int i = 0; i < 8; i++) //Black and white king testing at same time of check is made
		{
			for (int j = 0; j < 8; j++)
			{
				if (this->_board[i][j] != nullptr && this->_board[i][j]->getColor() != this->_player && this->_board[i][j]->checkValidMove(arrToString(i, j) + myKingLoc, boardToStr()) == 0 && !couldEatBefore)
				{
					result = 4;
					i = 9;
					break;
				}
				if (this->_board[i][j] != nullptr && this->_board[i][j]->getColor() == this->_player && this->_board[i][j]->checkValidMove(arrToString(i, j) + otherKingLoc, boardToStr()) == 0)
				{
					if (this->_board[i][j]->getType() == 0)
					{
						result = 4;
						kingRule = true;
					}
					else
					{
						result = 1;
					}
					i = 9;
					break;
				}
			}
		}
	}
	if (result == 4)
	{
		this->_board[src[1]][src[0]] = this->_board[dst[1]][dst[0]];
		this->_board[dst[1]][dst[0]] = tmp;
		this->_board[src[1]][src[0]]->fixPoan();
	}
	if (result == 0 || result == 1)
	{
		this->_player = !(this->_player);
		delete tmp;
	}
	if (kingRule)
		result = 6;
	delete[] src;
	delete[] dst;
	delete[] kingLocation1;
	return result;
}


void Board::printBoard()
{
	std::string strBoard = boardToStr();
	std::string line;
	for (int i = 56; i >= 0; i -= 8)
	{
		for (int j = 0; j < 8; j++)
		{
			line += strBoard.substr(i+j, 1) + " ";
		}
		std::cout << line << std::endl;
		line = "";
	}
}

