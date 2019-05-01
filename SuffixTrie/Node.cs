using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuffixTrie
{
    class Node
    {
        Node parent;
        public List<Node> children;
        bool isTerminal;
        Tuple<int, int> label;

        Node suffixLink;
        public Node(Node parent, int labelFrom, int labelTo, bool isTerminal)
        {
            this.parent = parent;
            label = new Tuple<int, int>(labelFrom, labelTo);
            this.isTerminal = isTerminal;
            this.children = new List<Node>();

        }
        
        public Tuple<int,int> getLabel() { return this.label;  }
       
        public int getLabelFrom() {
            return this.label.Item1;
        }
        public void setLabelFrom(int labelFrom)
        {
            label = new Tuple<int, int>(labelFrom, label.Item2);
        }
        public int getLabelTo()
        {
            //if (isTerminal) return Constants.globalIndex;
            return this.label.Item2;
        }
        public void setLabelTo(int labelTo) { 
            label = new Tuple<int, int>(label.Item1, labelTo);
        }
        public int getEdgeLength()
        {
            return (getLabelTo() - getLabelFrom()) + 1;
        }
        public Node getParent()
        {
            return this.parent;
        }
        public void setParent(Node parent)
        {
            this.parent = parent;
        }
        public void addChild(Node child)
        {
            children.Add(child);
        }
        public Node getChild(int index)
        {
            return children.ElementAt(index);
        }
        public void setIsTerminal(bool isTerminal)
        {
            this.isTerminal = isTerminal;
        }
        public bool getIsTerminal()
        {
            return this.isTerminal;
        }
        public void setSuffixLink(Node suffixLink)
        {
            this.suffixLink = suffixLink;
        }
        
        
    }
}
