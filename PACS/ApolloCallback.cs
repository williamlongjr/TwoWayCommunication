using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace PACS
{
    class ApolloCallback : IMeasurementOperationsCallback,IDisposable
    {
        //private ApolloService proxy;

        public delegate void eventHandler(ApolloCallback a, CallBackEventArgs e);
        public event eventHandler eventCallback;


	    public ApolloCallback()
	    {
		    
	    }

        public void Dispose()
        {
            throw new NotImplementedException();
        }


		void IMeasurementOperationsCallback.MeasurementCallback(string str)
		{
			eventCallback(this, new CallBackEventArgs(str));
		}
	}

	public class CallBackEventArgs:EventArgs
	{
		public String Str { get; set; }

		public CallBackEventArgs(string str)
		{
			Str = str;
		}
	}

}
