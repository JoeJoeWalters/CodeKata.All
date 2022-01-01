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

            // ACT

            // ASSERT
        }
    }
    #endregion
}
