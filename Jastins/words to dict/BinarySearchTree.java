import java.util.*;

public class BinarySearchTree {

    Node root;
    
    ArrayList<String> InOrderWords = new ArrayList<>();
    ArrayList<Integer> wordCount = new ArrayList<>();

    public BinarySearchTree(){
        Create();
    }

    public void Create(){
        this.root = null;
    }

    public void Insert(Node node, Node parent){
        if(root == null){
            this.root = node;
            return;
        }
        else{
            // If node is alphabetically found before root input, insert to left (A < B)
            if(node.key.compareToIgnoreCase(parent.key) < 0){
                //If left child is empty, assign to right child
                if(parent.left == null){
                    parent.left = node;
                }
                //Else, insert to left
                else
                    Insert(node, parent.left);
            }

            // If node is alphabetically found before root input, insert to left (B > A)
            else if (node.key.compareToIgnoreCase(parent.key) > 0) {
                // If right child is empty, assign to right child
                if (parent.right == null){
                    parent.right = node;
                }
                // Else, insert to left
                else
                    Insert(node, parent.right);
            }

            // If node has the same key as root input, increment count
            else if (node.key.equalsIgnoreCase(parent.key)) {
                parent.incrementCount();
                return;
            }
        }
    }

    public boolean Search(String k, Node parent){
        //If there is no root node, return false
        if(this.root == null)
            return false;
        else{
            //If word is found to be lesser (a < b) search left child
            if(k.compareToIgnoreCase(parent.key) < 0){
                if(parent.left != null)
                    Search(k, parent.left);
                //If left child is empty, return false
                else
                    return false;
            }

            // If word is found to be greater (b > a) search right child
            else if(k.compareToIgnoreCase(parent.key) > 0){
                if(parent.right != null)
                    Search(k, parent.right);
                //If right child is empty, return false
                else
                    return false;
            }

            // If word was found to have the same order and letters, return true
            else if(k.compareToIgnoreCase(parent.key) == 0){
                return true;
            }
        }
        return false;
    }

    public void Inorder(Node parent){
        
        // If node input is null, break out of loop
        if (parent == null) {
            return;
        }
        
        // Recurse through the left childs
        Inorder(parent.left);

        // Adds all the words to the word list after recursing the left, then adds the parent key before moving onto the right
        InOrderWords.add(parent.key);
        wordCount.add(parent.count);

        // Recurses through the right childs
        Inorder(parent.right);
    }

    public void Destroy(){
        this.root = null;
        this.wordCount = null;
        this.InOrderWords = null;
        System.gc();
    }
}