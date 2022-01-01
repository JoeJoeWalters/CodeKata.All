using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Katas
{
    /*
    Problem:

    Reconstruct Binary Tree from Preorder array
    */

    #region Solution

    public class Node<T>
    {
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }
        public T Data { get; set; }
    }

    public static class BinaryTree
    {
        public static Node<T> Generate<T>(T[] source)
        {
            return new Node<T>();
        }
    }
    #endregion

    #region Tests
    public class Tests
    {
        [Fact]
        public void Generate_BinaryTree_From_Preorder()
        {
            // ARRANGE
            string[] preorder = new string[] { "a", "b", "d", "e", "c", "f", "g" };
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
            Node<string> result = BinaryTree.Generate<string>(preorder);

            // ASSERT
            result.Should().BeEquivalentTo(expected);
        }
    }
    #endregion
}
