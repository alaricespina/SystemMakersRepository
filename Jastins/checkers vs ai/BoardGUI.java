package checkers;

import java.awt.*;
import java.awt.Color.*;
import java.awt.event.*;
import java.io.File;
import java.io.IOException;
import java.util.ArrayList;

import javax.imageio.ImageIO;
import javax.swing.*;

public class BoardGUI extends JFrame{

	final int boardLen = 8;
	final int panelSize = 100;
	ArrayList<ImageIcon> imageIcons;
	JLabel squaresJP[][] = new JLabel[boardLen][boardLen];
	JPanel contentPanel;
	Board board;
	
	public BoardGUI() {
		imageIcons = new ArrayList<>();
		getImages(imageIcons);
		
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		setBounds(250, 10, 1000, 780);
		setResizable(false);
		
		contentPanel = new JPanel();
		contentPanel.setLayout(new GridLayout(8,8));
		
		this.board = new Board();
		
		this.board.printSquares();
			
		for(int i = 0; i < boardLen; i++) {
			for(int j = 0; j < boardLen; j++) {
				squaresJP[i][j] = new JLabel();
				squaresJP[i][j].setOpaque(true);
				squaresJP[i][j].setBorder(BorderFactory.createLineBorder(Color.BLACK));
				squaresJP[i][j].setPreferredSize(new Dimension(100,100));
				
				if(board.squares[i][j].color == Constants.BLACK)
					squaresJP[i][j].setBackground(new Color(142, 105, 40));
				else
					squaresJP[i][j].setBackground(new Color(218, 195, 155));

				if(board.squares[i][j].getPiece() != null) {
					if(board.squares[i][j].getPiece().color == Constants.RED)
						squaresJP[i][j].setIcon(imageIcons.get(0));
					else
						squaresJP[i][j].setIcon(imageIcons.get(1));
				}
				
				contentPanel.add(squaresJP[i][j]);
			}	
		}
		
		contentPanel.setVisible(true);
		setContentPane(contentPanel);
		validate();
		pack();
		setVisible(true);
	}
	
	public void getImages(ArrayList<ImageIcon> icons){
		try {
			icons.add(new ImageIcon(ImageIO.read(new File("redPiece.png")).getScaledInstance(100, 100,  java.awt.Image.SCALE_SMOOTH)));
			icons.add(new ImageIcon(ImageIO.read(new File("blackPiece.png")).getScaledInstance(100, 100,  java.awt.Image.SCALE_SMOOTH)));
			icons.add(new ImageIcon(ImageIO.read(new File("redKing.png")).getScaledInstance(100, 100,  java.awt.Image.SCALE_SMOOTH)));
			icons.add(new ImageIcon(ImageIO.read(new File("blackKing.png")).getScaledInstance(100, 100,  java.awt.Image.SCALE_SMOOTH)));
		} catch (IOException e) {
			e.printStackTrace();
		}
	}
	
	public void refresh() {
		this.repaint();
		this.update(getGraphics());
		this.revalidate();
	}
	
}
