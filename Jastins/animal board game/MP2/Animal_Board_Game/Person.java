package Animal_Board_Game;

//package test2;

import java.util.ArrayList;

/**
 * @author Justin T. Ayuyao
 * <p>
 * Person class who owns their own respective pieces. They also have their own name,
 * <p>
 * and their pieces can be assigned a respective color depending on the board game.
 */
public class Person {
    
    private ArrayList<Animal_Piece> Pieces = new ArrayList<>();
    private String pieceColor; 
    private String name;

    /** Constructor to construct the person, given a string parameter pertaining the person's name
     * <p>
     * Also initializes the list of pieces the person has, from mouse to elephant.
     * @param n String to be assigned to the person
     */
    public Person(String n){
    	
        this.name = n;
        
        String[] pieceList = {"Mouse", "Cat", "Wolf", "Dog", "Leopard", "Tiger", "Lion", "Elephant"};
        
        for(int i = 0; i < pieceList.length; i++){
            Pieces.add(new Animal_Piece(i+1, pieceList[i]));
        }   
    }

    /**Given a string pertaining to color, assigns the given color to the pieces owned by the player
     * @param c String color to be assigned to the pieces
     */
    public void setPiecesColor(String c){
        this.pieceColor = c;
        for(Animal_Piece t: Pieces){
            t.setColor(pieceColor);
        }
    }
    
    /**Gets the string name of the person
     * @return String name of the person
     */
    public String getName(){
        return name;
    }

    /**Gets the pieces owned by the person
     * @return ArrayList of animal pieces owned by the person
     */
    public ArrayList<Animal_Piece> getPieces(){
        return Pieces;
    }
}
