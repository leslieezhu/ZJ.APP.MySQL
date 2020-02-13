using System.Web.Mvc;

namespace ZJ.MVC5.Platform.Areas.QuestionBank
{
    /// <summary>
    /// 题库资料管理
    /// </summary>
    public class QuestionBankAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "QuestionBank";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "QuestionBank_default",
                "QuestionBank/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}