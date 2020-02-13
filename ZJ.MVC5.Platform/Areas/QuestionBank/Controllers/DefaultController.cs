using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using ZJ.App.BLL;
using ZJ.App.Common;
using ZJ.App.Entity;
using ZJ.App.Common.Extension;
using Webdiyer.WebControls.Mvc;
using System.Threading;
using System.Globalization;

namespace ZJ.MVC5.Platform.Areas.QuestionBank
{
    public class DefaultController : Controller
    {
        // GET: QuestionBank/Default
        public ActionResult Index()
        {
            //movie type dropdownList
            tcfg_dictitemBLL dictitemBLL = new tcfg_dictitemBLL();
            List<tcfg_dictitemEntity> subjectTypeSelect = dictitemBLL.GetDictitemEntity("question", "subjectType");
            ViewBag.SubjectType = new SelectList(subjectTypeSelect, "PropertyValue", "PropertyName"); //学科下拉
            return View();
        }

        /// <summary>
        /// 试题列表数据
        /// </summary>
        /// <returns></returns>
        public string GetList()
        {
            tbiz_questionBLL questionBLL = new tbiz_questionBLL();
            List<SqlDbParameter> parms = new List<SqlDbParameter>();
            SqlDbParameter parm = null;
            if (!string.IsNullOrEmpty(Request.Form["MovieFileName"]))
            {
                parm = new SqlDbParameter();
                parm.ColumnName = "MovieFileName";
                parm.ParameterName = "MovieFileName";
                parm.ParameterValue = Request.Form["MovieFileName"].UrlDecode().Trim();
                parm.ColumnType = DbType.String;
                parm.QualificationType = SqlDbParameter.QualificationSymbol.Like;
                parms.Add(parm);
            }

            int recordCount;
            int draw = Convert.ToInt32(Request["draw"]);
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            int page = start / length; //start 初始值0
            DataTable dataTable = questionBLL.GetQuestionDataTablePage(parms, "Id DESC", length, page, out recordCount);
            IDictionary info = new Hashtable();
            info.Add("draw", draw);
            info.Add("recordsTotal", recordCount);
            info.Add("recordsFiltered", recordCount);
            info.Add("data", dataTable);

            var settings = new JsonSerializerSettings() { ContractResolver = new NullToEmptyStringResolver() };
            //Person testPerson = new Person();
            //string t1 =  JsonConvert.SerializeObject(testPerson);
            //string t2 = JsonConvert.SerializeObject(testPerson, settings);
            string t3 = JsonConvert.SerializeObject(info, settings);
            return t3;
        }

        public ActionResult CreateQuestion()
        {
            //movie type dropdownList
            tcfg_dictitemBLL dictitemBLL = new tcfg_dictitemBLL();
            List<tcfg_dictitemEntity> subjectTypeSelect = dictitemBLL.GetDictitemEntity("question", "subjectType");
            ViewBag.SubjectType = new SelectList(subjectTypeSelect, "PropertyValue", "PropertyName"); //学科下拉
            List<tcfg_dictitemEntity> questionTypeSelect = dictitemBLL.GetDictitemEntity("question", "questionType");
            ViewBag.QuestionType = new SelectList(questionTypeSelect, "PropertyValue", "PropertyName"); //学科下拉
            return View();
        }

        [HttpPost]
        public string CreateQuestion(tbiz_questionEntity question)
        {
            
            if (ModelState.IsValid)
            {
                tbiz_questionBLL questionBLL = new tbiz_questionBLL();
                if (question.Id == 0)//Add
                {
                    question.QuestionTitle = question.QuestionTitle.UrlDecode();
                    question.CreateTime = DateTime.Now;
                    questionBLL.Addtbiz_questionEntity(question);
                    return JsonConvert.SerializeObject(new { result = true, message = "", returnUrl = "Index" });
                }
                else
                {
                    tbiz_questionEntity questionIni = questionBLL.Gettbiz_questionEntityById(question.Id);
                    questionIni.QuestionTitle = question.QuestionTitle.UrlDecode();
                    questionIni.SubjectType = question.SubjectType;//学科
                    questionIni.QuestionType = question.QuestionType;//题目类型
                    questionBLL.Updatetbiz_questionEntity(questionIni);
                    return JsonConvert.SerializeObject(new { result = true, message = "update", returnUrl = "/QuestionBank/Default/EditQuestion/"+question.Id });
                }
                
            }
            return JsonConvert.SerializeObject(new { result = false, message = "" });
        }

        public ActionResult EditQuestion(int id)
        {
            tbiz_questionBLL questionBLL = new tbiz_questionBLL();
            tbiz_questionEntity question = questionBLL.Gettbiz_questionEntityById(id);
            //ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", album.GenreId);

            //movie type dropdownList
            tcfg_dictitemBLL dictitemBLL = new tcfg_dictitemBLL();
            List<tcfg_dictitemEntity> subjectTypeSelect = dictitemBLL.GetDictitemEntity("question", "subjectType");
            ViewBag.SubjectType = new SelectList(subjectTypeSelect, "PropertyValue", "PropertyName", question.SubjectType);
            List<tcfg_dictitemEntity> questionTypeSelect = dictitemBLL.GetDictitemEntity("question", "questionType");
            ViewBag.QuestionType = new SelectList(questionTypeSelect, "PropertyValue", "PropertyName", question.QuestionType);
            return View(question);
        }



    }
}