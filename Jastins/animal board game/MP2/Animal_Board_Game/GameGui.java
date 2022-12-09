package Animal_Board_Game;

import java.awt.BorderLayout;
import java.awt.Font;
import java.awt.Graphics;

import javax.imageio.ImageIO;
import javax.swing.JFrame;
import javax.swing.JPanel;
import javax.swing.border.Border;

import java.awt.Color;
import java.awt.Component;
import java.awt.Image;
import java.awt.event.*;
import java.awt.image.BufferedImage;
import java.io.IOException;
import java.awt.GridLayout;
import javax.swing.JLabel;
import javax.swing.BorderFactory;
import javax.swing.ImageIcon;

/**
 * @author Justin T. Ayuyao
 * <p>
 * The class which implements the visual implementation or representation of the
 * <p>
 * game board and where the players interact or control their respective pieces.
 */
@SuppressWarnings("serial")
public class GameGui extends JFrame{
	
	private final int WINDOW_WIDTH, WINDOW_HEIGHT;
	
	private Tile sourceTile;
	
	private JPanel contentPane;
	private JPanel titlePanel;
	private boardPanel GamePanel;
	
	private JLabel titleJLabel;
	private JLabel[][] jLabels;
	private JLabel[][] selected;
	
	private tilePanel[][] tiles;
	
	private GameBoard gameboard;
	private String[] turn = new String[1];
	
	/** Constructor to build and initialize the graphical user interface of the board.
	 * @param p1Name String name for player 1
	 * @param p2Name String name for player 2
	 * @param p1Color String color of the pieces of player 1
	 * @param p2Color String color of the pieces of player 2
	 * @param currentTurn String color of the color with the first turn
	 */
	public GameGui(String p1Name, String p2Name, String p1Color, String p2Color, String currentTurn) {
		this.WINDOW_WIDTH = 1014;
		this.WINDOW_HEIGHT = 785;
		
		this.turn[0] = currentTurn;
		
		this.gameboard = new GameBoard(p1Name, p2Name);
		gameboard.Init_Board(p1Color, p2Color);

		jLabels = new JLabel[gameboard.getHeight()][gameboard.getWidth()];
		selected = new JLabel[gameboard.getHeight()][gameboard.getWidth()];
		
		//Initialize settings of main window
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		setBounds(250, 10, WINDOW_WIDTH, WINDOW_HEIGHT);
		setResizable(false);
		contentPane = new JPanel();
		contentPane.setSize(1000, 750);
		setContentPane(contentPane);
		contentPane.setLayout(null);
		
		//Turn Panel
		this.titlePanel = new JPanel();
		titlePanel.setLayout(new BorderLayout());
		titlePanel.setBackground(Color.BLACK);
		titlePanel.setBounds(0, 0, contentPane.getWidth(), 50);
		
		//JLabel of title panel
		titleJLabel = new JLabel();
		titleJLabel.setForeground(Color.WHITE);
		titleJLabel.setText(turn[0] + "'s TURN");
		titleJLabel.setFont(new Font("Verdana", Font.PLAIN, 18));
		titleJLabel.setHorizontalAlignment(JLabel.CENTER);
		titlePanel.add(titleJLabel, BorderLayout.CENTER);
		
		//Board Panel
		this.GamePanel = new boardPanel(contentPane.getWidth(), contentPane.getHeight() - titlePanel.getHeight());

		//Tile Panels
		tiles = new tilePanel[gameboard.getHeight()][gameboard.getWidth()];
		InitImageIcon(tiles, jLabels);

		for(int i = 0; i < 7; i++) {
			for(int j = 0; j < 9; j++) {
				GamePanel.add(tiles[i][j]);
				GamePanel.validate();
			}
		}
		
		contentPane.add(titlePanel);
		contentPane.add(GamePanel);
	}

	 /**Background panel where the game board will be drawn on and where the tilePanels
	 * <p>
	 * will be laid out on.
	 */
	private class boardPanel extends JPanel{
		
		private Image background;
		/**Constructor for game board image panel
		 * @param width Integer width assigned to the panel's width size
		 * @param height Integer height assigned to the panel's height size
		 */
		public boardPanel(int width, int height) {
			try {
				this.background = ImageIO.read(GameGui.class.getClassLoader().getResourceAsStream("\\resources\\GameBoard.jpg"));
				background = background.getScaledInstance(999, 700, BufferedImage.SCALE_SMOOTH);
			} catch (IOException e) {
				e.printStackTrace();
			}
			
			setBounds(0, 50, width, height);
			//Layout for the piece panels to be laid out on
			GridLayout gbl_panel_1 = new GridLayout(7, 9, 0, 0);
			
			setLayout(gbl_panel_1);
			setOpaque(true);
			validate();
		}
		@Override
		protected void paintComponent(Graphics g) {
			super.paintComponent(g);
			g.drawImage(background, 0, 0, this);
		}
	}
	
	/**
	 * Inner class inside the GameGui that extends the JPanel, called the tilePanel. This panel is where the several
	 * <p> 
	 * JLabels accommodating the pieces' images will be held in.
	 */
	private class tilePanel extends JPanel{
		
		private int x, y;
		private boolean isSelected;
		
		/**Constructor of the tile panel.
		 * @param X Integer x to be assigned to the panel's x position
		 * @param Y Integer y to be assigned to the panel's y positon
		 */
		public tilePanel(int X, int Y) {
			this.x = X;
			this.y = Y;
			
			setLayout(new BorderLayout());

			setOpaque(false);
			
			addMouseListener(new MouseAdapter() {
				public void mousePressed(MouseEvent c) {
					
					//If right click, deselect any selected tiles
					if(c.getButton() == MouseEvent.BUTTON3){
						if(sourceTile != null) {
							deselect();
						}
						sourceTile = null;
					}
					
					//If left click
					else if(c.getButton() == MouseEvent.BUTTON1) {
						//While no winner
						if(!gameboard.getWin()) {
							//If no tile is selected yet
							if(sourceTile == null) {
								sourceTile = gameboard.getTile(x, y);
								//If source tile has a piece, select the tile
								if(sourceTile.hasPiece()) {
									if(sourceTile.getPiece().getColor().equalsIgnoreCase(turn[0])) {
										isSelected = true;
										select(tiles[sourceTile.getPos().getY()][sourceTile.getPos().getX()]);
									}	
									else
										sourceTile = null;
								}
								else {
									sourceTile = null;
								}
							}
							else if(sourceTile != null) {
								//IF source of the click is not the sourceTile's tilePanel
								if( (tilePanel)c.getSource() != tiles[sourceTile.getPos().getY()][sourceTile.getPos().getX()]) {
									//If swapping the tile image was not successful, deselect current tile
									if(!swapTileImage(tiles[sourceTile.getPos().getY()][sourceTile.getPos().getX()], (tilePanel) c.getSource(), sourceTile));
										deselect();
									sourceTile = null;
								}
								else 
									deselect();
							}
						}	
					}
				}
			});
		}
		
		/**Gets the integer x position of the panel
		 * @return Integer x position of panel
		 */
		public int getXPos() {
			return x;
		}
		
		/**Gets the integer y position of the panel 
		 * @return Integer y position of panel
		 */
		public int getYPos() {
			return y;
		}
		
		/**Sets the select boolean attribute of the panel to the given boolean parameter
		 * @param s Boolean to be assigned to the select attribute
		 */
		public void setSelect(boolean s) {
			this.isSelected = s;
		}
		
		/**Gets the select boolean of the panel
		 * @return Boolean select attribute
		 */
		public boolean isSelected() {
			return isSelected;
		}
	
		/**Sets the select boolean of the panel to true. Then, it adds a border on the panel
		 * <p>
		 * with the same color as the piece on that panel and refreshes the pane.
		 * @param panel Panel that will be selected
		 */
		public void select(tilePanel panel) {
			Border border;
			panel.setSelect(true);
			
			if(sourceTile.getPiece().getColor().equalsIgnoreCase("red"))
				border = BorderFactory.createLineBorder(Color.RED, 5);
			else
				border = BorderFactory.createLineBorder(Color.BLUE, 5);
			
			//add border to jlabel then add jlabel to panel
			selected[y][x].setBorder(border);
			panel.add(selected[y][x]);
			refreshPane();
		}
		
		/**
		 * Scans the 2d array of panels and if the panel is selected, it scans its components and
		 * <p>
		 * if the component found was the border, it removes the border, deselects the panel and 
		 * <p>
		 * revalidates the panel
		 */
		public void deselect() {
			for(int i = 0; i < gameboard.getHeight(); i++) {
				for (int j = 0; j < gameboard.getWidth(); j++) {
					//If tile was selected
					if(tiles[i][j].isSelected()) {
						//scan panel components
						for(Component c: tiles[i][j].getComponents()) {
							//If component is a jlabel containing the border, remove it then revalidate
							if(c.equals(selected[i][j])) {
								tiles[i][j].remove(c);
								tiles[i][j].revalidate();
							}
						}
						//set selected to false
						tiles[i][j].setSelect(false);
					}
				}
			}
			sourceTile = null;
			refreshPane();
		}
	}
	
	/**Initializes the 2d JPanel array as well as the 2d JLabel array given. If the aligned tile to the
	 * <p>
	 * panel has no piece, the panel is created and a JLabel with no image is added to the panel. If the 
	 * <p>
	 * tile aligned to the panel has a piece, the panel is created and a JLabel with an icon image of the
	 * <p>
	 * piece is added to the panel.
	 * @param panels 2d array of panels who holds the jlabels
	 * @param labels 2d array of jlabels holding piece images and is aligned with the 2d array of panels
	 */
	public void InitImageIcon(tilePanel[][] panels, JLabel[][] labels) {
		Tile temp;
		for(int i = 0; i < 7; i++) {
			for(int j = 0; j < 9; j++) {

				temp = gameboard.getTile(j, i);

				//If tile has no piece, add blank jlabel to panel
				if(!(temp.hasPiece())) {
					panels[i][j] = new tilePanel(j, i);
					labels[i][j] = new JLabel("");
					labels[i][j].setOpaque(false);
					panels[i][j].add(labels[i][j]);
					panels[i][j].validate();
				}
				//If tile has piece, get set jlabel image to piece's image then add to panel
				else{
					panels[i][j] = new tilePanel(j, i);
					labels[i][j] = new JLabel("");
					labels[i][j].setIcon(new ImageIcon(temp.getPiece().getPieceImage()));
					labels[i][j].setOpaque(false);
					panels[i][j].add(labels[i][j]);
					panels[i][j].validate();
				}
				//border jlabel is created
				selected[i][j] = new JLabel();
			}
		}
	}

	/**Swaps the image icons between two JLables of two given panels and moves the piece located
	 * <p>
	 * in the first panel's position to its wanted destination. If a win was detected in the
	 * <p>
	 * game board, the title panel is updated to the color that won the game. 
	 * @param panel1 Source panel where the piece was originally displayed in
	 * @param panel2 Destination panel which the mouse clicked for the piece to move to
	 * @param sourceTile Tile object containing the piece
	 * @return Boolean if the swapping of tiles was successful
	 */
	public boolean swapTileImage(tilePanel panel1, tilePanel panel2, Tile sourceTile) {
		int x = panel1.getXPos(), y = panel1.getYPos();

		//create temporary positions object to pass in isValidMove method
		Positions tempPos = new Positions(panel2.getXPos(), panel2.getYPos());

		if(sourceTile.hasPiece()) {
			
			//If temporary destination position is valid
			if(gameboard.isValidMove(sourceTile, tempPos)) {
				
				//Move piece to destination
				gameboard.Move(sourceTile, tempPos, turn);

				int a = tempPos.getX(), b = tempPos.getY();
				
				//Remove destination img, replace with sourceTile image, then remove previous position img
				if(jLabels[b][a].getIcon() != null)
					jLabels[b][a].setIcon(null);
				
				//Set icon of destination to source tile
				jLabels[b][a].setIcon(jLabels[y][x].getIcon());
				jLabels[y][x].setIcon(null);
				
				//Revalidate jlabels
				jLabels[b][a].revalidate();
				jLabels[y][x].revalidate();
				
				//remove jlabels in first panel and destination panel
				panel1.removeAll();
				tiles[b][a].removeAll();

				//add new jlabels whose icons were swapped
				panel1.add(jLabels[y][x]);
				tiles[b][a].add(jLabels[b][a]);

				refreshPane();
				
				//If a winner was found
				if(gameboard.getWin()) {
					if(turn[0].equalsIgnoreCase("red"))
						updateTitlePanel("BLUE" + " WINS");
					else
						updateTitlePanel("RED" + " WINS");
				}
				else
					updateTitlePanel(turn[0] + "'s TURN");
				return true;
			}
			else
				return false;
		}
		return false;
	}
	
	/**Scans through the components of the title panel and if its a JLabel, it sets the text of
	 * <p>
	 * that JLabel to the string parameter given.
	 * @param s String to be assigned to the JLabel found in the Title Panel
	 */
	public void updateTitlePanel(String s) {
		//Scans components in title panel
		for(Component c : titlePanel.getComponents()) {
			//If component is a JLabel, set text
			if(c instanceof JLabel){
				((JLabel)c).setText(s);
				((JLabel)c).setFont(new Font("Verdana", Font.PLAIN, 18));
			}
		}
	}
	
	/**First invalidates the content pane of the frame, then validates it after. After that, it repaints the components
	 * <p>
	 * of the content pane.
	 */
	public void refreshPane() {
		contentPane.invalidate();
		contentPane.validate();
		contentPane.repaint();
	}
}

