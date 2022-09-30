package checkers;

import java.util.ArrayList;

public class MoveNode {
	int value;
	int x, y;
	MoveNode parent;
	ArrayList<MoveNode> children;
	
	public MoveNode(int Value, MoveNode Parent) {
		this.parent = Parent;
		this.value = Value;
		children = new ArrayList<>();
	}
	
	public void insertNode(MoveNode node) {
		this.children.add(node);
	}
	
	public void displayNodes() {
		System.out.println(this.value);
		if(this.children.size() > 0)
			for(MoveNode n: this.children)
				n.displayNodes();
	}
	
	public static void main(String[] args) {
		System.out.println("Test");
		MoveNode testMoveNode = new MoveNode(0, null);
		testMoveNode.insertNode(new MoveNode(1, testMoveNode));
		testMoveNode.children.get(0).insertNode(new MoveNode(2, testMoveNode.children.get(0)));
		testMoveNode.children.get(0).children.get(0).parent.parent.displayNodes();
	}
}
