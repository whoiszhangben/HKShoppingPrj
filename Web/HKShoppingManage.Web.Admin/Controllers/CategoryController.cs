using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Web.Mvc;
using HKShoppingManage.Common;
using HKShoppingManage.Model;
using HKShoppingManage.BLL;

namespace HKShoppingManage.Web.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        private ICategoryBLL bll;
        public CategoryController(ICategoryBLL _bll)
        {
            this.bll = _bll;
        }
        // GET: Catalog
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            List<Category> displayCategory = new List<Category>();
            foreach (var eachItem in CacheManager.CategoryLstCache.Values)
            {
                displayCategory.AddRange(eachItem);
            }
            SelectList selectLst = new SelectList(displayCategory, "Id", "Name");
            ViewData["CategoryLst"] = selectLst;
            return View();
        }
        public JsonResult GetTrees()
        {
            ConcurrentDictionary<int, zTreeNode> nodesLst = new ConcurrentDictionary<int, zTreeNode>();
            List<Category> categoryLst = null;
            foreach (var item in CacheManager.CategoryLstCache.Keys)
            {
                CacheManager.CategoryLstCache.TryGetValue(item, out categoryLst);
                zTreeNode node = new zTreeNode();
                var rootNode = categoryLst.FirstOrDefault(p => p.ParentCategoryId == null);
                node.name = rootNode.Name;
                node.id = rootNode.Id;
                node.open = true;
                node.pId = rootNode.ParentCategoryId;
                node.children = GetChildren(node, categoryLst);
                nodesLst.TryAdd(item, node);
            }
            return Json(new JsonModel(true, "", nodesLst));
        }
        private List<zTreeNode> GetChildren(zTreeNode node,List<Category> lst)
        {
            var childLst = lst.Where(p => p.ParentCategoryId == node.id).ToList();
            for (var i = 0; i < childLst.Count; i++)
            {
                var childNode = childLst[i];
                zTreeNode subNode = new zTreeNode();
                subNode.name = System.Text.RegularExpressions.Regex.Split(childNode.Name, "->").Last();
                subNode.id = childNode.Id;
                subNode.open = true;
                subNode.pId = childNode.ParentCategoryId;
                subNode.children = GetChildren(subNode, lst);
                node.children.Add(subNode);
            }
            return node.children;
        }
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Create(Category model)
        {
            model.CreateTime = DateTime.Now;
            model.UpdateTime = DateTime.Now;
            model.Creator = MvcHelper.User.LoginCode;
            model.Updator = MvcHelper.User.LoginCode;
            if (model.ParentCategoryId != null)
            {
                model.Name = CacheManager.CategoryCache[model.ParentCategoryId ?? 0].Name + "->" + model.Name;
            }
            var result = await this.bll.Insert(model);
            if (result>0)
            {
                model.Id = result;
                //更新类别缓存
                CacheManager.CategoryCache.SyncCache(result, model, false);
                if (model.ParentCategoryId == null)
                {
                    var list = new List<Category>();
                    list.Add(model);
                    CacheManager.CategoryLstCache.TryAdd(result, list);
                }
                else
                {
                    var currentPID = model.ParentCategoryId;
                    foreach (var cacheLst in CacheManager.CategoryLstCache.Values)
                    {
                        if (cacheLst.Exists(p => p.Id == currentPID))
                        {
                            var parentCategory = cacheLst.FirstOrDefault(p => p.Id == currentPID);
                            model.Level = parentCategory.Level + 1;
                            cacheLst.Add(model);
                            break;
                        }
                    }
                }
            }
            return RedirectToAction("Index", "Category");
        }
        [HttpPost]
        public async Task<JsonResult> Delete(int Id)
        {
            var result = await this.bll.Delete(Id);
            return Json(new JsonModel(true, "", result));
        }
        [HttpPost]
        public async Task<JsonResult> Update(Category model)
        {
            model.UpdateTime = DateTime.Now;
            model.Updator = MvcHelper.User.LoginCode;
            var result = await this.bll.Update(model);
            return Json(new JsonModel(true, "", result));
        }
        [HttpPost]
        public async Task<JsonResult> IsExist(string catalogName)
        {
            var result = await this.bll.IsExists(catalogName);
            return Json(new JsonModel(true, "", result));
        }
        [HttpPost]
        public async Task<JsonResult> GetModel(int Id)
        {
            var result = await this.bll.GetModel(Id);
            return Json(new JsonModel(true, "", result));
        }
        [HttpPost]
        public async Task<JsonResult> GetList()
        {
            var result = await this.bll.GetList();
            return Json(new JsonModel(true, "", result));
        }
    }
}