using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Katas.ReconstructBinaryTree
{
    /*
    Problem:

    Reconstruct Binary Tree from Preorder or Inorder array

    Preorder -> If assuming from an existing tree, reconstruction can be done 
    by splitting the remaining nodes in to two, taking the first element and doing the 
    same with the following nodes etc. etc.
    */

    #region Solution

    public class Node<T>
    {
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }
        public T Data { get; set; }
    }

    public enum BinaryTreePopulationType
    {
        FromUnordered,
        FromPreorder,
        FromInorder
    }

    public class BinaryTree<T>
    {
        public Node<T> Generate(T[] source, BinaryTreePopulationType populationType)
        {
            // Because C# 8.0 not 9.0
            Stack<T> ordered =
                (populationType == BinaryTreePopulationType.FromUnordered) ?
                    new Stack<T>(source.OrderByDescending(x => x)) :
                    new Stack<T>(source);

            if (populationType == BinaryTreePopulationType.FromUnordered)
            {
                Node<T> result = new Node<T>() { Data = ordered.Pop() };

                Stuff(result, ordered);

                return result;
            }

            return null;
        }

        private void Stuff(Node<T> node, Stack<T> ordered)
        {
            if (node.Left == null && ordered.Count != 0)
                node.Left = new Node<T>() { Data = ordered.Pop() };

            if (node.Right == null && ordered.Count != 0)
                node.Right = new Node<T>() { Data = ordered.Pop() };

            if (node.Left != null)
                Stuff(node.Left, ordered);

            if (node.Right != null)
                Stuff(node.Right, ordered);
        }
    }
    #endregion

    #region Tests
    public class Tests
    {
        [Fact]
        public void Generate_BinaryTree_From_PreOrder()
        {
            // ARRANGE
            string[] order = new string[] { "a", "b", "d", "e", "c", "f", "g" };
            Node<string> expected = new Node<string>()
            {
                Data = "a",
                Left = new Node<string>()
                {
                    Data = "b",
                    Left = new Node<string>()
                    {
                        Data = "d"
                    },
                    Right = new Node<string>()
                    {
                        Data = "e"
                    }
                },
                Right = new Node<string>()
                {
                    Data = "c",
                    Left = new Node<string>()
                    {
                        Data = "f"
                    },
                    Right = new Node<string>()
                    {
                        Data = "g"
                    }
                }
            };

            // ACT
            Node<string> result = (new BinaryTree<string>()).Generate(order, BinaryTreePopulationType.FromUnordered);

            // ASSERT
            result.Should().BeEquivalentTo(expected);
        }
    }
    #endregion
}
