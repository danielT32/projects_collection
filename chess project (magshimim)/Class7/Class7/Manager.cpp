#include "Manager.h"

Manager::Manager(Pipe& pipe1, bool player) : _pipe(pipe1)
{
	this->_board = new Board(player);
}

//Manager::~Manager()
//{
//	delete this->_board;
//}

int Manager::move(const std::string loc)
{
	return this->_board->move(loc);
}

void Manager::printBoard() const
{
	this->_board->printBoard();
	std::cout << std::endl << std::endl;
}

std::string Manager::sendMsgRtrnMsg(int error)
{
	char msgToGraphics[1024];
	std::string strRslt = std::to_string(error) + " ";
	strcpy_s(msgToGraphics, strRslt.c_str());
	this->_pipe.sendMessageToGraphics(msgToGraphics);
	return this->_pipe.getMessageFromGraphics();
}



