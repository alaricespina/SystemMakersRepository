package checkers;

public class Piece {
	boolean upgrade;
	Constants color;
	int x;
	int y;
	
	
	public Piece(Constants c, int Xpos, int Ypos) {
		this.color = c;
		this.x = Xpos;
		this.y = Ypos;
		this.upgrade = false;
	}

	public Piece(Piece piece) {
		this.upgrade = piece.upgrade;
		this.color = piece.color;
		this.x = piece.x;
		this.y = piece.y;
	}
	
	public void setQueen() {
		this.upgrade = true;
	}

	public void setPos(int X, int Y) {
		this.x = X;
		this.y = Y;
	}
}
