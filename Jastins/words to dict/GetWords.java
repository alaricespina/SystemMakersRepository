import java.util.*;
import java.io.*;

public class GetWords {

    ArrayList<String> words;
    File scanFile;
    public GetWords(File file) throws FileNotFoundException{
    	scanFile = file;
    	
        Scanner sc = new Scanner(scanFile);
        String line;

        this.words = new ArrayList<>();
        
        while (sc.hasNextLine()) {
        	
            line = sc.nextLine();
            int len = line.length();
            String temp = "";

            if(!(line.equalsIgnoreCase("\n"))){
                for (int i = 0; i < len; i++) {

                    char ch = line.charAt(i);

                    if ((((int) ch) > 64 && ((int) ch) < 91) || (((int) ch) > 96 && ((int) ch) < 123)) {
                        temp = temp + ch;
                    }

                    else if(ch == '-' || ch == '\''){
                        temp = temp + ch;
                    }

                    else if (ch == ' ') {
                        if (temp.length() >= 3) {
                            words.add(temp);
                            temp = "";
                        } else
                            temp = "";
                    }

                    if (i == len - 1) {
                        if (temp.length() >= 3) {
                            words.add(temp);
                            temp = "";
                        } else
                            temp = "";
                    }
                }
                
            }
        }
        sc.close();
    }
}