using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Main.AnjieSi
{
    public partial class index : System.Web.UI.Page
    {

        //bll层
        slidelinkBll slideBll = new slidelinkBll();
        articleBll atBll = new articleBll();
        linkBll lkBll = new linkBll();
        //数据层
        public DataTable _dtlb = new DataTable();//轮播图
        public DataTable _dtNew = new DataTable();
        public DataTable _dtLk = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _dtlb = slideBll.GetList("");
                _dtNew = atBll.GetList("");
                _dtLk = lkBll.GetList("");
            }
        }
    }
}