using AwesomeAssertions;
using System;
using Xunit;

namespace Katas.ContainerMostWater
{
    /*
    https://leetcode.com/problems/container-with-most-water/

    https://dev.to/seanpgallivan/solution-container-with-most-water-1907

    https://www.geeksforgeeks.org/container-with-most-water/

    https://leetcode.com/problems/deepest-leaves-sum/ 
    */

    #region Solution
    public class Solution
    {
        public Solution()
        {

        }

        public int MaxArea(int[] heights)
        {
            if (heights == null || heights.Length < 2)
                return 0; 
            
            int maxArea = 0;

            for (var x = 0; x < heights.Length - 1; x++)
            {
                for (var next = x + 1; next < heights.Length; next++)
                {
                    // Calculate the area between the two markers
                    int width = next - x;
                    if (width <= 0)
                        continue; // No area if width is zero or negative

                    int height = Math.Min(heights[x], heights[next]); // Smaller of the two heights
                    int area = width * height; 

                    // Update maxArea if this is larger
                    if (area > maxArea)
                        maxArea = area;
                }
            }

            return maxArea;
        }
    }
    #endregion

    #region Tests
    public class Tests
    {
        public Tests()
        {
        }

        [Theory]
        [InlineData(new int[] { 1, 8, 6, 2, 5, 4, 8, 3, 7 }, 49)]
        [InlineData(new int[] { 1, 1 }, 1)]
        public void Given_Markers_ShouldBeMaxSize(int[] heights, int expectedMaxArea)
        {
            // ARRANGE
            var solution = new Solution();

            // ACT
            var maxArea = solution.MaxArea(heights);
                
            // ASSERT
            maxArea.Should().Be(expectedMaxArea);

        }

        [Theory]
        [InlineData(null, 0)]
        [InlineData(new int[] { }, 0)]
        public void Given_EmptyOrNullArray_ShouldReturnZero(int[] heights, int expectedMaxArea)
        {
            // ARRANGE
            var solution = new Solution();
         
            // ACT
            var maxArea = solution.MaxArea(heights);
            
            // ASSERT
            maxArea.Should().Be(expectedMaxArea);
        }
    }

    #endregion
}
