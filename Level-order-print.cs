using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Level_Order_Print
{
    class Node
    {
        static public readonly Char END = '!';
        public Node left;
        public Node right;
        public Char value;
        public Node ()
        {
            left = right = null;
            value = '?';
        }
        public Node(Char v)
        {
            left = right = null;
            value = v;
        }

        public Node( Node l, Node r, char v='?')
        {
            left = l;
            right = r;
            value = v;
        }
    }

    


    class Program
    {
        /*
         *   Build another struc like this out of a given tree 
         *    Levels |0| ->   ListOfNodes 
         *           |1| ->   List of Nodes
         *           |2| ->  List of nodees at level 2 
         *           
         */
        static void LevelOrderPrintTree(Node root)
        {
            if ( root == null)
            {
                return;
            }

            ArrayList Levels = new ArrayList();   // element = ArrayList of nodes in one level
            ArrayList thisLevelNodes = new ArrayList();
            int level = 0;
            thisLevelNodes.Add(root);
            Levels.Add(thisLevelNodes);

            while (true)
            {
                ArrayList nodes = new ArrayList();
                ArrayList al = Levels[level] as ArrayList;
                foreach (Node n in al)
                {
                    if (n.left != null)
                    {
                        nodes.Add(n.left);
                    }
                    if (n.right != null)
                    {
                        nodes.Add(n.right);
                    }
                }
                if (nodes.Count > 0)
                {
                    Levels.Add(  nodes) ;
                    level++;
                }
                else
                {
                    break;
                }
            }


            // Print it
            for(int i = 0; i <= level; i++ )        // NOTE:  <= missed at 1st attempt
            {
                Console.Write("Level = {0} ==> ", i);
                ArrayList al = Levels[i] as ArrayList;
                foreach (Node n in al)
                {
                    Console.Write("{0} ", n.value);
                }
                Console.WriteLine();
            }

        }

        static Node BuildABinaryTree() 
        {
            Node top_root, l, r, root;
            root = new Node('A');
            top_root = root;

            l = new Node('B');
            r = new Node('C');
            root.left = l;
            root.right = r;

            l.left = new Node('D');
            l.right = new Node('E');  

            r.left = new Node('F');
            r.right = new Node('G');


            root = l.left;  // D
            root.left = new Node('H');

            root = l.right; // E
            root.left = new Node('J');

            root = root.left;  // J
            root.left = new Node('K');


            root = r.left;  // F
            root.right = new Node('I');

            return top_root;


        }

        //
        // ALT WAY USING QUEUE and MARKER NODE
        //

        static Node GetMarkerNode()
        {
            Node n = new Node(Node.END);
            return n;
        }

        static bool IsMarker( Node node )
        {
            return node.value == Node.END;
        }

        static void LevelOrderPrintUsingMarkerNode( Node root)
        {

            Queue<Node> q = new Queue<Node>();
            q.Enqueue(root);
            q.Enqueue(GetMarkerNode());

            while ( q.Count > 0 )
            {
                Node n = q.Dequeue();

                if( IsMarker(n))
                {
                    Console.WriteLine();
                    if (q.Count > 0)        // Missed at 1st attempt
                    {
                        q.Enqueue(GetMarkerNode());
                    }
                    continue;
                }

                Console.Write("{0} ", n.value);


                if ( n.left != null  )
                {
                    q.Enqueue(n.left);
                }


                if ( n.right != null )
                {
                    q.Enqueue(n.right);
                }


            }


        }

        static void Main(string[] args)
        {
            Node root = BuildABinaryTree();

            Console.WriteLine("Using Extra Strcut.");
            LevelOrderPrintTree(root);

            Console.WriteLine("Using Marker");

            LevelOrderPrintUsingMarkerNode(root);

            Console.ReadKey();

        }
    }
}
