using System;
using System.Collections.Generic;
using System.Linq;
using WS.Contracts.Contracts.Dtos.Categories;

namespace WS.Logic.Products.Extensions
{
    public static class CategoryExtensions
    {
        public static List<CategorySimpleDto> SortByParentageAndAssignDepth(this List<CategorySimpleDto> categories)
        {

            var sorted = RecursiveParentageSort(categories, categories.FirstOrDefault(c => c.ParentId == null), 0);
            categories = sorted;
            return categories;
        }

        private static List<CategorySimpleDto> RecursiveParentageSort(List<CategorySimpleDto> categories, CategorySimpleDto root, int depth)
        {
            if (root == null)
            {
                throw new ArgumentException("Root can never be null");
            }

            root.Depth = depth;
            var sorted = new List<CategorySimpleDto> { root };

            var rootDirectChildren = categories.Where(c => c.ParentId == root.Id).ToList();

            var increadedDepth = depth + 1;
            foreach (var child in rootDirectChildren)
            {
                sorted.AddRange(RecursiveParentageSort(categories, child, increadedDepth));
            }

            return sorted;
        }
    }
}
