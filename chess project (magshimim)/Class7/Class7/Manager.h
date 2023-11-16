#pragma once
#include "Pipe.h"
#include "Board.h"
#include <iostream>

class Manager
{
private:
	Pipe& _pipe;
	Board* _board;
public:
	Manager(Pipe& pipe1, bool player);
	//~Manager(); 
	int move(const std::string loc);
	void printBoard() const;
	std::string sendMsgRtrnMsg(int error);
};