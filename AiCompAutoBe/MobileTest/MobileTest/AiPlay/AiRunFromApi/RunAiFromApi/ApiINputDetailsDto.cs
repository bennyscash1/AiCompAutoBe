using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiCompAutoBe.MobileTest.MobileTest.AiPlay.AiRunFromApi.RunAiFromApi
{


    public class TestInputData
    {
        public string RuningApp { get; set; }
        public string UrlForChrome { get; set; }="";
        public List<AiTasksList>? AiTasksList { get; set; }

        public List<StepInstruction>? TestInputSteps { get; set; }
    }
    public class StepInstruction
    {
        public string ElementView { get; set; }
        public string InputText { get; set; } = "";
    }
    public class AiTasksList
    {
        public string TaskStep { get; set; }
    }

    public class CreateFileInputDto
    {
        public string RuningApp { get; set; }
        public string RecordFileName { get; set; }
    }
}
