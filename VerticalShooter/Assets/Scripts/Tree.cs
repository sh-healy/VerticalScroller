using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree
{

    public TreeNode root { get; private set; }
    public TreeNode last { get; private set; }
    public int count;
    public TreeNode[] nodesInTree;

    #region pubic methods
    public Tree()
    {
    }

    public void Add(GameObject newObj)
    {
        TreeNode newNode = new TreeNode(newObj);
        AddToTree(newNode);
    }

    public GameObject GetClosest(Vector2 position)
    {
        TreeNode closestNode = FindClosest(position);
        if (closestNode != null)
            return closestNode.obj;
        else
            return null;
    }

    public void RemoveObj(GameObject obj)
    {
        List<TreeNode> treeNodes = GetList();

        //find a match to the object
        for(int i =0; i<treeNodes.Count;i++)
        {
            if (treeNodes[i].obj == obj)
                treeNodes.RemoveAt(i);
        }

        //clear the tree
        EmptyTree();

        //repopulate tree
        for (int i = 0; i < treeNodes.Count; i++)
        {
            AddToTree(treeNodes[i]);
        }

        Debug.Log("removed object");
    }

    public void EmptyTree()
    {
        root = null;
        last = null;
        count = 0;
    }

    #endregion pubic methods

    #region private methods

    /// <summary>
    /// Adds a node to the tree
    /// </summary>
    /// <param name="addedNode">the new node to add</param>
    private void AddToTree(TreeNode addedNode)
    {
        addedNode.left = null;
        addedNode.right = null;
        addedNode.level = 0;

        count++;

        TreeNode parent = GetParent(addedNode.obj.transform.position);

        if (parent == null)
        {
            root = addedNode;
            last = addedNode;
            return;
        }

        if (last != null)
            last.next = addedNode;
        else
            last = addedNode;
        
        addedNode.level = parent.level + 1;
        
        float xyOfParentNode = CheckXOrY(parent.level, parent.obj.transform.position);
        float xyOfNewNode = CheckXOrY(parent.level, addedNode.obj.transform.position);

        if (xyOfNewNode < xyOfParentNode)
            parent.left = addedNode;
        else
            parent.right = addedNode;
        
    }

    /// <summary>
    /// Gets the parent node of the position being added
    /// </summary>
    /// <param name="position">position of the new node being added</param>
    /// <returns>parent node</returns>
    private TreeNode GetParent(Vector2 position)
    {
        TreeNode current = root;
        TreeNode parent = root;

        while (current != null)
        {
            float xyOfCurrNode = CheckXOrY(current.level, current.obj.transform.position);
            float xyOfNewNode = CheckXOrY(current.level, position);

            parent = current;
            if (xyOfNewNode < xyOfCurrNode)
                current = current.left;
            else
                current = current.right;
        }

        return parent;
    }

    /// <summary>
    /// Searches through tree to find the closest node to that position
    /// </summary>
    /// <param name="position">position of the new node being added</param>
    /// <returns>node with closest point</returns>
    private TreeNode FindClosest(Vector2 position)
    {
        //check if empty
        if (root == null)
            return null;

        float nearestDist = float.MaxValue;
        TreeNode nearest = null;

        //check if nodesInTree has been initilized and if the length is the same as the amount of nodes we've added
        if (nodesInTree == null || nodesInTree.Length < count)
            nodesInTree = new TreeNode[count];

        //reset tree
        for (int i = 0; i < nodesInTree.Length; i++)
        {
            nodesInTree[i] = null;
        }

        //nodes we've added to the list
        int addCount = 0;

        //index were at in the array
        int currentNodesInTree = 0;

        if (root != null)
            nodesInTree[addCount++] = root;

        TreeNode current;

        
        //while inside array of nodes AND the node in the array of nodes is null/empty
        while (currentNodesInTree < nodesInTree.Length && nodesInTree[currentNodesInTree] != null)
        {
            current = nodesInTree[currentNodesInTree++];

            float distanceBetween = Vector2.Distance(position, current.obj.transform.position);

            //if the distance is less change nearest
            if (distanceBetween < nearestDist)
            {
                nearestDist = distanceBetween;
                nearest = current;
            }

            //check against either x or y value
            float xyOfCurrNode = CheckXOrY(current.level, current.obj.transform.position);
            float xyOfSearchNode = CheckXOrY(current.level, position);


            if (xyOfSearchNode < xyOfCurrNode)
            {
                //go left
                if (current.left != null)
                    nodesInTree[addCount++] = current.left;
                //go right
                if (Mathf.Abs(xyOfCurrNode - xyOfSearchNode) * Mathf.Abs(xyOfCurrNode - xyOfSearchNode) < nearestDist && current.right != null)
                    nodesInTree[addCount++] = current.right;
            }
            else
            {
                //go right
                if (current.right != null)
                    nodesInTree[addCount++] = current.right; 
                //go left
                if (Mathf.Abs(xyOfCurrNode - xyOfSearchNode) * Mathf.Abs(xyOfCurrNode - xyOfSearchNode) < nearestDist && current.left != null)
                    nodesInTree[addCount++] = current.left; 
            }
            

        }

        return nearest;

    }

    /// <summary>
    /// checks if we should be compaing the x or y value of the point on the tree based on the branch
    /// </summary>
    /// <param name="lvl">level were checking against, determines whether its x aligned or y aligned</param>
    /// <param name="position">the position in the tree were checking against or inserting</param>
    /// <returns>returns x value or y value based on the level of the tree</returns>
    private float CheckXOrY(int lvl, Vector2 position)
    {
        float val = lvl % 2 == 0 ? position.x : position.y;
        return val;
    }

    /// <summary>
    /// Gets list of nodes currently added to the tree
    /// </summary>
    /// <returns> list of nodes </returns>
    private List<TreeNode> GetList()
    {
        List<TreeNode> nodesAdded = new List<TreeNode>();
        TreeNode current = root;
        while (current.next!=null)
        {
            nodesAdded.Add(current);
            current = current.next;
        }

        return nodesAdded;
    }

    #endregion private methods
}

public class TreeNode
{
    public GameObject obj;
    public int level;
    public TreeNode left;
    public TreeNode right;
    public TreeNode next;

    public TreeNode(GameObject newObj)
    {
        obj = newObj;
    }
}


