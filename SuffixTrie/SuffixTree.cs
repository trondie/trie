using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuffixTrie
{
    enum Match { PERFECT_MATCH, PARTIAL_MATCH, CONTINUE_MATCHING,  CREATE_INTERNAL, NO_MATCH, INCREMENT , CREATE_LEAF};

    class SuffixTree
    {
        Node root;
        Node currentNode;
        String text;

        int globalIndex;

        public SuffixTree(string text)
        {
            this.text = text;
            this.globalIndex = 0;
            buildTree();
        }
        private char getCharAt(int index)
        {
            return text.ElementAt(index);
        }
        private bool charEqual(int index1, int index2)
        {
            return text[index1] == text[index2];
        }
        private Node findFirstCharacterMatch(int index)
        {
            for (int i = 0; i < currentNode.children.Count; ++i)
            {
                if (getCharAt(index) == getCharAt(currentNode.children[i].getLabelFrom()))
                {
                    return currentNode.children[i];
                }
            }
            return null;
        }
        private Node findFirstCharacterMatchByChar(char c)
        {
            for (int i = 0; i < currentNode.children.Count; ++i)
            {
                if (c == getCharAt(currentNode.children[i].getLabelFrom()))
                {
                    return currentNode.children[i];
                }
            }
            return null;
        }
        private void addNewLeafNode(int labelFrom, int labelTo)
        {
            Node node = new Node(currentNode, labelFrom, labelTo, true);
            currentNode.addChild(node);
        }
        
        private int findParentChildIndex()
        {
            for (int i = 0; i < currentNode.getParent().children.Count; ++i)
            {
                if (currentNode.Equals(currentNode.getParent().children[i]))
                {
                    return i;
                }
            }
            throw new Exception("Tree is broken! Trying to find broken reference to child");
        }
        // "internalTo" index is the character position at internalNode's labelTo.
        private Node createInternalNode(int internalToIndex, int i)
        {
            Node internalNode = new Node(currentNode.getParent(), currentNode.getLabelFrom(), internalToIndex, false);
            //Make parent point to internal node instead of current node
            currentNode.getParent().children[findParentChildIndex()] = internalNode;
            currentNode.setParent(internalNode);
            //Change label for this node (toIndex remains the same)
            currentNode.setLabelFrom(internalToIndex + 1);
            //Add new child node from splitpoint with label T[i:i]
            Node newChildNode = new Node(internalNode, i, i, true);
            internalNode.addChild(newChildNode);
            internalNode.addChild(currentNode);
           
            //Return the interal node (set current node elsewhere)
            return internalNode;
        }
        private int findSplitPoint(int tempj, int i)
        {
            int edgeDelta = currentNode.getLabelTo() - currentNode.getLabelFrom();
            string edge = text.Substring(currentNode.getLabelFrom(), edgeDelta + 1);
            string suffixString = text.Substring(tempj, (i - tempj) + 1);

            int iterLength = (edge.Length < suffixString.Length) ? edge.Length : suffixString.Length;
            for (int it = 0; it < iterLength; ++it)
            {
                if (edge[it] != suffixString[it])
                {
                    return currentNode.getLabelFrom() + it - 1;
                }
            }
            throw new Exception("Tree is broken!");
        }
        public string longestExactMatch(string pattern) {

            string result = "";

            currentNode = root;
            int patternIndex = 0;
            char tempChar = pattern[patternIndex];
            string tempResult = "";
            while (true)
            {
                //Find first character to match
                Node firstChild = findFirstCharacterMatchByChar(tempChar);

                if (firstChild == null)
                {
                    return result;
                }

                currentNode = firstChild;
                bool currentTerminal = checkForTerminalChild();
                int edgeLength = currentNode.getEdgeLength();
                for (int i = 0; i < edgeLength; ++i)
                {
                    if (patternIndex == pattern.Length)
                    {
                        if (text[currentNode.getLabelFrom() + i] == '$')
                        {
                            result += tempResult;
                            result += getText(currentNode.getLabelFrom(), currentNode.getLabelTo() - 1);
                            return result;
                        }
                        return string.Empty;
                    }
                    if (pattern[patternIndex++] != text[currentNode.getLabelFrom() + i])
                    {
                        if (text[currentNode.getLabelFrom() + i] == '$')
                        {
                            result += tempResult;
                            result += getText(currentNode.getLabelFrom(), currentNode.getLabelTo() - 1);
                            
                        }
                        return result;
                    }
                    else if (text[currentNode.getLabelFrom() + i] == '$')
                    {
                        break;
                    }
                }

                tempResult += getText(currentNode.getLabelFrom(), currentNode.getLabelTo());
                if (checkForTerminalChild())
                {
                    result += tempResult;
                    tempResult = "";
                }
                


                patternIndex = (patternIndex < pattern.Length) ? patternIndex : patternIndex - 1;
                tempChar = pattern[patternIndex];
            }
        }
        public bool mismatchPass(string str1, double percentage)
        {
            return false;
        }
        public string getPathToHere(Node terminalNode)
        {
            string result = "";
            Node tempNode = terminalNode;
            while (tempNode.getParent() != null)
            {
                int terminalOffset = tempNode.getIsTerminal() ? 0 : 1;
                string edge = text.Substring(currentNode.getLabelFrom(), currentNode.getEdgeLength() - terminalOffset);             
                edge = Reverse(edge);
                result += edge;
                tempNode = tempNode.getParent();
            }
            return Reverse(result);
        }
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        public string longestApproximateMatch(string pattern, double percentage)
        {
            string bestResult = "";
            Stack<Node> nodes = new Stack<Node>();
            
            nodes.Push(currentNode);

            // Strategy : DFS. If terminal node ? Gather strings comprising path upto root, and match it! 
            while (nodes.Count != 0)
            {
                currentNode = nodes.Pop();       
                
                if (currentNode.getIsTerminal())
                {
                    string tempResult = getPathToHere(currentNode);
                    if (tempResult.Length > bestResult.Length)
                    {
                        bestResult = tempResult;
                    }
                }      
                for (int i = 0; i < currentNode.children.Count; ++i)
                {
                    nodes.Push(currentNode.children[i]);
                }

            }
            return "";
        }
        public bool currentNodeTerminalOnly()
        {
            if ((currentNode.getLabelFrom() == currentNode.getLabelTo()) && (currentNode.getLabelTo() == text.Length - 1))
            {
                return true;
            }
            return false;
        }
        public bool checkForTerminalChild()
        {
            foreach (Node c in currentNode.children)
            {
                if (getCharAt(c.getLabelFrom()) == '$')
                {
                    return true;
                }
            }
            return false;
        }
        public string getText(int j, int i)
        {
            return text.Substring(j, i - j + 1);
        }
        
        public Match matchEdgeEval(int j, int i, ref int splitPoint)
        {
            int edgeDelta = currentNode.getLabelTo() - currentNode.getLabelFrom();
            string edge = text.Substring(currentNode.getLabelFrom(), edgeDelta + 1);
            string suffixString = text.Substring(j, (i - j) + 1);

            bool match = false;
            if (edge.Length == suffixString.Length)
            {
                for (int ei = 0; ei < edge.Length; ++ei)
                {
                    // Note that the first char have already been matched on this edge
                    if (edge[ei] != suffixString[ei])
                    {
                        if (ei == 0) throw new Exception("Bad index!");
                        splitPoint = currentNode.getLabelFrom() + ei - 1;
                        return Match.CREATE_INTERNAL;
                    }
                }
                // Rule 1
                return Match.PERFECT_MATCH;         
            }
            else if (edge.Length < suffixString.Length) 
            {
                // This edge has previously been matched. If there's a mismatch at this point, something has scrambled the edges
                // We simply want to move to correct child node, and therefore need to add the "next j" of the suffixString 
                splitPoint = j + edge.Length;
                return Match.CONTINUE_MATCHING;
            }
            if (edge.Length > suffixString.Length)
            {
                for (int si = 0; si < suffixString.Length; ++si)
                {
                    if (edge[si] != suffixString[si])
                    {
                        splitPoint = currentNode.getLabelFrom() + si - 1;
                        return Match.CREATE_INTERNAL;
                    }
                }
                return Match.PARTIAL_MATCH;
            }
            throw new Exception("Broken tree! No matching rules found!");
        }
        private void buildTree()
        {
            //I1
            root = new Node(null, -1, -1, false);
            Node child = new Node(root, 0, 0, true);
            root.addChild(child);

            for (int i = 1; i < text.Length; ++i)
            {
                globalIndex = i;
                currentNode = root;
                for (int j = 0; j <= i; ++j)
                {
                    
                    Node firstMatch = findFirstCharacterMatch(j);

                    if (firstMatch == null)
                    {
                        addNewLeafNode(j, i);
                        continue;
                    }

                    //Move down to child node to consider the label
                    currentNode = firstMatch;

                    //Used by matchEdge function to locate a splitpoint if we need to create an internal ndoe
                    int splitPoint = 0;
                    bool doBreak = false;

                    Match eval = matchEdgeEval(j, i, ref splitPoint);
                    switch (eval)
                    {
                        // Rule 1
                        case Match.INCREMENT:
                            // TODO : Make global
                            currentNode.setLabelTo(currentNode.getLabelTo() + 1);
                            // We are done extending current leaf node with i + 1. We can therefore go to root, and move on to next j.
                            currentNode = root;
                            break;
                        // Rule 2, case 1
                        case Match.CREATE_LEAF:
                            currentNode.addChild(new Node(currentNode, i, i, true));
                            // We have extended with a leaf, and are done for this j. 
                            currentNode = root;
                            break;
                        // Rule 2, case 2
                        case Match.CREATE_INTERNAL:
                            currentNode = createInternalNode(splitPoint, i);
                            // We have created an internal node, and extended with i + 1. We are done, and want to go to next j.
                            currentNode = root;
                            break;
                        // Rule 3
                        case Match.PARTIAL_MATCH:
                            // We have matched j:i, and can go to next j. 
                            currentNode = root;
                            break;
                        // Rule 3
                        case Match.PERFECT_MATCH:
                            // We have matched j:i, and can go to next j. 
                            currentNode = root;
                            break;
                        // Traversal
                        case Match.CONTINUE_MATCHING:
                            int tempj = j;
                            bool loop = true;
                            while (loop)
                            {
                                // We are not done, and want to continue. Move to the next node. We use splitpoint in this case also. 
                                Node nextChild = findFirstCharacterMatch(splitPoint);
                                // If the there are no matches from this node, this implies that j == i - 1.
                                if (nextChild == null)
                                {
                                    // if (currentNode.getEdgeLength() != (i - j)) throw new Exception("Bad index when continuing matching!");
                                    if (currentNode.getIsTerminal()) goto case Match.INCREMENT;
                                    goto case Match.CREATE_LEAF;
                                }

                                // We have a match, and want to go to the next child to continue with further matching
                                currentNode = nextChild;
                                tempj = splitPoint;
                                Match tempMatch = matchEdgeEval(tempj, i, ref splitPoint);

                                if (tempMatch == Match.CONTINUE_MATCHING) continue;
                                if (tempMatch == Match.INCREMENT)
                                {
                                    loop = false;
                                    goto case Match.INCREMENT;
                                }
                                if (tempMatch == Match.PARTIAL_MATCH)
                                {
                                    loop = false;
                                    goto case Match.PARTIAL_MATCH;
                                }
                                else if (tempMatch == Match.CREATE_INTERNAL)
                                {
                                    loop = false;
                                    goto case Match.CREATE_INTERNAL;
                                }
                                else if (tempMatch == Match.PERFECT_MATCH)
                                {
                                    loop = false;
                                    goto case Match.PERFECT_MATCH;
                                }
                                else if (tempMatch == Match.CREATE_LEAF)
                                {
                                    loop = false;
                                    goto case Match.CREATE_LEAF;
                                }
                                else
                                {
                                    throw new Exception("What?");
                                }
                            }
                            // Match tempMatch = matchEdgeEval(tempj, i, ref splitPoint);
                            break;               
                    }                
                }
            }
        }       
    }
}
