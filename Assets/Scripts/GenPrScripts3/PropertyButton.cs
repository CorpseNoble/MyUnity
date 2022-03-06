using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenPr3
{
    [Serializable]
    public class PropertyButton
    {
        public Action action;

        public PropertyButton(Action action)
        {
            this.action = action;
        }

        public void Button_Cklick()
        {
            action?.Invoke();
        }
    }
}
