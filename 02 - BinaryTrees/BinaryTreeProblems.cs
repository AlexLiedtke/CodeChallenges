using System;

//The Node class, used to set up the binary trees
public class Node
{
    public Node LeftNode { get; set; }
    public Node RightNode { get; set; }
    public int Data { get; set; }
}

public class BinaryTreeProblems {

    public static void Main() {
        //Setup:  Make the following Binary Tree
        //                0
        //            /       \
        //          1           2
        //        /   \       /   \
        //      3       4   5       6
        //    /   \
        //  7       8        
        Node[] arr = new Node[9];
        for (int i = 0; i <= 8; i++) 
        {
            Node temp = new Node();
            temp.Data = i;
            arr[i] = temp;
        }
        
        arr[0].LeftNode = arr[1];
        arr[0].RightNode = arr[2];
        arr[1].LeftNode = arr[3];
        arr[1].RightNode = arr[4];
        arr[2].LeftNode = arr[5];
        arr[2].RightNode = arr[6];
        arr[3].LeftNode = arr[7];
        arr[3].RightNode = arr[8];
        
        
        //PROBLEM #1:
        //  Write a function where you pass it a node and it returns the sum of the depths of the nodes beneath it.
        //  In the setup tree this would mean nodes 5 and 6 each have a depth of 1 relative to node 2, so DepthSum(Node 2) = 2 which is 1 + 1.
        //  A more complicated example would be DepthSum(Node 1) = 6, since nodes 3 and 4 are depth 1 each, and nodes 7 and 8 are depth 2 each.
        Console.WriteLine("PROBLEM #1, test #1 (Answer should be 16): " + DepthSum(arr[0]));  //Should be 16
        Console.WriteLine("PROBLEM #1, test #2 (Answer should be 2): " + DepthSum(arr[3]));  //Should be 2
        Console.WriteLine("PROBLEM #1, test #3 (Answer should be 6): " + DepthSum(arr[1]));  //Should be 6
        Console.WriteLine("PROBLEM #1, test #3 (Answer should be 0): " + DepthSum(arr[5]));  //Should be 0
        
        //Problem #2:
        //  Building on Problem #1, write a function that returns the sum of the DepthSums of all nodes in the tree.
        //  In the setup tree this would mean AllDepthSums(Node 1) = 8, since DepthSum(Node 3) = 2, DepthSum(1) = 6, and all other DepthSums are 0.
        Console.WriteLine("PROBLEM #2, test #1 (Answer should be 26): " + AllDepthSums(arr[0]));  //Should be 26
        Console.WriteLine("PROBLEM #2, test #2 (Answer should be 8): " + AllDepthSums(arr[1]));  //Should be 8
        
        //Problem #3:
        //  Continuing with the theme, write a function where you pass it a node within a binary tree, and its head node.  Returns the sum of the distances 
        //  from all other nodes in the tree.  In the setup tree this would mean DistanceSum(Node Head, Node 0) = 16 still, but if you called 
        //  DistanceSum(Node Head, Node 1) the answer would be 15, since Node 0 is a distance of 1 from Node 1, Node 2 is distance 2, and Nodes 5 and 6 are distance 3
        Console.WriteLine("PROBLEM #3, test #1 (Answer should be 16): " + DistanceSum(arr[0], arr[0]));  //Should be 16
        Console.WriteLine("PROBLEM #3, test #2 (Answer should be 15): " + DistanceSum(arr[0], arr[1]));  //Should be 15
        Console.WriteLine("PROBLEM #3, test #3 (Answer should be 22): " + DistanceSum(arr[0], arr[4]));  //Should be 22
        Console.WriteLine("PROBLEM #3, test #4 (Answer should be 25): " + DistanceSum(arr[0], arr[7]));  //Should be 25
    }
    
    //ANSWER #1:
    public static int DepthSum(Node head) {
        return DepthSum(head, 0);
    }
    
    public static int DepthSum(Node currentNode, int currentDepth) {
        int tempVal = currentDepth;
        if (currentNode.LeftNode != null) { 
            tempVal += DepthSum(currentNode.LeftNode, currentDepth + 1);
        }
        if (currentNode.RightNode != null) { 
            tempVal += DepthSum(currentNode.RightNode, currentDepth + 1);
        }
        return tempVal;
    }
    
    
    //ANSWER #2:
    public static int AllDepthSums (Node currentNode) {
        int tempVal = DepthSum(currentNode);
        if (currentNode.LeftNode != null) { 
            tempVal += AllDepthSums(currentNode.LeftNode);
        }
        if (currentNode.RightNode != null) { 
            tempVal += AllDepthSums(currentNode.RightNode);
        }
        return tempVal;
    }
    
    
    //ANSWER #3:
    public static int DistanceSum(Node currentNode, Node targetNode) {
        return DistanceSumFinder(currentNode, targetNode).RunningTotal;
    }
    
    public static BranchInfo DistanceSumFinder(Node currentNode, Node targetNode){
        BranchInfo tempInfo = new BranchInfo();
        if (currentNode == targetNode) {
            tempInfo.RunningTotal = DepthSum(currentNode);
        }
        else if (currentNode.LeftNode == null && currentNode.RightNode == null) {
            tempInfo.NumberOfNodes = 1;
            tempInfo.RunningTotal = 1;
        }
        else {
            BranchInfo leftInfo = new BranchInfo();
            BranchInfo rightInfo = new BranchInfo();
            
            if (currentNode.LeftNode != null) { leftInfo = DistanceSumFinder(currentNode.LeftNode, targetNode);    }
            if (currentNode.RightNode != null) { rightInfo = DistanceSumFinder(currentNode.RightNode, targetNode);    }
            
            //node not null but NumberOfNodes valued at 0 implies the targetNode was found in that branch
            if (leftInfo.NumberOfNodes == 0) {
                tempInfo.NumberOfNodes = 0;
                tempInfo.CurrentDistance = leftInfo.CurrentDistance + 1;
                tempInfo.RunningTotal = leftInfo.RunningTotal + tempInfo.CurrentDistance + (rightInfo.NumberOfNodes * tempInfo.CurrentDistance) + rightInfo.RunningTotal;
            }
            else if (rightInfo.NumberOfNodes == 0) {
                tempInfo.NumberOfNodes = 0;
                tempInfo.CurrentDistance = rightInfo.CurrentDistance + 1;
                tempInfo.RunningTotal = rightInfo.RunningTotal + tempInfo.CurrentDistance + (leftInfo.NumberOfNodes * tempInfo.CurrentDistance) + leftInfo.RunningTotal;
            }
            //this block happens when the targetNode isn't anywhere under this node
            else {
                tempInfo.NumberOfNodes = 1 + leftInfo.NumberOfNodes + rightInfo.NumberOfNodes;
                tempInfo.RunningTotal = leftInfo.RunningTotal + rightInfo.RunningTotal + 1 + (leftInfo.NumberOfNodes + rightInfo.NumberOfNodes) ;
            }
            
        }
        
        return tempInfo;
    }
    
}

//The BranchInfo class, used to help with PROBLEM #3
public class BranchInfo
{
    public int NumberOfNodes { get; set; } = 0;
    public int CurrentDistance { get; set; } = 0;
    public int RunningTotal { get; set; } = 0;
}
