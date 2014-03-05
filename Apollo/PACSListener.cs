using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;
using Common;

namespace Apollo
{
    public class PACSListener
    {
        ServiceHost mHost;
		PACSListenerService listenerService;

        public void Start(string uri)
        {
            
            listenerService = new PACSListenerService();

            mHost = new ServiceHost(listenerService, new Uri(uri));

            ServiceMetadataBehavior smb;
            smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = false;
            
            mHost.Description.Behaviors.Add(smb);

			//WSDualHttpBinding binding = new WSDualHttpBinding();
			NetTcpBinding binding = new NetTcpBinding();


	        mHost.AddServiceEndpoint(typeof(IMeasurementOperations), binding, "/DuplexMEP");
			//mHost.AddServiceEndpoint(typeof(IMetadataExchange),
			//	MetadataExchangeBindings.CreateMexHttpBinding(),
			//	"/Mex");
			mHost.AddServiceEndpoint(typeof(IMetadataExchange),
			MetadataExchangeBindings.CreateMexTcpBinding(),
			"/Mex");


            mHost.Open();
        }

        public void MeasurementCallback(string str)
        {
			//IMeasurementCallback measurementCallback;
			//measurementCallback = OperationContext.Current.GetCallbackChannel<IMeasurementCallback>();

			//measurementCallback.MeasurementCallback(str);

			listenerService.cb(str);
        }
    }

    [ServiceBehavior(InstanceContextMode=InstanceContextMode.Single)]
    public class PACSListenerService : IMeasurementOperations
    {
		OperationContext operationContext;

		public PACSListenerService()
        {

        }

        public void NormalFunction(string str)
        {
	        int i;
	        i = 0;
        }

        public string GetMeasurementRecords(string studyUID)
        {
			operationContext = OperationContext.Current;
            return "Value=5";
        }


		public void cb(string str)
		{
			IMeasurementCallback measurementCallback;
			//measurementCallback = OperationContext.Current.GetCallbackChannel<IMeasurementCallback>();
			
			measurementCallback = operationContext.GetCallbackChannel<IMeasurementCallback>();
			measurementCallback.MeasurementCallback(str);
		}

    }

}
