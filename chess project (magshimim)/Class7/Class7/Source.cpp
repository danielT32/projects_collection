#include <iostream>
#include "Manager.h"
#include "Helper.h"
#include <string>

int main()
{
	srand(time_t(NULL));
	Pipe p;
	int result, intChoice;
	char msgToGraphics[1024];
	std::string ans;
	std::string choice = "";
	std::string msg = "";
	std::cout << "Black or White? (b/w)" << std::endl;
	std::cin >> choice;
	while (choice.substr(0, 1).compare("w") != 0 && (choice.substr(0, 1)).compare("b"))
	{
		std::cout << "Wrong input!" << std::endl << "Black or White ? (b / w)" << std::endl;
		std::cin >> choice;
	}	
	if (choice.compare("w") == 0)
	{
		strcpy_s(msgToGraphics, "rnbkqbnrpppppppp################################PPPPPPPPRNBKQBNR0");
		intChoice = 0;
	}
	if (choice.compare("b") == 0)
	{
		strcpy_s(msgToGraphics, "rnbkqbnrpppppppp################################PPPPPPPPRNBKQBNR1");
		intChoice = 1;
	}

	Manager game(p, intChoice);
	game.printBoard();
	bool isConnect = p.connect();
	while (!isConnect)
	{
		std::cout << "cant connect to graphics" << std::endl;
		std::cout << "Do you try to connect again or exit? (0-try again, 1-exit)" << std::endl;
		std::cin >> ans;

		if (ans == "0")
		{
			std::cout << "trying connect again.." << std::endl;
			Sleep(5000);
			isConnect = p.connect();
		}
		else
		{
			p.close();
		}
	}

	p.sendMessageToGraphics(msgToGraphics);   
	std::string msgFromGraphics = p.getMessageFromGraphics();

	while (msgFromGraphics != "quit")
	{
		result = game.move(msgFromGraphics);
		game.printBoard();
		msgFromGraphics = game.sendMsgRtrnMsg(result);
	}
	p.close();

}
/*
This file servers as an example of how to use Pipe.h file.
It is recommended to use the following code in your project, 
in order to read and write information from and to the Backend
*/

//#include "Pipe.h"
//#include <iostream>
//#include <thread>
//
//using std::cout;
//using std::endl;
//using std::string;
//
//
//void main()
//{
//	srand(time_t(NULL));
//
//	
//	Pipe p;
//	bool isConnect = p.connect();
//	
//	string ans;
//	while (!isConnect)
//	{
//		cout << "cant connect to graphics" << endl;
//		cout << "Do you try to connect again or exit? (0-try again, 1-exit)" << endl;
//		std::cin >> ans;
//
//		if (ans == "0")
//		{
//			cout << "trying connect again.." << endl;
//			Sleep(5000);
//			isConnect = p.connect();
//		}
//		else 
//		{
//			p.close();
//			return;
//		}
//	}
//	
//
//	char msgToGraphics[1024];
//	// msgToGraphics should contain the board string accord the protocol
//	// YOUR CODE
//
//	strcpy_s(msgToGraphics, "rnbkqbnrpppppppp################################PPPPPPPPRNBKQBNR1"); // just example...
//	
//	p.sendMessageToGraphics(msgToGraphics);   // send the board string
//
//	// get message from graphics
//	string msgFromGraphics = p.getMessageFromGraphics();
//
//	while (msgFromGraphics != "quit")
//	{
//		// should handle the string the sent from graphics
//		// according the protocol. Ex: e2e4           (move e2 to e4)
//		
//		// YOUR CODE
//		strcpy_s(msgToGraphics, "YOUR CODE"); // msgToGraphics should contain the result of the operation
//
//		/******* JUST FOR EREZ DEBUGGING ******/
//		/*
//		int r = rand() % 10; // just for debugging......
//		msgToGraphics[0] = (char)(1 + '0');
//		msgToGraphics[1] = 0;
//		*/
//		/******* JUST FOR EREZ DEBUGGING ******/
//
//
//		// return result to graphics		
//		p.sendMessageToGraphics(msgToGraphics);   
//
//		// get message from graphics
//		msgFromGraphics = p.getMessageFromGraphics();
//	}
//
//	p.close();
//}
