using System;
using System.Collections.Generic;
using System.Linq;
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
        //    Public Sub Start(activeData As ActiveDataSet, uri As String)
    //    Me.mActiveData = activeData
    //    Dim ads As New PACSListenerService(Me.mActiveData)
    //    mHost = New ServiceHost(ads, New Uri(uri))
    //    Dim smb As New ServiceMetadataBehavior()
    //    smb.HttpGetEnabled = True
    //    mHost.Description.Behaviors.Add(smb)
    //    mHost.Open()
    //End Sub

        public void Start(string uri)
        {
            PACSListenerService listenerService;
            listenerService = new PACSListenerService();

            mHost = new ServiceHost(listenerService, new Uri(uri));

            ServiceMetadataBehavior smb;
            smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;

            mHost.Description.Behaviors.Add(smb);
            mHost.Open();
        }

        public void MeasurementCallback(string str)
        {
            IMeasurementCallback measurementCallback;
            measurementCallback = OperationContext.Current.GetCallbackChannel<IMeasurementCallback>();

            measurementCallback.MeasurementCallback(str);
        }
    }

    [ServiceBehavior(InstanceContextMode=InstanceContextMode.Single)]
    public class PACSListenerService : IMeasurementOperations
    {

        public void NormalFunction(string str)
        {
            
        }

        public string GetMeasurementRecords(string studyUID)
        {
            return "Value=5";
        }
    }

}
