package checkers;

import java.util.ArrayList;

public class MoveOrdering {

	
	public static void flipMoveOrder(ArrayList<Board> b) {
		//Flips the order of the list
		for(int i = 0; i < (b.size()/2); i++) 
		{
			Board tempBoard = b.get(i);
			Board tempBoard2 = b.get(b.size()-1-i);
			b.set(i, tempBoard2);
			b.set(b.size()-1-i, tempBoard);
		}
	}
	
	public static void bestFirstOrder(ArrayList<Board> b, Constants c) {
		//Sorts the list in increasing order then flips it
		sortList(b, 0, b.size()-1, c);
		flipMoveOrder(b);
	}
	
	public static void mergeList(ArrayList<Board> b, int l, int m, int r, Constants c) {
		ArrayList<Board> result = new ArrayList<>();
		
		int i = l;
		int j = m + 1;
		
		while(i <= m && j <= r) {
			if(b.get(i).evaluatePos(c, b.get(i)) <= b.get(j).evaluatePos(c, b.get(j)))
				result.add(b.get(i++));
			else
				result.add(b.get(j++));
		}
		
		while(i <= m)
			result.add(b.get(i++));
		
		while(j <= r)
			result.add(b.get(j++));
		
		i = 0; j = l;
		while(i < result.size()) {
			b.set(j, result.get(i++));
			j++;
		}
	}
	
	public static void sortList(ArrayList<Board> points, int l, int r, Constants c) {
		if(l<r && (r-l)>=1) {
			int mid = (r + l) / 2;
			sortList(points, l, mid, c);
			sortList(points, mid + 1, r, c);
			mergeList(points, l, mid, r, c);
		}
	}
	
	
	
}
