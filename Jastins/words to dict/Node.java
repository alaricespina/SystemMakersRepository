public class Node {
    String key;
    Node left;
    Node right;
    int count;

    public Node(String k) {
        this.key = k;
        this.count = 1;
        this.left = null;
        this.right = null;
    }

    public void incrementCount(){
        count = count + 1;
    }
}