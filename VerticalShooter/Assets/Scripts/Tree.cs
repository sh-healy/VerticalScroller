using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree
{
    /// <summary>
    /// root of the tree, first node added
    /// </summary>
    private TreeNode root;

    /// <summary>
    /// the last node inserted into the tree
    /// </summary>
    private TreeNode last;

    /// <summary>
    /// Amount of objects added to tree
    /// </summary>
    private int count;

    /// <summary>
    /// List of all the nodes in the tree
    /// </summary>
    public TreeNode[] nodesToCheck;

    #region pubic methods

    /// <summary>
    /// Constructor
    /// </summary>
    public Tree()
    {
    }

    /// <summary>
    /// Creates a new node with a reference to a game object and adds it to the tree
    /// </summary>
    /// <param name="newObj">new object to add to tree</param>
    public void Add(GameObject newObj)
    {
        TreeNode newNode = new TreeNode(newObj);
        AddToTree(newNode);
    }

    /// <summary>
    /// Gets the closest object to a point
    /// </summary>
    /// <param name="position">the point to find the closest object to</param>
    /// <returns>The GO which is the closest to that point</returns>
    public GameObject GetClosest(Vector2 position)
    {
        TreeNode closestNode = FindClosest(position);
        if (closestNode != null)
            return closestNode.obj;
        else
            return null;
    }

    /// <summary>
    /// Removes object from the tree
    /// </summary>
    /// <param name="obj">object to remove from tree</param>
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
        
    }

    /// <summary>
    /// Clears tree of all objects
    /// </summary>
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

        //increase count of nodes
        count++;

        //get the parent of the node
        TreeNode parent = GetParent(addedNode.obj.transform.position);

        //no root yet
        if (parent == null)
        {
            root = addedNode;
            last = addedNode;
            return;
        }

        
        last.next = addedNode;
        last = addedNode;
        
        addedNode.level = parent.level + 1;

        //check if comparing the x or y based on level
        bool isXAligned = IsXAligned(parent.level);

        float xyOfParentNode = isXAligned ? parent.obj.transform.position.x : parent.obj.transform.position.y;
        float xyOfNewNode = isXAligned ? addedNode.obj.transform.position.x : addedNode.obj.transform.position.y;

        //if x or y is less than parent node add to the left else right
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
            //check if comparing the x or y based on level
            bool isXAligned = IsXAligned(current.level);

            float xyOfCurrNode = isXAligned? current.obj.transform.position.x : current.obj.transform.position.y;
            float xyOfNewNode = isXAligned ? position.x : position.y;

            parent = current;

            //if x or y is less than parent node move to the left else move to the right
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

        //check if nodesToCheck has been initilized and if the length is the same as the amount of nodes we've added
        if (nodesToCheck == null || nodesToCheck.Length < count)
            nodesToCheck = new TreeNode[count];

        //reset tree
        for (int i = 0; i < nodesToCheck.Length; i++)
        {
            nodesToCheck[i] = null;
        }

        //nodes we've added to check
        int addCount = 0;

        //nodes we're checking in the array
        int currNodeToCheck = 0;

        if (root != null)
            nodesToCheck[addCount++] = root;

        TreeNode current;

        
        //while inside array of nodes AND the node in the array of nodes is null/empty
        while (currNodeToCheck < nodesToCheck.Length && nodesToCheck[currNodeToCheck] != null)
        {
            //check the next node in the iist
            current = nodesToCheck[currNodeToCheck++];

            float distanceBetween = Vector2.Distance(position, current.obj.transform.position);

            //if the distance is less change nearest
            if (distanceBetween < nearestDist)
            {
                nearestDist = distanceBetween;
                nearest = current;
            }

            //check if comparing the x or y based on level
            bool isXAligned = IsXAligned(current.level);

            float xyOfCurrNode = isXAligned ? current.obj.transform.position.x : current.obj.transform.position.y;
            float xyOfSearchNode = isXAligned ? position.x : position.y;

            //if x or y of position being checked is less than than the x/y of current node
            if (xyOfSearchNode < xyOfCurrNode)
            {
                //add left node to nodes to check
                if (current.left != null)
                    nodesToCheck[addCount++] = current.left;
                //add right node to nodes to check
                if (Mathf.Abs(xyOfCurrNode - xyOfSearchNode) * Mathf.Abs(xyOfCurrNode - xyOfSearchNode) < nearestDist && current.right != null)
                    nodesToCheck[addCount++] = current.right;
            }
            else
            {
                //add right node to nodes to check
                if (current.right != null)
                    nodesToCheck[addCount++] = current.right;
                //add left node to nodes to check
                if (Mathf.Abs(xyOfCurrNode - xyOfSearchNode) * Mathf.Abs(xyOfCurrNode - xyOfSearchNode) < nearestDist && current.left != null)
                    nodesToCheck[addCount++] = current.left; 
            }
            

        }

        return nearest;

    }

    /// <summary>
    /// checks if we should be compaing the level of the tree is even therefore x aligned to compare to
    /// </summary>
    /// <param name="lvl">level were checking against, determines whether its x aligned or y aligned</param>
    /// <returns>returns true is level is x aligned</returns>
    private bool IsXAligned(int lvl)
    {
        bool isXAligned = lvl % 2 == 0;
        return isXAligned;
    }

    /// <summary>
    /// Gets list of nodes currently added to the tree
    /// </summary>
    /// <returns> list of nodes </returns>
    private List<TreeNode> GetList()
    {
        List<TreeNode> nodesAdded = new List<TreeNode>();
        TreeNode current = root;
        while (current!=null)
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
    /// <summary>
    /// GameObject well be using the position of to insert into tree
    /// </summary>
    public GameObject obj;

    /// <summary>
    /// level of the tree it's on
    /// </summary>
    public int level;

    /// <summary>
    /// left subtree of node
    /// </summary>
    public TreeNode left;

    /// <summary>
    /// right subtree of node
    /// </summary>
    public TreeNode right;

    /// <summary>
    /// next node in list
    /// </summary>
    public TreeNode next;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="newObj"></param>
    public TreeNode(GameObject newObj)
    {
        obj = newObj;
    }
}


