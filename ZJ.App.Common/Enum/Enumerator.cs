using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZJ.App.Common
{
    /// <summary>
    /// 获取枚举描述，例如：
    /// EnumDescription.GetFieldText(TaskStatus.NotStart);
    /// 方法根据当前线程文化，自动获取中英文值
    /// </summary>
    public class Enumerator
    {
        /// <summary>
        /// 
        /// </summary>
        public enum DataType : int
        {
            [EnumDescription("电影", "电影")]
            Movie = 1,
        }


        /// <summary>
        /// 字典表记录软删标识
        /// </summary>
        public enum DictionaryType : int
        {
            [EnumDescription("电影", "电影")]
            Movie = 0,
            [EnumDescription("启用", "启用")]
            Enabled = 1,
        }

        public enum EnablePuzzle : int
        {
            [EnumDescription("关闭", "关闭")]
            Disabled = 0,
            [EnumDescription("开启", "开启")]
            Enabled = 1,
        }

        /// <summary>
        /// 
        /// </summary>
        public enum AccountState : int
        {
            [EnumDescription("停用", "停用")]
            Termination = 2,
            [EnumDescription("使用", "使用")]
            Use = 1,
        }

        public enum Gender : int
        {
            [EnumDescription("女", "女")]
            Female = 2,
            [EnumDescription("男", "男")]
            Male = 1,
            [EnumDescription("不限", "不限")]
            Nolimit = -1
        }

        /// <summary>
        /// 审核状态
        /// </summary>
        public enum AuditState : int
        {
            /// <summary>
            /// 未审核
            /// </summary>
            [EnumDescription("未审核", "未审核")]
            NotAudit = 0,
            /// <summary>
            /// 已审核
            /// </summary>
            [EnumDescription("已审核", "已审核")]
            Audit = 1,
            /// <summary>
            /// 拒绝
            /// </summary>
            [EnumDescription("拒绝", "拒绝")]
            Reject = 2
        }

        

        /// <summary>
        /// 简历表[tbiz_PuzzleBasic] 字段 [IsPublic] 枚举值
        /// </summary>
        public enum IsPublic : int
        {
            /// <summary>
            /// 不公开
            /// </summary>
            [EnumDescription("不公开", "不公开")]
            No = 0,

            /// <summary>
            ///  个人注册可公开
            /// </summary>
            [EnumDescription("可公开", "可公开")]
            Yes = 1,


        }

    }
}