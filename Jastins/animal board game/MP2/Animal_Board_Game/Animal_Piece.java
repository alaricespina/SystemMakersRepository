package Animal_Board_Game;
import java.awt.Image;

//package test2;

/**
 * @author Justin T. Ayuyao
 * <p>
 * Class pertaining to animal pieces which will be played on the game board.
 * <p>
 * Each piece has a rank, a name or type, color, and an image associated with it. 
 */
public class Animal_Piece{
    private int rank;
    private String name;
    private String color;
    private Image pieceImage;
    
	/**Creates an animal piece object given an integer pertaining to rank of animal
	 * and a string pertaining to the type of animal
	 * @param r Integer to be assigned to integer rank of the animal
	 * @param n String to be assigned to the name of the animal
	 */
	public Animal_Piece(int r, String n){
        this.rank = r;
        this.name = n;
    }

    /**Gets the rank of the animal piece
     * @return Integer value of the rank of the animal
     */
    public int getRank(){
        return rank;
    }

    /**Gets the String color of the animal piece
     * @return String value of the name or type of the animal
     */
    public String getColor() {
        return color;
    }
    
    /**Gets the String type or name of the animal piece
     * @return String name or type of the animal piece
     */
    public String getName(){
        return name;
    }

    /**Gets the Piece Image assigned to the piece
     * @return Image value or PieceImage of the Animal Piece
     */
    public Image getPieceImage() {
		return pieceImage;
	}

    /**Sets the color of the animal piece to the passed string
     * @param c Color to be assigned to the animal piece
     */
    public void setColor(String c) {
        this.color = c;
    }

	/**Sets the image of the piece given an Image parameter
	 * @param p Image to be assigned to the Image of the Animal Piece
	 */
	public void setPieceImage(Image p) {
		this.pieceImage = p;
	}

    /**Sets the rank of the animal piece given an integer
     * @param r Integer rank to be assigned to the animal piece
     */
    public void setRank(int r){
        this.rank = r;
    }
}
