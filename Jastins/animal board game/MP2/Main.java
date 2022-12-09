import java.awt.EventQueue;

import Animal_Board_Game.GameStartUp;


public class Main {

		public static void main(String[] args) {
			EventQueue.invokeLater(new Runnable() {
			public void run() {
				try {
					GameStartUp startUp = new GameStartUp();
					
				} catch (Exception e) {
					e.printStackTrace();
				}
			}
		});
	}
}
