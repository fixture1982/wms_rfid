﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using THOK.Wms.Bll.Interfaces;
using THOK.Wms.DbModel;
using THOK.Common.WebUtil;
using THOK.Security;

namespace Wms.Controllers.Wms.BasisInfo
{
    [TokenAclAuthorize]
    public class PositionController : Controller
    {
        [Dependency]
        public IPositionService PositionService { get; set; }
        //
        // GET: /Position/

        public ActionResult Index(string moduleID)
        {
            ViewBag.hasSearch = true;
            ViewBag.hasAdd = true;
            ViewBag.hasEdit = true;
            ViewBag.hasDelete = true;
            ViewBag.hasPrint = true;
            ViewBag.hasHelp = true;
            ViewBag.ModuleID = moduleID;
            return View();
        }
        public ActionResult SearchPage()
        {
            return View();
        }

        public ActionResult AddPage()
        {
            return View();
        }

        //
        // GET: /Position/Details/5

        public ActionResult Details (int page, int rows, FormCollection collection)
        {
            string PositionName = collection["PositionName"] ?? "";
            string PositionType = collection["PositionType"] ?? "";
            string SRMName = collection["SRMName"] ?? "";
            string State = collection["State"] ?? "";

            var path = PositionService.GetDetails(page, rows, PositionName, PositionType,SRMName, State);
            return Json(path, "text", JsonRequestBehavior.AllowGet);
        }
        //
        // POST: /Position/Create

        [HttpPost]
        public ActionResult Create(Position position)
        {
            string strResult = string.Empty;
            bool bResult = PositionService.Add(position, out strResult);
            string msg = bResult ? "新增成功" : "新增失败";
            return Json(JsonMessageHelper.getJsonMessage(bResult, msg, strResult), "text", JsonRequestBehavior.AllowGet);
        }
        
      
        //
        // POST: /Position/Edit/5

        [HttpPost]
        public ActionResult Edit(Position position)
        {
            string strResult = string.Empty;
            bool bResult = PositionService.Save(position, out strResult);
            string msg = bResult ? "修改成功" : "修改失败";
            return Json(JsonMessageHelper.getJsonMessage(bResult, msg, strResult), "text", JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Position/Delete/5

        [HttpPost]
        public ActionResult Delete(int positionId)
        {
            string strResult = string.Empty;
            bool bResult = PositionService.Delete(positionId, out strResult);
            string msg = bResult ? "删除成功" : "删除失败";
            return Json(JsonMessageHelper.getJsonMessage(bResult, msg, strResult), "text", JsonRequestBehavior.AllowGet);
        }
         // POST: /Position/GetPosition/
        public ActionResult GetPosition(int page, int rows, string queryString, string value)
        {
            
            if (queryString == null)
            {
                queryString = "PositionName";
            }
            if (value == null)
            {
                value = "";
            }
            var employee = PositionService.GetPosition(page, rows, queryString, value);
            return Json(employee, "text", JsonRequestBehavior.AllowGet);
        }

        #region /Position/CreateExcelToClient/
        public FileStreamResult CreateExcelToClient()
        {
            int page = 0, rows = 0;
            string positionName = Request.QueryString["positionName"];
            string srmName = Request.QueryString["srmName"];

            THOK.Common.NPOI.Models.ExportParam ep = new THOK.Common.NPOI.Models.ExportParam();
            ep.DT1 = PositionService.GetPosition(page, rows, positionName, srmName,null);
            ep.HeadTitle1 = "位置信息";
            System.IO.MemoryStream ms = THOK.Common.NPOI.Service.ExportExcel.ExportDT(ep);
            return new FileStreamResult(ms, "application/ms-excel");
        } 
        #endregion
    }
}
    
