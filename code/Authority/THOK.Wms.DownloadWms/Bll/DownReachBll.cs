using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using THOK.Util;
using THOK.WMS.DownloadWms.Dao;

namespace THOK.WMS.DownloadWms.Bll
{
    public class DownReachBll
   {
       #region 从营销系统下载送货区域
       /// <summary>
       /// 下载送货区域表
       /// </summary>
       /// <returns></returns>
       public bool DownReachInfo()
       {
           bool tag = true;
           DataTable reachCodeDt = this.QueryReachCode();
           string reachCodeList = UtinString.MakeString(reachCodeDt, "DIST_STA_CODE");
           reachCodeList = "DIST_STA_CODE NOT IN(" + reachCodeList + ")";
           DataTable reachDt = this.GetReachInfo(reachCodeList);
           if (reachDt.Rows.Count > 0)
               this.Insert(reachDt);
           else
               tag = false;

           return tag;
       }

       /// <summary>
       /// 下载送货区域表
       /// </summary>
       /// <returns></returns>
       public bool GetDownReachInfo()
       {
           bool tag = true;
           this.Delete();//下载前清楚哦送货区域表
           DataTable reachDt = this.GetReachInfo();
           if (reachDt.Rows.Count > 0)
               this.Insert(reachDt);
           else
               tag = false;
           return tag;
       }


       /// <summary>
       /// 清除送货区域表
       /// </summary>
       public void Delete()
       {
           using (PersistentManager dbPm = new PersistentManager())
           {
               DownReachDao dao = new DownReachDao();
               dao.Delete();
           }
       }

       /// <summary>
       /// 下载送货区域
       /// </summary>
       /// <returns></returns>
       public DataTable GetReachInfo()
       {
           using (PersistentManager dbPm = new PersistentManager("YXConnection"))
           {
               DownReachDao dao = new DownReachDao();
               dao.SetPersistentManager(dbPm);
               return dao.GetReachInfo();
           }
       }

       /// <summary>
       /// 根据编号下载送货区域
       /// </summary>
       /// <returns></returns>
       public DataTable GetReachInfo(string reachCode)
       {
           using (PersistentManager dbPm = new PersistentManager("YXConnection"))
           {
               DownReachDao dao = new DownReachDao();
               dao.SetPersistentManager(dbPm);
               return dao.GetReachInfo(reachCode);
           }
       }

       /// <summary>
       /// 查询送货区域编码
       /// </summary>
       /// <returns></returns>
       public DataTable QueryReachCode()
       {
           using (PersistentManager dbPm = new PersistentManager())
           {
               DownReachDao dao = new DownReachDao();
               return dao.QueryReachCode();
           }
       }

        /// <summary>
       /// 添加数据到数据库
       /// </summary>
       /// <param name="reachDt"></param>
       public void Insert(DataTable reachDt)
       {
           using (PersistentManager dbPm = new PersistentManager())
           {
               DownReachDao dao = new DownReachDao();
               dao.Insert(reachDt);
           }
       }
       #endregion
   }
}
