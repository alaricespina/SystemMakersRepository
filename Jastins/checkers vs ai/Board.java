package checkers;

import java.util.ArrayList;

public class Board{
	int size;
	int red, black;
	int rQueen, bQueen;
	ArrayList<Piece> redPieces;
	ArrayList<Piece> blackPieces;
	Square[][] squares = new Square[8][8];
	
	public Board() {
		this.redPieces = new ArrayList<>();
		this.blackPieces = new ArrayList<>();
		this.setPieces();
		
		this.red = 12;
		this.black = 12;
		this.rQueen = 0;
		this.bQueen = 0;
		this.size = 8;
		
		int x = 0, y = 0;
		for(int i = 0; i < size; i++) {
			for(int j = 0; j< size; j++) {
					if((j+i) % 2 == 0)
						squares[i][j] = new Square(Constants.BLACK);
					else{
						squares[i][j] = new Square(Constants.WHITE);
						if(i < 3)
							squares[i][j].setPiece(redPieces.get(x++));
						else if(i > 4)
							squares[i][j].setPiece(blackPieces.get(y++));
					}
			}
		}
	}
	
	public Board(Board b) { //Deepcopy
		this.size = b.size;
		this.red = b.red;
		this.black = b.black;
		this.rQueen = b.rQueen;
		this.bQueen = b.bQueen;
		
		this.redPieces = new ArrayList<>();
		this.blackPieces = new ArrayList<>();
		
		for(int i = 0; i < b.redPieces.size(); i++) {
			this.redPieces.add(new Piece(b.redPieces.get(i)));
		}
		for(int i = 0; i < b.blackPieces.size(); i++) {
			this.blackPieces.add(new Piece(b.blackPieces.get(i)));
		}
		for(int i = 0; i < size; i++) {
			for(int j = 0; j < size; j++) {
				this.squares[i][j] = new Square(b.squares[i][j]);
			}
		}
		
		for(Piece p: redPieces)
			this.squares[p.y][p.x].setPiece(p);
		
		for(Piece p: blackPieces)
			this.squares[p.y][p.x].setPiece(p);
		
	}
	
	public void setPieces() {
		redPieces.add(new Piece(Constants.RED, 1, 0));
		redPieces.add(new Piece(Constants.RED, 3, 0));
		redPieces.add(new Piece(Constants.RED, 5, 0));
		redPieces.add(new Piece(Constants.RED, 7, 0));
		redPieces.add(new Piece(Constants.RED, 0, 1));
		redPieces.add(new Piece(Constants.RED, 2, 1));
		redPieces.add(new Piece(Constants.RED, 4, 1));
		redPieces.add(new Piece(Constants.RED, 6, 1));
		redPieces.add(new Piece(Constants.RED, 1, 2));
		redPieces.add(new Piece(Constants.RED, 3, 2));
		redPieces.add(new Piece(Constants.RED, 5, 2));
		redPieces.add(new Piece(Constants.RED, 7, 2));
		
		blackPieces.add(new Piece(Constants.BLACK, 0, 5));
		blackPieces.add(new Piece(Constants.BLACK, 2, 5));
		blackPieces.add(new Piece(Constants.BLACK, 4, 5));
		blackPieces.add(new Piece(Constants.BLACK, 6, 5));
		blackPieces.add(new Piece(Constants.BLACK, 1, 6));
		blackPieces.add(new Piece(Constants.BLACK, 3, 6));
		blackPieces.add(new Piece(Constants.BLACK, 5, 6));
		blackPieces.add(new Piece(Constants.BLACK, 7, 6));
		blackPieces.add(new Piece(Constants.BLACK, 0, 7));
		blackPieces.add(new Piece(Constants.BLACK, 2, 7));
		blackPieces.add(new Piece(Constants.BLACK, 4, 7));
		blackPieces.add(new Piece(Constants.BLACK, 6, 7));
	}
	
	public void printSquares() {
		System.out.println("\n X  0 1 2 3 4 5 6 7\nY");
		for(int i = 0; i < this.size; i++) {
			System.out.print(i + "   ");
			for(int j = 0; j < this.size; j++) {
				if(this.squares[i][j].getPiece() != null) {
					if(this.squares[i][j].getPiece().color == Constants.RED) {
						if(this.squares[i][j].getPiece().upgrade)
							System.out.print("R ");
						else
							System.out.print("r ");
					}
						
					else {
						if(this.squares[i][j].getPiece().upgrade)
							System.out.print("B ");
						else
							System.out.print("b ");
					}
				}
				else {
					System.out.print("- ");
				}				
			}
			System.out.println();
		}
		
	}
	
	public ArrayList<Move> getMovesofPiece(Square square){
		
		ArrayList<Move> moveList = new ArrayList<>();

		boolean taken = false;
		
		int x = square.getPiece().x, y = square.getPiece().y;
		
		if(square.getPiece().color == Constants.BLACK || square.getPiece().upgrade) 
		{
			scanUPLEFT(moveList, null, x-1, y-1, square.getPiece().color, square.getPiece().upgrade);
			scanUPRIGHT(moveList, null, x+1, y-1, square.getPiece().color, square.getPiece().upgrade);
		}
		
		if(square.getPiece().color == Constants.RED || square.getPiece().upgrade) 
		{
			scanDLEFT(moveList, null, x-1, y+1, square.getPiece().color, square.getPiece().upgrade);
			scanDRIGHT(moveList, null, x+1, y+1, square.getPiece().color, square.getPiece().upgrade);
		}
		
		if(moveList.size() > 0)
			for(Move m: moveList)
				if(m.jumped != null && m.jumped.size() > 0)
					taken = true;
		
		int i = 0;
		if(taken)
			while(i < moveList.size()) {
				if(moveList.get(i).jumped == null || moveList.get(i).jumped.size() == 0) 
					moveList.remove(i);
				else
					i++;
			}
				
		return moveList;
	}
	
	public void scanUPLEFT(ArrayList<Move> moveList, ArrayList<Piece> pieceList, int x, int y, Constants color, boolean upgrade) {
		if(y >= 0 && y < size && x >= 0 && x < size) 
		{
			if(pieceList == null)
				pieceList = new ArrayList<>();
			
			if(squares[y][x].getPiece() != null && squares[y][x].getPiece().color != color) 
			{
				if(y-1 >= 0 && x-1 >= 0 && squares[y-1][x-1].getPiece() == null)
				{
					int len1;
					boolean cond = false;
					
					pieceList.add(squares[y][x].getPiece());
					len1 = pieceList.size();
					
					scanUPLEFT(moveList, pieceList,x-1, y-1, color, upgrade);
					
					if(len1 != pieceList.size()) {
						cond = true;
						while(pieceList.size() != len1)
							pieceList.remove(pieceList.size() - 1);
					}
					
					scanUPRIGHT(moveList, pieceList, x, y-2, color, upgrade);

					if(upgrade) {
						if(len1 != pieceList.size()) {
							cond = true;
							while(pieceList.size() != len1)
								pieceList.remove(pieceList.size() - 1);
						}
						scanDLEFT(moveList, pieceList, x-2, y, color, upgrade);
					}
					if(pieceList.size() == len1 && !cond) {
						moveList.add(new Move(x-1, y-1));
						if(pieceList.size() > 0)
							for(Piece p: pieceList)
								moveList.get(moveList.size() - 1).addJumped(p);
					}
				}
			}
			else if(squares[y][x].getPiece() == null) { 
				if((color == Constants.BLACK && pieceList.size() == 0 ) || upgrade)
				{
					moveList.add(new Move(x, y));
				}
			}
		}
		return;
	}
	
	public void scanUPRIGHT(ArrayList<Move> moveList, ArrayList<Piece> pieceList, int x, int y, Constants color, boolean upgrade) {
		if(y >= 0 && y < size && x >= 0 && x < size) 
		{   					
			if(pieceList == null)
				pieceList = new ArrayList<>();
			
			if(squares[y][x].getPiece() != null && squares[y][x].getPiece().color != color) 
			{
				if(y-1 >= 0 && x+1 < size && squares[y-1][x+1].getPiece() == null)
				{
					int len1;
					boolean cond = false;
					
					pieceList.add(squares[y][x].getPiece());
					len1 = pieceList.size();
					
					scanUPRIGHT(moveList, pieceList, x+2, y-2, color, upgrade);
					
					if(len1 != pieceList.size()) {
						cond = true;
						while(pieceList.size() != len1)
							pieceList.remove(pieceList.size() - 1);
					}
					scanUPLEFT(moveList, pieceList, x, y-2, color, upgrade);
					
					if(upgrade) {
						if(len1 != pieceList.size()) {
							cond = true;
							while(pieceList.size() != len1)
								pieceList.remove(pieceList.size() - 1);
						}
						scanDRIGHT(moveList, pieceList, x+2, y, color, upgrade);
					}
					
					if(pieceList.size() == len1 && cond != true) {
						moveList.add(new Move(x+1, y-1));
						if(pieceList.size() > 0)
						for(Piece p: pieceList)
							moveList.get(moveList.size() - 1).addJumped(p);
					}
				}
			}
			else if(squares[y][x].getPiece() == null) 
				if((color == Constants.BLACK && pieceList.size() == 0 ) || upgrade)
					moveList.add(new Move(x, y));
		}
	}
	
	public void scanDLEFT(ArrayList<Move> moveList, ArrayList<Piece> pieceList, int x, int y, Constants color, boolean upgrade) {
		if(y >= 0 && y < size && x >= 0 && x < size) 
		{
			if(pieceList == null)
				pieceList = new ArrayList<>();
			
			if(squares[y][x].getPiece() != null && squares[y][x].getPiece().color != color)
			{
				if(y+1 < size && x-1 >= 0 && squares[y+1][x-1].getPiece() == null)
				{
					int len1; boolean cond = false;
					
					pieceList.add(squares[y][x].getPiece());
					scanDLEFT(moveList, pieceList, x-2, y+2, color, upgrade);
					len1 = pieceList.size();
					
					if(len1 != pieceList.size()) {
						cond = true;
						while(pieceList.size() != len1)
							pieceList.remove(pieceList.size() - 1);
					}
					
					scanDRIGHT(moveList, pieceList, x, y+2, color, upgrade);
	
					if(upgrade) {
						if(len1 != pieceList.size()) {
							cond = true;
							while(pieceList.size() != len1)
								pieceList.remove(pieceList.size() - 1);
						}
						scanUPLEFT(moveList, pieceList, x-2, y, color, upgrade);
					}
					if(pieceList.size() == len1 && !cond) {
						moveList.add(new Move(x-1, y+1));
						if(pieceList.size() > 0)
						for(Piece p: pieceList)
							moveList.get(moveList.size() - 1).addJumped(p);
					}
				}
			}
			else if(squares[y][x].getPiece() == null)
				if((color == Constants.RED && pieceList.size() == 0 ) || upgrade)
					moveList.add(new Move(x, y));
		}
	}
	
	public void scanDRIGHT(ArrayList<Move> moveList, ArrayList<Piece> pieceList, int x, int y, Constants color, boolean upgrade) {
		if(y >= 0 && y < size && x >= 0 && x < size) 
		{
			if(pieceList == null)
				pieceList = new ArrayList<>();
			
			if(squares[y][x].getPiece() != null && squares[y][x].getPiece().color != color) 
			{
				if(y+1 < size && x+1 < size && squares[y+1][x+1].getPiece() == null)
				{
					int len1; boolean cond = false;
					
					pieceList.add(squares[y][x].getPiece());
					scanDRIGHT(moveList, pieceList, x+2, y+2, color, upgrade);
					len1 = pieceList.size();
					
					if(len1 != pieceList.size()) {
						cond = true;
						while(pieceList.size() != len1)
							pieceList.remove(pieceList.size() - 1);
					}
					
					scanDLEFT(moveList, pieceList, x, y+2, color, upgrade);

					if(upgrade) {
						if(len1 != pieceList.size()) {
							cond = true;
							while(pieceList.size() != len1)
								pieceList.remove(pieceList.size() - 1);
						}
						scanUPRIGHT(moveList, pieceList, x+2, y, color, upgrade);
					}
					
					if(pieceList.size() == len1 && !cond) {
						moveList.add(new Move(x+1, y+1));
						if(pieceList.size() > 0)
						for(Piece p: pieceList)
							moveList.get(moveList.size() - 1).addJumped(p);
					}
				}
			}
			else if(squares[y][x].getPiece() == null)
				if((color == Constants.RED && pieceList.size() == 0 ) || upgrade)
					moveList.add(new Move(x, y));
		}
	}

	
	public double evaluatePos(Constants color, Board b) {
		double blackSideScore = 0;
		double redSideScore = 0;
		
		if(b.red > 0)
			for(Piece p: b.redPieces)
				if(p.x == 0 || p.y == 0 || p.x == (b.size-1) || p.y == (b.size-1))
					redSideScore++;
		
		if(b.black > 0)
			for(Piece p: b.blackPieces)
				if(p.x == 0 || p.y == 0 || p.x == (b.size-1) || p.y == (b.size-1))
					blackSideScore++;
		
		
		if(color == Constants.BLACK) 
			return (b.black - b.red) + (((double)b.bQueen)*0.5 - ((double)b.rQueen)*0.5) + (0.2*blackSideScore - 0.2*redSideScore);
		
		
		else if (color == Constants.RED)
			return (b.red - b.black) + (((double)b.rQueen)*0.5 - ((double)b.bQueen)*0.5) + (0.2*redSideScore - 0.2*blackSideScore);
		
		return 0;
	}
	
	public ArrayList<Board> getAllBoards(Constants color, Board board) {
		ArrayList<Board> boardList = new ArrayList<>();
		
		if(color == Constants.RED)
			for(Piece p: board.redPieces) 
			{
				ArrayList<Move> moves = getMovesofPiece(board.squares[p.y][p.x]);
				for(Move m: moves) {
					Board b = simulateMove(m.desX, m.desY, board, board.squares[p.y][p.x], moves);
					boardList.add(b);
				}
			}
		
		else
			for(Piece p: board.blackPieces) 
			{
				ArrayList<Move> moves = getMovesofPiece(board.squares[p.y][p.x]);
				for(Move m: moves) {
					Board b = simulateMove(m.desX, m.desY, board, board.squares[p.y][p.x], moves);
					boardList.add(b);
				}
			}
		return boardList;
	}
	
	public Board miniMax(Board board, int depth, boolean isMax, double alpha, double beta, Constants color, int[] count) {
		if(depth < 0 || board.getWinner() == color) {
			return board;
		}
		if(isMax)
		{
			double max = (-1 * Double.MAX_VALUE);
			Board bestBoard = null;
			ArrayList<Board> boardList = getAllBoards(color, board);
			
			count[3]++;
			if(depth == 2)
				count[0]++;
			if(depth == 1)
				count[1]++;
			if(depth == 0)
				count[2]++;
			
			MoveOrdering.bestFirstOrder(boardList, color);
			for(int i = 0; i < boardList.size(); i++) 
			{
				Board val = miniMax(boardList.get(i), depth - 1, !isMax, alpha, beta, Constants.BLACK, count);
				max = Math.max(evaluatePos(color, val), max);
				alpha = Math.max(alpha, evaluatePos(color, val));
				if(max == evaluatePos(color,val)) {
					bestBoard = boardList.get(i);
				}
				if(beta <= alpha)
					break;
			}
			return bestBoard; 
		}
		else 
		{
			double min = (Double.MAX_VALUE);
			Board bestBoard = null;
			ArrayList<Board> boardList = getAllBoards(color, board);
			
			count[3]++;
			if(depth == 2)
				count[0]++;
			if(depth == 1)
				count[1]++;
			if(depth == 0)
				count[2]++;
			
			MoveOrdering.bestFirstOrder(boardList, color);
			for(int i = 0; i < boardList.size(); i++) 
			{
				Board val = miniMax(boardList.get(i), depth - 1, !isMax, alpha, beta, Constants.RED, count);
				min = Math.min(val.evaluatePos(color, val), min);
				beta = Math.min(beta, evaluatePos(color, val));
				if(min == val.evaluatePos(color, val)) {
					bestBoard = boardList.get(i);
				}
				if(beta <= alpha)
					break;
			}
			return bestBoard; 
		}
	}

	public Board simulateMove(int X, int Y, Board board, Square square, ArrayList<Move> moves) {
		Board boardCopy =  new Board(board);
		int x = square.getPiece().x, y = square.getPiece().y;
		boardCopy.movePiece(boardCopy.squares[y][x], X, Y, moves, boardCopy);
		return boardCopy;
	}
	
	public boolean movePiece(Square square, int x1, int y1, ArrayList<Move> moves, Board board){
		Piece tempPiece;
		int x = square.getPiece().x;
		int y = square.getPiece().y;
		
		if(	x1 < 0 || x1 >= size || y1 < 0 || y1 >= size ||
			(x1 == x+1 && y1 == y) || (x1 == x-1 && y1 == y) ||
			(x1 == x && y1 == y+1) || (x1 == x && y1 == y-1))
		{
			System.out.println();
			System.out.println("==Error: Invalid Move==");
			
			try {
				Thread.sleep(1000);
			} catch (InterruptedException e) {
				e.printStackTrace();
			}
			return false;
		}
		else
		{
			int i = 0;
			if(moves.size() > 0) {
				for(Move m: moves) 
				{
					if(m.desX == x1 && m.desY == y1) 
					{
						if(m.jumped != null && m.jumped.size() > 0)
							for(Piece p: m.jumped) {
								squares[p.y][p.x].setPiece(null);
								if(p.color == Constants.BLACK) {
									int j = 0;
									while(j < board.blackPieces.size()) {
										if(board.blackPieces.get(j).x == p.x && board.blackPieces.get(j).y == p.y) {
											board.blackPieces.remove(j);
											break;
										}
										j++;
									}
									board.black -= 1;
								}
								else {
									int j = 0;
									while(j < board.redPieces.size()) {
										if(board.redPieces.get(j).x == p.x && board.redPieces.get(j).y == p.y) {
											board.redPieces.remove(j);
											break;
										}
										j++;
									}
									board.red -=1;
								}
							}
						break;
					}
					if(i == moves.size() - 1) {
						System.out.println();
						System.out.println("==Error: Invalid Move==");
						try {
							Thread.sleep(1000);
						} catch (InterruptedException e) {
							e.printStackTrace();
						}
						return false;
					}
					i++;
				}
			}
			else {
				System.out.println();
				System.out.println("==Error: Invalid Move==");
				try {
					Thread.sleep(1000);
				} catch (InterruptedException e) {
					e.printStackTrace();
				}
				return false;
			}
				
			if(squares[y1][x1].getPiece() == null) 
			{
				tempPiece = square.getPiece();
				square.setPiece(null);
				tempPiece.setPos(x1, y1);
				upgradePiece(tempPiece);
				squares[y1][x1].setPiece(tempPiece);
				return true;
			}
		}
		return false;
	}
	
	public void upgradePiece(Piece piece) {
		if(piece.color == Constants.BLACK) {
			if(piece.y == 0)
				piece.setQueen();
			this.bQueen += 1;
		}
		else {
			if(piece.y == size - 1)
				piece.setQueen();
			this.rQueen += 1;
		}
			
	}
	
	public Constants getWinner() {
		if(red <= 0)
			return Constants.BLACK;
		if(black <= 0)
			return Constants.RED;
		
		return null;
	}
}
