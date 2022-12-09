package checkers;


import java.util.Scanner;

public class Game {
	Board gameBoard;
	Constants win;
	
	public Game() {
		gameBoard = new Board();
		win = null;
	}
	
	public void startGame() {
		int x, y, x1, y1;
		int j = 0;
		Constants turn = Constants.BLACK;
		String tempString[] = new String[4];
		Scanner scanner = new Scanner(System.in);			
		int[] count = new int[4];
		for(int i: count)
			i = 0;
		
		try {
			while(win == null) {
				
				gameBoard.printSquares();
				if(turn == Constants.BLACK) {
					System.out.println("Move format (x y x1 y1)\nYOUR MOVE: ");
					
					tempString = scanner.nextLine().split(" ");
					x = Integer.parseInt(tempString[0]);
					y = Integer.parseInt(tempString[1]);
					x1 = Integer.parseInt(tempString[2]);
					y1 = Integer.parseInt(tempString[3]);
					
					if(x == 0 && y == 0 && x1 == 0 && y1 == 0) {
						break;
					}
					
					if(gameBoard.squares[y][x].getPiece() == null)
						System.out.println("==Error: No Piece==");
					
					else
						if(gameBoard.movePiece(gameBoard.squares[y][x], x1, y1, gameBoard.getMovesofPiece(gameBoard.squares[y][x]), gameBoard))
							turn = Constants.RED;
				}
				
				else {
					gameBoard = gameBoard.miniMax(gameBoard, 3, true, -1 * Double.MAX_VALUE, Double.MAX_VALUE, turn, count);
					turn = Constants.BLACK;
					for(int i = 0; i < 3; i++)
						count[i] = 0;
				}

				win = gameBoard.getWinner();
			}
	
		} catch (Exception e) {
			e.printStackTrace();
		}
		System.out.println(win + " HAS WON");
	}
}
