package Animal_Board_Game;


//package test2;

/**
 * @author Justin T. Ayuyao
 * <p>
 * Class pertaining to a specific position on a 2d plane. This class contains both
 * <p>
 * an integer value for the x position and y position. 
 */
public class Positions {

    private int PosX, PosY;
    /**Constructs a Position class given a set of two integer values, and
     * assigns the x and y position integers to those two values
     * @param x Integer x position
     * @param y Integer y position
     */
    public Positions(int x, int y) {
        this.PosX = x;
        this.PosY = y;
    }

    /**Given two integer values, sets class's x value to the first and y value to the second integer
     * @param x Integer x position to be assigned
     * @param y Integer y position to be assigned
     */
    public void setXY(int x, int y) {
        this.PosX = x;
        this.PosY = y;
    }

    /**Return integer value pertaining to x position
     * @return Integer X position attribute of class
     */
    public int getX() {
        return PosX;
    }

    /**Return integer value pertaining to y position
     * @return Integer Y position attribute of class
     */
    public int getY() {
        return PosY;
    }
}
