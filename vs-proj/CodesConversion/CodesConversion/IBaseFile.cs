using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CodesConversion
{
    public interface IBaseFile : IEnumerable<Code>
    {
        bool ParseFile();
        TreeNode BuildTree();
        bool ConvertFile(IBaseFile file);

        int CodeCount
        {
            get;
        }

        Code this[int index]
        {
            get;
        }

        int MaxInstances
        {
            get;
        }
    }
}
