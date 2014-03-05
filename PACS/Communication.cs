using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Channels;

using Common;

namespace PACS
{


	class Communication
	{
		ApolloService mApolloService;
		public delegate void eventHandler(string str);
		public event eventHandler eventCallback;

		private ApolloCallback mApolloCallback;

		public Communication()
		{
			EndpointAddress endpoint = new EndpointAddress("net.tcp://localhost:8080/ApolloService/DuplexMEP");
			mApolloService = new ApolloService(new InstanceContext(mApolloCallback), GetBindingFromName("NetTcp"), endpoint);

			mApolloCallback = new ApolloCallback();
			mApolloCallback.eventCallback += mApolloCallback_eventCallback;
		}

		void mApolloCallback_eventCallback(ApolloCallback a, CallBackEventArgs e)
		{
			eventCallback(e.Str);
		}

		public string GetMeasurementRecords(string studyUID)
		{

            NetTcpBinding binding;
            binding = new NetTcpBinding();

            EndpointAddress endpoint = new EndpointAddress("net.tcp://localhost:8080/ApolloService/DuplexMEP");

            MeasurementOperationsClient client = new MeasurementOperationsClient(
                new InstanceContext(mApolloCallback), binding, endpoint);

            return client.GetMeasurementRecords(studyUID);

			//return mApolloService.GetMeasurementRecords(studyUID);
		}

		public void NormalFunction(string str)
		{
			MeasurementOperationsClient client = new MeasurementOperationsClient(
				new InstanceContext(new ApolloCallback()));
			client.NormalFunction(str);

		}

		private static Binding GetBindingFromName(string bindingType)
		{
			System.ServiceModel.Channels.Binding b = null;

			switch (bindingType)
			{
				case "BasicHttp":
					b = new BasicHttpBinding();
					break;
				case "NetNamedPipe":
					b = new NetNamedPipeBinding();
					break;
				case "NetTcp":
					b = new NetTcpBinding();
					break;
			}
			return b;
			//return new WSDualHttpBinding();
		}

	}

	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
	public class ApolloService : DuplexClientBase<IMeasurementOperations>, IMeasurementOperations
	{
		//public delegate void eventHandler(string str);
		//public event eventHandler eventCallback;

		public ApolloService(System.ServiceModel.InstanceContext callbackInstance) :
			base(callbackInstance)
		{
		}

		public ApolloService(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) :
			base(callbackInstance, endpointConfigurationName)
		{
		}

		public ApolloService(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) :
			base(callbackInstance, endpointConfigurationName, remoteAddress)
		{
		}

		public ApolloService(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
			base(callbackInstance, endpointConfigurationName, remoteAddress)
		{
		}

		public ApolloService(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
			base(callbackInstance, binding, remoteAddress)
		{
		}

		public void NormalFunction(string str)
		{
			base.Channel.NormalFunction(str);
		}

		public Task NormalFunctionAsync(string str)
		{
			return base.Channel.NormalFunctionAsync(str);
		}

		public string GetMeasurementRecords(string studyUID)
		{
			return base.Channel.GetMeasurementRecords(studyUID);
		}

		public Task<string> GetMeasurementRecordsAsync(string studyUID)
		{
			return base.Channel.GetMeasurementRecordsAsync(studyUID);
		}
	}
}
