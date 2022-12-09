package Animal_Board_Game;
//package test2;

/**
 * @author Justin T. Ayuyao
 * <p>
 * Class pertaining to the tiles on a game board. The tile could be empty, a river,
 * <p>
 * a trap, a haven, or contain a piece. Some tiles also have their own respective
 * <p>
 * colors if they are a trap or haven. Every tile also contain their own position
 * <p>
 * with respect to the game board.
 */
public class Tile{
    
    private Positions pos;
    private boolean isEmpty;
    private boolean isRiver;
    private boolean isTrap;
    private boolean isHaven;
    private String color;
    private Animal_Piece piece;
    
    /**Creates a tile given 4 boolean values an a string. Initializes the tile's animal piece as null
     * @param e Boolean assigned if the tile is empty or not
     * @param r Boolean assigned if the tile is a river tile or not
     * @param t Boolean assigned if the tile is a trap tile or not
     * @param h Boolean assigned if the tile is a haven tile or not
     * @param c String assigned to the tile's color
     */
    public Tile(boolean e, boolean r, boolean t, boolean h, String c){
        this.isEmpty = e;
        this.isRiver = r;
        this.isTrap = t;
        this.isHaven = h;
        this.color = c;
        this.piece = null;
    }
    
    /**Get the boolean identifying if the tile is empty
     * @return Boolean attribute if the tile is empty or not
     */
    public boolean isEmpty(){
        return isEmpty;
    }

    /**Get the boolean identifying if the tile is a river
     * @return Boolean attribute if the tile is a river or not
     */
    public boolean isRiver(){
        return isRiver;
    }
    
    /**Get the boolean identifying if the tile is a trap
     * @return Boolean attribute if the tile is trap or not
     */
    public boolean isTrap() {
    	return isTrap;
    }
    
    /**Get the boolean identifying if the tile is a haven
     * @return Boolean attribute if the tile is a haven or not
     */
    public boolean isHaven() {
    	return isHaven;
    }
    
    /**Get the String pertaining to the color of the tile
     * @return String attribute pertaining to color or tile
     */
    public String getColor() {
    	return color;
    }

    /**Get the Positions of the tile
     * @return Position attribute of the tile
     */
    public Positions getPos(){
        return pos;
    }

    /**Get the Animal Piece of the tile
     * @return Animal Piece attribute of the tile
     */
    public Animal_Piece getPiece(){
        return piece;
    }

    /**Returns if the tile has an animal piece 
     * @return Boolean pertaining if the tile's piece is null or not
     */
    public boolean hasPiece(){
        return !(piece == null);
    }

    /**Set x and y position value of the tile's position given 2 integers
     * @param x Integer value to assign to x position of the tile's position 
     * @param y Integer value to assign to y position of the tile's position
     */
    public void setPos(int x, int y){
        if(pos == null)
        	pos = new Positions(x, y);
        else
        	pos.setXY(x, y);
    }
    
    /**Sets the boolean attribute pertaining to if the tile is empty or not
     * @param e Boolean value to assign to the tile's boolean attribute if the tile is empty
     */
    public void setEmpty(boolean e) {
    	this.isEmpty = e;
    }
    
    /**Sets the String color of the tile given a string
     * @param c String to assigned to color attribute of tile
     */
    public void setColor(String c) {
    	this.color = c;
    }
    
    /**Sets the piece of the tile given an animal piece
     * @param piece Animal Piece to be assigned to the animal piece attribute
     */
    public void setPiece(Animal_Piece piece){
        this.piece = piece;
    }

    /**Given an animal piece, returns true if the piece has the same color as the current tile, else return false 
     * @param piece Piece whose color will be compared to the current tile
     * @return Boolean if the piece has the same color of the current tile
     */
    public boolean isAllyTile(Animal_Piece piece) {
    	return piece.getColor().equalsIgnoreCase(this.color);
    }
    
}
