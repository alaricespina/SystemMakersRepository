import java.util.*;
import java.io.*;

public class Main {
	
	
    public static void main(String[] args){

        ArrayList<String> wordList;
        try {
        	
            File file1 = new File("Input.txt");
            File file2 = new File("output.txt");
            
            FileWriter fw = new FileWriter(file2);

            PrintWriter pw = new PrintWriter(fw);

            GetWords wordScanner = new GetWords(file1);

            BinarySearchTree BTS = new BinarySearchTree();

            for (int i = 0; i < wordScanner.words.size(); i++) {
                BTS.Insert(new Node(wordScanner.words.get(i)), BTS.root);
            }

            BTS.Inorder(BTS.root);

            pw.println("======================");

            for (int i = 0; i < BTS.InOrderWords.size(); i++) {
                System.out.format("%-20s %s\n", BTS.InOrderWords.get(i), BTS.wordCount.get(i));
                pw.format("%-20s %s\n", BTS.InOrderWords.get(i), BTS.wordCount.get(i));

            }
            pw.println("======================");
            pw.close();
            
        } catch (Exception e) {
            System.out.println("Input File not Found");
        }
    }
}
