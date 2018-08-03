using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpeechLib;


namespace player
{
    class Recognizer
    {
        SpSharedRecoContext context;
        public Recognizer()
        {
            this.context = new SpSharedRecoContext();
            //TODO : needs to be fulfilled            
        }

        private void Context_Hypothesis(int StreamNumber, object StreamPosition, ISpeechRecoResult Result)
        {
            throw new NotImplementedException();
        }
    }
}
