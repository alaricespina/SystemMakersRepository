package checkers;

public class Square {
	Constants color;
	private Piece piece;
	
	public Square(Constants c) {
		this.color = c;
		this.piece = null;
	}
	
	public Square(Square s) {
		this.color = s.color;
	}
	
	public void setPiece(Piece p) {
		this.piece = p;
	}
	
	public Piece getPiece() {
		return this.piece;
	}
}
