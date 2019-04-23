using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirmaMVC.CustomClasses;
using FirmaMVC.Models;
using FirmaMVC.Models.Employee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirmaMVC.Controllers
{
    public class DepartmentController : Controller
    {
        DBContextModel context = new DBContextModel();
        public IActionResult Index()
        {
             var query = (from d in context.Department
                         select new Department 
                         {
                             DepartmentId = d.DepartmentId,
                             Name = d.Name,
                             Parent = d.Parent,
                             Hierarchy = d.Hierarchy
                         }).
                         OrderBy(d => d.Hierarchy).ToList();

            List<Department> depart = new List<Department>();
            AnalyzeHierarchy ah = new AnalyzeHierarchy();
            List<int> zapamietajID = new List<int>();

            int currentParentId = query[0].Parent;
            int rememberParentId = 0;
            bool firstLoop = false;
            bool whenBothCheckChildrenFalse = false;
            Department rememberOmittedDepartment = null;
            int iStart = 0;

            for (int z = 0; z < 20; z++)
            {
                if (firstLoop == true)
                {
                    for (int i = 0; i < zapamietajID.Count ; i++)
                    {
                        if (query[i].DepartmentId != zapamietajID[i])
                        {
                            currentParentId = query[i].Parent;
                            break;
                        }
                    }
                }
                firstLoop = true;

                for (int i = iStart; i < query.Count; i++)
                {
                    whenBothCheckChildrenFalse = false;

                    if (zapamietajID.Contains(query[i].DepartmentId))
                    {
                        continue;
                    }

                    //if (rememberOmittedDepartment != null)
                    //{
                    //    zapamietajID.Add(rememberOmittedDepartment.DepartmentId);
                    //    depart.Add(rememberOmittedDepartment);
                    //    //currentParentId = rememberOmittedDepartment.DepartmentId;
                    //    //currentParentId = query[i].DepartmentId;
                    //    rememberOmittedDepartment = null;
                    //    continue;
                    //}

                    if (ah.czyMaszDzieci(query[i].DepartmentId, query) == true)
                    {
                        if(currentParentId == query[i].Parent)
                        {
                            zapamietajID.Add(query[i].DepartmentId);
                            depart.Add(query[i]);
                            currentParentId = query[i].DepartmentId;
                            if (rememberParentId != query[i].Parent)
                            {
                                rememberParentId = currentParentId;
                            }
                            continue;
                        } 
                    }
                    else if (ah.czyMaszDzieci(query[i].DepartmentId, query) == false)
                    {
                        if (currentParentId == query[i].Parent)
                        {
                            zapamietajID.Add(query[i].DepartmentId);
                            depart.Add(query[i]);
                            continue;
                        } 
                        if (rememberParentId == query[i].Parent)
                        {
                            whenBothCheckChildrenFalse = true;
                        }
                    }

                    if (whenBothCheckChildrenFalse == true)
                    {
                        //rememberOmittedDepartment = query[i];
                        iStart = i - 1;
                        break;
                    }
                }
            }
            return View(depart);
        }

        [HttpPost]
        public IActionResult Add(int Id, string Name)
        {
            var depart = context.Department
                .Where(d => d.DepartmentId == Id)
                .Select(d => new {DepartId = d.DepartmentId, DepartHier = d.Hierarchy, DepartParent = d.Parent }).FirstOrDefault();

            var department = context.Set<Department>();
            department.Add(new Department { Name = Name, Hierarchy = depart.DepartHier + 1,Parent=depart.DepartId });
            context.SaveChanges();
            return RedirectToAction("Index", "Department");
        }

        public IActionResult Delete(int id)
        {
            var query = (from d in context.Department
            select new Department 
            {
                DepartmentId = d.DepartmentId,
                Name = d.Name,
                Parent = d.Parent,
                Hierarchy = d.Hierarchy
            }).
            OrderBy(d => d.Parent).ToList();

            AnalyzeHierarchy ah = new AnalyzeHierarchy();

            if (ah.czyMaszDzieci(id, query) == true)
            {
                return RedirectToAction("Index", "Department");
            }
            else if (ah.czyMaszDzieci(id, query) == false)
            {
                var department = context.Set<Department>();
                department.Remove(new Department { DepartmentId = id });

                try
                {
                    context.SaveChanges();
                    return RedirectToAction("Index", "Department");
                }
                catch (Exception e)
                {
                    return RedirectToAction("Index", "Department");
                }
            }
            return RedirectToAction("Index", "Department");
        }

        public JsonResult ReturnJsonDepartmentsHierarchy()
        {
            List<DepartmentDiagram> diagram = new List<DepartmentDiagram>();

            var depart = context.Department
                .Select(d => new Department
                {
                    DepartmentId = d.DepartmentId,
                    Name = d.Name,
                    Hierarchy = d.Hierarchy,
                    Parent = d.Parent
                }).
                OrderBy(d => d.Parent).ToList();

            var lastHierarchyResult = (from max in context.Department
                    orderby max.Hierarchy descending
                    select max.Hierarchy).Take(1).ToArray();

            int lastHierarchy = lastHierarchyResult[0];
            int firstInHierarchy = 1;
            int count = depart.Count();
            int counter = 0;
            for (int i = firstInHierarchy; i <= lastHierarchy; i++)
            {
                int howManyInHierarchy = (from c in depart where c.Hierarchy == i select c.Hierarchy).Count();
                for (int j = 0; j < howManyInHierarchy; j++)
                {
                    diagram.Add(new DepartmentDiagram { Department = depart[counter], PositionX = i *50, PositionY = j *50 });
                    counter++;
                }
            }
            return Json(diagram);
        }
    }
}