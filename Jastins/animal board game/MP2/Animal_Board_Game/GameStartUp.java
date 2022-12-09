package Animal_Board_Game;
import java.awt.Color;
import java.awt.Font;
import java.awt.Image;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.awt.event.MouseListener;
import java.awt.image.BufferedImage;
import java.io.IOException;
import java.util.concurrent.TimeUnit;

import javax.imageio.ImageIO;
import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JTextField;
import javax.swing.SwingConstants;

/**
 * @author Justin T. Ayuyao
 * <p>
 * This class is the game start up class which at first asks for the two names of
 * <p>
 * the players. The players then begin to choose a card from the given which carry
 * <p>
 * their own respective animal piece image. The player who got a piece with a higher
 * <p>
 * ranking piece gets to choose their color and get the first turn. After this set up,
 * <p>
 * the game board gui is run and the start up is disposed.
 */
@SuppressWarnings("serial")
public class GameStartUp{

	private final int WINDOW_HEIGHT = 800, WINDOW_WIDTH = 900;
	
	private String Player1, Player2;
	private String p1Color, p2Color;
	private int p1Choice, p2Choice;
	
	private JButton submitButton;
	private JTextField player1Input;
	private JTextField player2Input;
	
	private JPanel contentPane;
	private JPanel inputPanel;
	private ChooseCardPanel chooseCardPanel;
	
	private String firstTurn;
	private JFrame mainWindow;
	private GameGui GameWindow;
	
	/**Constructor that initializes the first main window, sets its bounds, as well as its content pane. 
	 * <p>
	 * Window is not resizable. The first window shown features two text fields that take in both Player 1's
	 * <p>
	 * and Player 2's given names and a submit button. After submitting the given names, the panel is hidden
	 * <p>
	 * and another panel, the panel where players choose a card, would be shown. 
	 */
	public GameStartUp() {
		mainWindow = new JFrame();
		Player1 = new String();
		Player2 = new String();
		
		mainWindow.setBounds(250, 10, WINDOW_WIDTH, WINDOW_HEIGHT);
		contentPane = new JPanel();
		contentPane.setLayout(null);
		contentPane.setBackground(Color.BLACK);
		contentPane.setBounds(0, 0, WINDOW_WIDTH, WINDOW_HEIGHT);
		mainWindow.setContentPane(contentPane);
		mainWindow.setResizable(false);
		mainWindow.setVisible(true);
		
		//Header Jlabel for Input panels
		JLabel[] inputPaneLabels = new JLabel[4];
		inputPaneLabels[0] = new JLabel("ENTER NAMES OF PLAYER 1 AND PLAYER 2 THEN PRESS SUBMIT");
		inputPaneLabels[0].setForeground(Color.RED);
		inputPaneLabels[0].setFont(new Font("Century Gothic", Font.PLAIN, 20));
		inputPaneLabels[0].setBounds(50, 0, 600, 50);
		
		//JLabel for text
		inputPaneLabels[1] = new JLabel("PLAYER 1: ");
		inputPaneLabels[1].setFont(new Font("Century Gothic", Font.PLAIN, 20));
		inputPaneLabels[1].setBounds(25, 45, 100, 50);
		
		//JLabel for text
		inputPaneLabels[2] = new JLabel("PLAYER 2: ");
		inputPaneLabels[2].setFont(new Font("Century Gothic", Font.PLAIN, 20));
		inputPaneLabels[2].setBounds(25, 95, 100, 50);
		
		//JLabel for text
		inputPaneLabels[3] = new JLabel("SUBMIT PLAYER NAMES: ");
		inputPaneLabels[3].setFont(new Font("Century Gothic", Font.PLAIN, 20));
		inputPaneLabels[3].setBounds(25, 145, 400, 50);
		
		//INput panel holding all components
		inputPanel = new JPanel();
		inputPanel.setBounds(100, 50, WINDOW_WIDTH - 200, WINDOW_HEIGHT - 600);
		inputPanel.setBackground(new Color(204, 204, 204));
		
		//JTextfield to take in player 1 name
		player1Input = new JTextField();
		player1Input.setFont(new Font("Calibri", Font.PLAIN, 20));
		player1Input.setBounds(150, 50, 460, 40);
		
		//JTextfield to take in player 2 name
		player2Input = new JTextField();
		player2Input.setFont(new Font("Calibri", Font.PLAIN, 20));
		player2Input.setBounds(150, 100, 460, 40);
		
		//Submit button
		submitButton = new JButton("Submit");
		submitButton.setBounds(275, 155, 100, 30);
		
		submitButton.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				if(e.getSource() == submitButton){
					//If player names are initialized
					if(player1Input.getText().length() > 0 && player2Input.getText().length() > 0) {
						try {
							TimeUnit.MILLISECONDS.sleep(50);
						} catch (InterruptedException c) {
							c.printStackTrace();
						}
						Player1 = player1Input.getText();
						Player2 = player2Input.getText();
						
						chooseCardPanel = new ChooseCardPanel();
						
						//Hide text input panel
						inputPanel.setVisible(false);
						contentPane.add(chooseCardPanel);
						refreshPane();
					}
				}
			}
		});
		
		inputPanel.setLayout(null);
		inputPanel.add(submitButton);
		inputPanel.add(player1Input);
		inputPanel.add(player2Input);
		
		for(JLabel p: inputPaneLabels)
			inputPanel.add(p);
		
		contentPane.add(inputPanel);
	}
	

	/**
	 * @author Justin T. Ayuyao
	 * <p>
	 * JPanel that would facilitate the session wherein players choose cards from a given
	 * <p>
	 * set, and decides which player gets the first turn and chooses their color.
	 */
	private class ChooseCardPanel extends JPanel{
		private BufferedImage cards;
		private JLabel[] cardHolder;
		private JLabel headerCardJLabel;
		private JLabel headerColorJLabel;
		private Image[] cardImages;
		private Image cardBGImage;
		private JButton redButton;
		private JButton blueButton;
		
		/**
		 * Inner class inheriting the JPanel. Panel wherein the players are inquired to choose a card from
		 * <p>
		 * the given shown cards. After clicking a card, an animal piece is shown and the player who got a
		 * <p>
		 * piece with a higher rank goes first. The order of the cards and pieces are randomized and shown
		 * <p>
		 * only after the card was clicked. After both players choose a card, the one who got a higher ranked
		 * <p>
		 * piece gets to choose their color as well as get the first turn.
		 */
		public ChooseCardPanel() {
			setBounds(100, 50 ,WINDOW_WIDTH - 200, 500);
			setBackground(Color.BLACK);
			setVisible(true);
			setOpaque(true);
			setLayout(null);
			
			this.cardImages = new Image[8];
			this.cardHolder = new JLabel[8];
			
			p1Choice = 0;
			p2Choice = 0;
			
			//Header label for choosing card panel
			this.headerCardJLabel = new JLabel(Player1 + " CHOOSE A CARD");
			headerCardJLabel.setForeground(Color.RED);
			headerCardJLabel.setBounds(225, 10, 400, 30);
			headerCardJLabel.setFont(new Font("Century Gothic", Font.BOLD, 20));
			
			//Header label for Choosing color panel
			this.headerColorJLabel = new JLabel();
			headerColorJLabel.setBounds(0, 10, 700, 100);
			headerColorJLabel.setForeground(Color.WHITE);
			headerColorJLabel.setHorizontalAlignment(SwingConstants.CENTER);
			headerColorJLabel.setVisible(false);
			
			//Color Buttons
			this.redButton = new JButton("RED");
			this.blueButton = new JButton("BLUE");
			
			try {
				//Get image of card
				cards = ImageIO.read(GameGui.class.getClassLoader().getResourceAsStream("\\resources\\Cards.png"));
				int z = 0;
				for(int i = 0; i < 2295; i += 255) {
					
					if(z < 8)
						cardImages[z] = cards.getSubimage(i, 0, 255, 365).getScaledInstance(140, 200, BufferedImage.SCALE_SMOOTH);
					
					//Back of card image
					if(z == 8)
						cardBGImage = cards.getSubimage(i, 0, 255, 365).getScaledInstance(140, 200, BufferedImage.SCALE_SMOOTH);
					z++;
				}

				//Initialize jlabel for cards
				for(int i = 0; i < cardHolder.length; i++) {
					cardHolder[i] = new JLabel();
					cardHolder[i].setIcon(new ImageIcon(cardBGImage));
				}
				
				//Ranks representing the images in the cards
				int[] ranks = new int[]{1,2,3,4,5,6,7,8};
				
				//Shuffle order of images and ranks
				shuffle(cardImages, ranks, 8);
				
				z = 0;
				for(int i = 50; i < 400; i += 250) {
					for(int j = 28; j < 700; j += 168) {
						cardHolder[z].setBounds(j, i, 140, 200);
						z++;
					}
				}
				
				for(int i = 0; i < 8; i++) {
					cardHolder[i].addMouseListener(new MouseAdapter() {
						@Override
						public void mousePressed(MouseEvent e){
							//If either player 1 and player 2  has not yet made a choice
							if(p1Choice == 0 || p2Choice == 0) {
								for(int i = 0; i < cardHolder.length; i++) {
									//If a jlabel is clicked
									if(e.getSource().equals(cardHolder[i])) {
										//Change back of card image to piece image
										cardHolder[i].setIcon(null);
										cardHolder[i].setIcon(new ImageIcon(cardImages[i]));
										
										try {
											TimeUnit.MILLISECONDS.sleep(50);
										} catch (InterruptedException c) {
											c.printStackTrace();
										}
										
										refreshPane();
										
										//If player 2 has not yet chosen, update header jlabel and remove mouse listener for clicked jlabel
										if(p1Choice == 0) {
											p1Choice = ranks[i];
											headerCardJLabel.setText(Player2 + " CHOOSE A CARD");
											cardHolder[i].removeMouseListener(this);
										}
										
										else if(p2Choice == 0) {
											p2Choice = ranks[i];
										}
									}									
								}
								refreshPane();
							}
						}
						
						@Override
						public void mouseReleased(MouseEvent e) {
							try {
								TimeUnit.MILLISECONDS.sleep(2000);
							} catch (InterruptedException c) {
								c.printStackTrace();
							}
							checkPlayerInputs();
						}
					});
					add(cardHolder[i]);
				}
				
				add(headerCardJLabel);
				add(headerColorJLabel);
				add(redButton);
				add(blueButton);
			} catch (IOException e) {
				e.printStackTrace();
			}
		}
		
		/**Given an array of images as well as an array of integers, this method shuffles both of their
		 * <p>
		 * elements' order. However, the order of elements of both arrays are the same.
		 * @param input Image Array to be shuffled or randomized
		 * @param input2 Integer Array to be shuffled or randomized
		 * @param length Number of elements of both arrays
		 */
		public void shuffle(Image[] input, int[] input2, int length){
	        Image temp;
	        int temp2;
	        int rand;
	        for (int i = 0; i < length; i++) {
	            rand = (int)(Math.random() * (length - 1)); 
	            
	            //Swap images
	            temp = input[rand];
	            input[rand] = input[i];
	            input[i] = temp;
	            
	            //Swap integers
	            temp2 = input2[rand];
	            input2[rand] = input2[i];
	            input2[i] = temp2;
	        }
	    }
		
		/**Sets the text to be displayed by the header JLabel, if player 1's choice is greater than player 2's choice
		 * <p>
		 * then player 1 is inquired to choose their starting color. Likewise, if the opposite were the case, player 2
		 * <p>
		 * would be the one inquired to choose a starting color. It also sets the header JLabel to be visible. 
		 */
		public void setColorHeader() {
			if(p1Choice > p2Choice)
				headerColorJLabel.setText(Player1 + " GOES FIRST, " + Player1 + " PLEASE CHOOSE A COLOR BELOW");
			else
				headerColorJLabel.setText(Player2 + " GOES FIRST, " + Player2 + " PLEASE CHOOSE A COLOR BELOW");
			
			headerColorJLabel.setFont(new Font("Century Gothic", Font.PLAIN, 20));
			headerColorJLabel.setVisible(true);
		}
		
		/**Sets the 2 buttons of choice which decides the starting colors (RED or BLUE).
		 * <p>
		 * The two buttons are set to visible and have their respective action listeners.
		 * <p>
		 * If the red button is pressed, the first turn is now red and the player with a higher choice card
		 * <p>
		 * gets the color red. Likewise if the blue button is pressed, the first turn is blue, and the player
		 * <p>
		 * with a higher choice card gets the color blue.
		 */
		public void setButtons() {
			redButton.setVisible(true);
			redButton.setBounds(100, 100, 100, 50);
			redButton.setBackground(Color.RED);
			redButton.setForeground(Color.WHITE);
			redButton.addActionListener(new ActionListener() {
				@Override
				public void actionPerformed(ActionEvent e) {
					if(e.getSource() == redButton)
						firstTurn = "Red";
					
					//If p1 gets first turn
					if(p1Choice > p2Choice) {
						p1Color = "Red";
						p2Color = "Blue";
					}
					
					// if p2 gets first turn
					else {
						p2Color = "Red";
						p1Color = "Blue";
					}

					//Creates the gameboard gui and disposes current JFrame
					GameWindow = new GameGui(Player1, Player2, p1Color, p2Color, firstTurn);
					GameWindow.setVisible(true);
					mainWindow.dispose();
				}
			});
			
			blueButton.setBounds(500, 100, 100, 50);
			blueButton.setBackground(Color.BLUE);
			blueButton.setForeground(Color.WHITE);
			blueButton.addActionListener(new ActionListener() {
				@Override
				public void actionPerformed(ActionEvent e) {
					if(e.getSource() == blueButton)
						firstTurn = "Blue";
					
					//If p1 gets first turn
					if(p1Choice > p2Choice) {
						p1Color = "Blue";
						p2Color = "Red";
					}
					
					//If p2 gets first turn
					else {
						p2Color = "Blue";
						p1Color = "Red";
					}
					
					//Creates the gameboard gui and disposes current JFrame
					GameWindow = new GameGui(Player1, Player2, p1Color, p2Color, firstTurn);
					GameWindow.setVisible(true);
					mainWindow.dispose();
				}
			});
		}
		
		/**
		 * Scans through all JLabels found in the JLabel array list, then if the list of mouse listeners
		 * <p>
		 * in the JLabel is not null, it removes all mouse listeners found in the list.
		 */
		public void removeMouseListeners() {
			for(JLabel p: cardHolder) {
				//If mouse listeners are present
				if(p.getMouseListeners() != null)
					for(MouseListener q: p.getMouseListeners()) {
						p.removeMouseListener(q);
					}
			}
		}
		
		/**
		 * Checks the player inputs, if both players have already chosen a card, all mouse listeners are removed.
		 * <p>
		 * After that, all JLabels in the panel are hidden, then the panel is hidden as well. Header JLabel is also
		 * 
		 */
		public void checkPlayerInputs(){
			//If player 1 and player 2 made a choice
			if(p1Choice != 0 && p2Choice != 0) {
				//remove all mouse listeners
				removeMouseListeners();
				
				//hide cards
				for(JLabel p: cardHolder)
					p.setVisible(false);
				
				//hide header label
				headerCardJLabel.setVisible(false);
				
				setColorHeader();
				setButtons();
				refreshPane();
			}
		}
	}
	
	/**
	 * Invalidates the given window, then validates it. After that, it repaints all the elements within the main window.
	 */
	public void refreshPane() {
		mainWindow.invalidate();
		mainWindow.validate();
		mainWindow.repaint();
	}
	
	/**Get String name of Player 1
	 * @return String name of Player 1
	 */
	public String getP1Name() {
		return Player1;
	}
	
	/**Get String name of Player 2
	 * @return String name of Player 2
	 */
	public String getP2Name() {
		return Player2;
	}
	
	/**Get String color of Player 1
	 * @return String color of Player 1
	 */
	public String getP1Color() {
		return p1Color;
	}
	
	/**Get String color of Player 2
	 * @return String color of Player 2
	 */
	public String getP2Color() {
		return p1Color;
	}
	
	/**Get the String pertaining to the color getting the first turn
	 * @return String of color who has the first turn
	 */
	public String getFirstTurn() {
		return firstTurn;
	}
}
