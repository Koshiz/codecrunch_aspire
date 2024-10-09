using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV_test01.AppHost
{
    public interface IOpenAiService
    {
        Task<string> SummarizeCV(string cvText);
    }

}
