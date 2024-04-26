using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.UI
{
    public interface IPanel
    {
        /// <summary>
        /// 初始化UI ,用于获取UI元素、系统等
        /// </summary>
        public void InitPanel();
        ///// <summary>
        ///// 打开UI
        ///// </summary>
        ///// <param name="uiData"></param>
        //public void Show(IUiData uiData);
        /// <summary>
        /// 刷新UI数值
        /// </summary>
        public void Refresh();
        /// <summary>
        /// 关闭
        /// </summary>
        public void Close();

    }

    public interface IUiData
    {

    }
}
