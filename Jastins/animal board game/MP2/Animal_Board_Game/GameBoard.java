package Animal_Board_Game;

//package test2;

import java.awt.Image;
import java.awt.image.BufferedImage;
import java.io.IOException;
import java.util.ArrayList;

import javax.imageio.ImageIO;

/**
 * @author Justin T. Ayuyao
 * <p>
 * The class pertaining to the game board containing all the tiles and it also 
 * <p>
 * manages the game state of the board.
 */
public class GameBoard {
    
    private final int WIDTH, HEIGHT;
    private Person[] players = new Person[2];
    
    private ArrayList<Animal_Piece> p1Pieces;
    private ArrayList<Animal_Piece> p2Pieces;
    
    private Tile[][] boardTiles;
    
    private ArrayList<Image> piecesImages;
    
    private boolean Win;

    /**Constructor used to build the game board. Initializes the win boolean as false, final width as 9, final height as 7.
     * <p>
     * Given 2 string parameters pertaining to names of player 1 and player 2, initializes the players of the game board.
     * <p>
     * Adds the pieces of the players to the pieces of the game board.
     * @param p1 String name to be assigned to player 1
     * @param p2 String name to be assigned to player 2
     */
    public GameBoard(String p1, String p2){
    	this.Win = false;
        this.WIDTH = 9;
        this.HEIGHT = 7;

        this.p1Pieces = new ArrayList<>();
        this.p2Pieces = new ArrayList<>();
        this.piecesImages = new ArrayList<>();
        this.boardTiles = new Tile[HEIGHT][WIDTH];

        this.players[0] = new Person(p1);
        this.players[1] = new Person(p2);
        
        for(Animal_Piece p: players[0].getPieces()) {
        	p1Pieces.add(p);
        }
        
        for(Animal_Piece p: players[1].getPieces()) {
        	p2Pieces.add(p);
        }
    }
    
    /**
     * Scans the directory for a specific image holding all the pieces' images. It splices and divides the overall image to several
     * <p>
     * pieces and adds them to the array list containing all the images of the pieces.
     */
    public void setPiecesImages() {
    	//Check if file is found
    	try {
			BufferedImage pieceImg = ImageIO.read(GameGui.class.getClassLoader().getResourceAsStream("\\resources\\Game Pieces.png"));
			for(int i = 0; i < pieceImg.getHeight(); i += 181)
				for(int j = 0; j < pieceImg.getWidth(); j += 181)
					//Splices the main image holding the pieces into 181 by 181 squares then re-scales it to a 111 by 100 image
					piecesImages.add(pieceImg.getSubimage(j, i, 181, 181).getScaledInstance(111, 100, BufferedImage.SCALE_SMOOTH));
		} catch (IOException e) {
			e.printStackTrace();
		}
    }
    
    /**
     * After adding the piece images to the array list, it sets the players' pieces image using the given index
     * <p>
     * of the images in the array list. Red pieces have image indexes of 0 to 7, while blue piece image index is
     * <p>
     * from 8 to 15.
     */
    public void setPiecesIcons() {
    	setPiecesImages();
    	
    	for(Animal_Piece p: p1Pieces) {
    		//Index 0 - 7 are images of red pieces mouse to elephant 
    		int index = p.getRank() - 1;
    		if(p.getColor().equalsIgnoreCase("blue"))
    			//Index 8 - 15 are images of blue pieces mouse to elephant
    			index += 8;
    		p.setPieceImage(piecesImages.get(index));
    	}
    	
    	for(Animal_Piece p: p2Pieces) {
    		//Index 0 - 7 are images of red pieces mouse to elephant 
    		int index = p.getRank() - 1;
    		if(p.getColor().equalsIgnoreCase("blue"))
    			//Index 8 - 15 are images of blue pieces mouse to elephant
    			index += 8;
    		p.setPieceImage(piecesImages.get(index));
    	}
    }

    /**Given 2 String colors for player 1 and player 2, initializes the board's tiles, traps and havens.
     * <p>
     * After setting the tiles' positions, set the pieces' positions. 
     * @param p1Color Color to be assigned to pieces of player 1
     * @param p2Color Color to be assigned to pieces of player 2
     */
    public void Init_Board(String p1Color, String p2Color){
    	
        players[0].setPiecesColor(p1Color);
        players[1].setPiecesColor(p2Color);

        setPiecesIcons();

        //Set up Traps and Haven for RED
        boardTiles[2][0] = new Tile(true, false, true, false, "Red");
        boardTiles[3][1] = new Tile(true, false, true, false, "Red");
        boardTiles[4][0] = new Tile(true, false, true, false, "Red");
        boardTiles[3][0] = new Tile(true, false, false, true, "Red");
        
        //Set up Traps and Haven for BLUE
        boardTiles[2][8] = new Tile(true, false, true, false, "Blue");
        boardTiles[3][7] = new Tile(true, false, true, false, "Blue");
        boardTiles[4][8] = new Tile(true, false, true, false, "Blue");
        boardTiles[3][8] = new Tile(true, false, false, true, "Blue");
        
        for(int i = 0; i < HEIGHT; i++){
            for(int j = 0; j < WIDTH; j++){
                //If tile is not yet initialized
                if(boardTiles[i][j] == null){
                    if (j > 2 && j < 6) {
                    	//If upper river
                        if (i > 0 && i < 3)
                            boardTiles[i][j] = new Tile(true, true, false, false, null);
                        //If lower river
                        else if (i > 3 && i < 6)
                            boardTiles[i][j] = new Tile(true, true, false, false, null);
                        else
                            boardTiles[i][j] = new Tile(true, false, false ,false, null);
                    }
                    else
                        boardTiles[i][j] = new Tile(true, false, false ,false, null);
                }
                boardTiles[i][j].setPos(j, i);
            }
        }

        setPiecesPosition(p1Pieces, p1Color);
        setPiecesPosition(p2Pieces, p2Color);
    }

    /**Given a tile and destination position, moves the tile's piece onto the tile pertaining to the destination.
     * <p>
     * If the destination tile has a piece, its removed, and that tile's piece is set to the piece being moved there.
     * <p>
     * If the destination tile is an enemy trap, set the moved piece's rank to the negative of its rank.
     * <p>
     * If the piece's current tile is an enemy trap and is to be moved elsewhere, revert back its rank to positive.
     * <p>
     * The previous tile's piece is set to null as the piece is moved to another tile.
     * <p>
     * Reverts the turn to the opposite color after the move, and checks if the win conditions were fulfilled. 
     * @param tile Tile whose piece is to be moved
     * @param pos Destination of where the piece will be moved to
     * @param turn Holder of the turn, changes the turn after the move
     */
    public void Move(Tile tile, Positions pos, String[] turn){
    	
    	//if tile has a piece
        if(tile.hasPiece()){
        	
        	int x = pos.getX(), y = pos.getY();
			
        	if(boardTiles[y][x].hasPiece()) {
        		for(int i = 0; i < p1Pieces.size(); i++)
            		if(p1Pieces.get(i) == tile.getPiece())
            			p1Pieces.remove(i);
            	
            	for(int i = 0; i < p2Pieces.size(); i++)
            		if(p2Pieces.get(i) == tile.getPiece())
            			p2Pieces.remove(i);
        	}
        	
        	//Set destination tile's piece to current tile's piece
	        boardTiles[y][x].setPiece(tile.getPiece());

            //If destination tile is enemy trap, subvert and lower piece rank
            if(boardTiles[y][x].isTrap()) {
            	if(!boardTiles[y][x].isAllyTile(tile.getPiece()))
            		boardTiles[y][x].getPiece().setRank(boardTiles[y][x].getPiece().getRank() * -1);
            }
            //If previous tile was a trap, revert rank
            if( tile.isTrap() ) {
            	if(!tile.isAllyTile(tile.getPiece()))
            		boardTiles[y][x].getPiece().setRank(boardTiles[y][x].getPiece().getRank() * -1);
            }
            
            //Remove current tile's piece and set tile to empty
	        tile.setPiece(null);
            tile.setEmpty(true);

            //Change turns
            if(turn[0].equalsIgnoreCase("red"))
                turn[0] = "BLUE";
            else
                turn[0] = "RED";
            
            //Check if a winnder is found
            checkWin();
        }
    }

    /**Facilitates the checking of the validity of a move, given the tile holding the piece and intended destination position
     * <p>
     * Diagonal moves are invalid, as well as moving more than 1 tile from current position. 
     * <p>
     * Pieces with lower ranks cannot take other pieces with higher ranks. 
     * <p>
     * Pieces with rank of 1 can move on top of the river unless that piece is on land while another piece is on the river.
     * <p>
     * Pieces with rank of 7 or 6 can jump over river unless there is a piece on top of the river tiles they jump over, and
     * <p>
     * if the tile across the river contains a piece with a higher rank. Pieces cannot take or move unto other pieces and havens
     * <p>
     * having the same color. Pieces on a river cannot take pieces on land but can take pieces on the river. Pieces can move freely
     * <p>
     * on traps whether same or different color unless another piece with a higher rank is on that trap. Last, pieces can move
     * <p>
     * freely onto tiles with no pieces unless previous aforementioned legalities are violated.
     * @param tile Tile object containing the piece to be moved
     * @param pos Position of the destination where the tile will be moved
     * @return Boolean value if the given tile's piece can move into the destination position
     */
    public boolean isValidMove(Tile tile, Positions pos){
        int a = tile.getPos().getX(), b = tile.getPos().getY();
        int x = pos.getX(), y = pos.getY();
        
        //Cannot move more than one block left and right
        if(x > a + 1 || x < a - 1)
            return false;

        //Cannot move more than one block up or down
        else if(y > b + 1 || y < b - 1)
            return false;     

        //If piece moved diagonally
        else if(x != a && y != b)
            return false;
        
        //If current tile is a river and destination tile has a piece
        if(tile.isRiver() && boardTiles[y][x].hasPiece()) {
        	if(boardTiles[y][x].isRiver()){
                if(tile.getPiece().getRank() >= boardTiles[y][x].getPiece().getRank())
                    return true;
                else
                    return false;
            }
        	return false;
        }
        
        //If destination tile is a river
        else if(boardTiles[y][x].isRiver()) {
        	//If current tile's piece is a lion or tiger
        	if(tile.getPiece().getRank() == 7) {
        		if(x == a + 1) {
        			//Move x coordinate destination to across the river
        			x = x + 3;
        			//Scan for mouse on river
        			for (int i = a + 1; i < x; i++) {
						if(boardTiles[y][i].hasPiece())
							return false;
					}
        		}
        		
        		else if(x == a - 1) {
        			//Move x coordinate destination to across the river
        			x = x - 3;
        			//Scan river path for mouse
        			for (int i = a - 1; i > x; i--) {
						if(boardTiles[y][i].hasPiece())
							return false;
					}
        		}
        		
        		else if(y == b + 1) {
        			//Move y coordinate destination to across the river
        			y = y + 2;
        			//Scan river path for mouse
        			for (int i = b + 1; i < y; i++) {
						if(boardTiles[i][x].hasPiece())
							return false;
					}
        		}
        		
        		else if(y == b - 1) {
        			//Move y position destination to across the river
        			y = y - 2;
        			//Scan river path for a mouse
        			for (int i = b - 1; i > y; i--) {
						if(boardTiles[i][x].hasPiece())
							return false;
					}
        		}
        	}
        	//If piece is a mouse or tiger
        	else if(tile.getPiece().getRank() == 1 || tile.getPiece().getRank() == 6) {
        		//If destination tile has a piece
        		if(boardTiles[y][x].hasPiece())
        			return false;
        		return true;
        	}
        	else
				return false;
        }
        
        //If destination tile has a piece and not on the river
        if(boardTiles[y][x].hasPiece()){
        	//Same piece color is invalid
        	if(tile.getPiece().getColor().equalsIgnoreCase(boardTiles[y][x].getPiece().getColor()))
        		return false;
        	
        	//Mouse can capture elephant
        	else if(tile.getPiece().getRank() == 1 && boardTiles[y][x].getPiece().getRank() == 8)
        		return true;
        	
        	//Elephant cannot capture mouse
        	else if(tile.getPiece().getRank() == 8 && boardTiles[y][x].getPiece().getRank() == 1)
        		return false;
        	
        	//Pieces with lower ranking cannot capture pieces with higher ranking
        	else if(tile.getPiece().getRank() < boardTiles[y][x].getPiece().getRank())
        		return false;
        	
        	else {
        		//Set new position to destination
        		pos.setXY(x, y);
        		return true;
        	}
        }
        
        //If destination tile is a trap
        else if(boardTiles[y][x].isTrap()) {
        		pos.setXY(x, y);
        		return true;
        }
        
        //if destination tile is a haven tile
        else if (boardTiles[y][x].isHaven()) {
        	//Invalid if same color as haven
			if(boardTiles[y][x].isAllyTile(tile.getPiece()))
				return false;
			else {
				pos.setXY(x, y);
				//Set win to true
				Win = true;
				return true;
			}
		}

        //If destination is empty
        else if (boardTiles[y][x].isEmpty()) {
        	pos.setXY(x, y);
        	return true;
        }

        return true;
    }

    /**Initializes the pieces on the 2d array of tiles, given an array list of pieces and their color
     * <p>
     * Also sets these tiles holding a piece as not empty
     * @param pieces ArrayList containing the animal pieces of a player
     * @param color String color assigned to the player's pieces
     */
    public void setPiecesPosition(ArrayList<Animal_Piece> pieces, String color){
    	//Sets pieces for red
        if(color.equalsIgnoreCase("red")){
            boardTiles[6][2].setPiece(pieces.get(0));
            boardTiles[6][2].setEmpty(false);
            boardTiles[1][1].setPiece(pieces.get(1));
            boardTiles[1][1].setEmpty(false);
            boardTiles[2][2].setPiece(pieces.get(2));
            boardTiles[2][2].setEmpty(false);
            boardTiles[4][2].setPiece(pieces.get(3));
            boardTiles[4][2].setEmpty(false);
            boardTiles[5][1].setPiece(pieces.get(4));
            boardTiles[5][1].setEmpty(false);
            boardTiles[0][0].setPiece(pieces.get(5));
            boardTiles[0][0].setEmpty(false);
            boardTiles[6][0].setPiece(pieces.get(6));
            boardTiles[6][0].setEmpty(false);
            boardTiles[0][2].setPiece(pieces.get(7));
            boardTiles[0][2].setEmpty(false);
        }
        //Sets pieces for blue
        else{
            boardTiles[0][6].setPiece(pieces.get(0));
            boardTiles[0][6].setEmpty(false);
            boardTiles[5][7].setPiece(pieces.get(1));
            boardTiles[5][7].setEmpty(false);
            boardTiles[4][6].setPiece(pieces.get(2));
            boardTiles[4][6].setEmpty(false);
            boardTiles[2][6].setPiece(pieces.get(3));
            boardTiles[2][6].setEmpty(false);
            boardTiles[1][7].setPiece(pieces.get(4));
            boardTiles[1][7].setEmpty(false);
            boardTiles[6][8].setPiece(pieces.get(5));
            boardTiles[6][8].setEmpty(false);
            boardTiles[0][8].setPiece(pieces.get(6));
            boardTiles[0][8].setEmpty(false);
            boardTiles[6][6].setPiece(pieces.get(7));
            boardTiles[6][6].setEmpty(false);
        }
    }

    /**Given 2 integer values, pertaining to x and y position, return the designated tile found in that position
     * @param x Integer value for x position
     * @param y Integer value for y position
     * @return Tile object in the 2d array of tiles using the y position as row and x position as column
     */
    public Tile getTile(int x, int y){
    	//Try to get board tiles, prints exception otherwise
        try {
			return boardTiles[y][x];
		} catch (Exception e) {
			return null;
		}
    }

    /**Returns the width of the board
     * @return Integer value of the board's width
     */
    public int getWidth(){
        return WIDTH;
    }

    /**Returns the height of the board
     * @return Integer value of the board's height
     */
    public int getHeight(){
        return HEIGHT;
    }
    
    /**Returns Boolean Win attribute, if the game has been won or not.
     * @return Boolean pertaining to win attribute
     */
    public boolean getWin() {
    	return Win;
    }
    
    /**
     * Scans the array of tiles, positions of pieces on the tiles, and counts the number of pieces per side.
     * <p>
     * If one of the counts of the pieces of each side reaches zero, it sets the win boolean to true and exits the method
     */
    public void checkWin() {
    	int redCount = 0, blueCount = 0;
		for(int i = 0; i < 7; i++) {
			for (int j = 0; j < 9; j++) {
				//If tile has a piece
				if(boardTiles[i][j].hasPiece()) {
					//Increments red count
					if(boardTiles[i][j].getPiece().getColor().equalsIgnoreCase("red"))
						redCount++;
					//increments blue count
					else
						blueCount++;
				}
			}
		}
		if(redCount == 0) {
			Win = true;
			return;
		}
		else if(blueCount == 0) {
			Win = true;
			return;
		}
    }
}
