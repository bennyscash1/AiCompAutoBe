using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiCompAutoBe.MobileTest.MobileTest.RecordAndPlay
{
    public static class RecordingSessionStore
    {
        public static Process? CurrentRecordingProcess { get; set; }
        public static string CurrentRecordingFile { get; set; }

    }

}
