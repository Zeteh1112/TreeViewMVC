using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TreeViewMVC.Models;
using WebGrease.Css.Ast.Selectors;
using static TreeViewMVC.Models.Index;

namespace TreeViewMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            //var model = new Index
            //{
            //    Tree = TreeView()
            //};
            ////var model = GetTreeData();
            //return View(model);
            TreeView();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult TreeView()
        {
            // Step 1: Initialize database connection and query
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string selectQuery = @"SELECT DISTINCT MC_BRANDS, MC_TYPES, MC_CC FROM MOTORCYCLESDATA";  // Fetch only distinct brands

            // Step 2: Create a list to hold the motorcycle brands
            var brandNodes = new List<TreeNode>();
            var subbrandNodes = new List<TreeNode>();
            var typeNodes = new List<TreeNode>();
            var typesubNodes = new List<TreeNode>();


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(selectQuery, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    subbrandNodes.Add(new TreeNode
                    {
                        Text = "Type",
                        Value = "",
                        Children = typeNodes
                    });
                    while (reader.Read())
                    {
                        brandNodes.Add(new TreeNode
                        {
                            Text = reader["MC_BRANDS"].ToString(),
                            Value = reader["MC_BRANDS"].ToString(),
                            Children = subbrandNodes
                        });
                        typeNodes.Add(new TreeNode
                        {
                            Text = reader["MC_TYPES"].ToString(),
                            Value = reader["MC_TYPES"].ToString(),
                            Children = typesubNodes
                        });
                        typesubNodes.Add(new TreeNode
                        {
                            Text = reader["MC_CC"].ToString(),
                            Value = reader["MC_CC"].ToString(),
                        });
                    }
                }
            }


            // Step 4: Create a root node ("Motorcycles") and assign the fetched brands as its children
            var motorcycleTree = new List<TreeNode>
            {
                new TreeNode
                {
                    Text = "Motorcycles",
                    Value = "1",
                    Children = brandNodes
                }
            };


            // Step 5: Pass the tree structure to the view
            ViewBag.MotorcycleTree = motorcycleTree;
            return View(motorcycleTree);
        }
    }
}