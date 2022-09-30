package checkers;

import java.util.ArrayList;

public class Move {
	int desX, desY;
	int origX, origY;
	ArrayList<Piece> jumped;
	
	public Move(int x, int y) {
		this.desX = x;
		this.desY = y;
	}
	
	public Move(Piece p) {
		this.jumped = new ArrayList<>();
		jumped.add(p);
	}
	
	public void setXY(int x, int y) {
		this.desX = x;
		this.desY = y;
	}
	
	public void setOrig(int x, int y) {
		this.origX = x;
		this.origY = y;
	}
	
	public void addJumped(Piece p) {
		if(jumped == null)
			jumped = new ArrayList<>();
		jumped.add(p);
	}
	
	public void releaseJumped() {
		this.jumped = null;
	}
	
	public boolean isSameLoc(int x, int y) {
		return this.desX == x && this.desY == y;
	}
}
