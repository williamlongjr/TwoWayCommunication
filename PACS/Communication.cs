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

        public Communication()
        {
            mApolloService = new ApolloService("NetTcp", "http//localhost:9000/PACSListener/");
        }

        public string GetMeasurementRecords(string studyUID)
        {
            return mApolloService.GetMeasurementRecords(studyUID);
        }

    }

    public class ApolloService : ClientBase<IMeasurementOperations>, IMeasurementOperations
    {
        public ApolloService(string bindingType, string bindingURI)
            : base(GetBindingFromName(bindingType), new EndpointAddress(bindingURI))
        {
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
        }

        public void NormalFunction(string str)
        {
            base.Channel.NormalFunction(str);
        }

        public string GetMeasurementRecords(string studyUID)
        {
            return base.Channel.GetMeasurementRecords(studyUID);
        }
    }
}
