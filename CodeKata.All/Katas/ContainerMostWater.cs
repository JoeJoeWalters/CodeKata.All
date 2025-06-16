using AwesomeAssertions;
using System;
using Xunit;

namespace Katas.ContainerMostWater
{
    /*
 
    You are given an integer array height of length n. There are n vertical lines drawn such that the two endpoints of the ith line are (i, 0) and (i, height[i]).

    Find two lines that together with the x-axis form a container, such that the container contains the most water.

    Return the maximum amount of water a container can store.

    Notice that you may not slant the container.
    
    |             |
    | |     |   | |
    | | | | | | | |

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
