using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiCompAutoBe.MobileTest.MobileTest.AiPlay.AiRunFromApi
{


    public class TestInputData
    {
        public string RuningApp { get; set; }
        public List<StepInstruction>? TestInputSteps { get; set; }
    }
    public class StepInstruction
    {
        public string ElementView { get; set; }
        public string InputText { get; set; } = "";
    }
    #region Input details for ai tasks mission

    public class AiTasksApiInputDto
    {
        public string AiTaskRequest { get; set; }
    }
    #endregion
}
