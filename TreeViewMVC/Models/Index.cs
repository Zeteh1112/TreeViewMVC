using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TreeViewMVC.Models
{
    public class Index
    {
        public TreeNode Tree { get; set; }
        public class TreeNode
        {
            public string Text { get; set; } 
            public string Value { get; set; }
            public List<TreeNode> Children { get; set; }
        }
    }
}