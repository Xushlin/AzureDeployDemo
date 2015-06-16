using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace MyWCFService
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string ShowName(WCFModel model);
    }

    [DataContract]
    public class WCFModel
    {
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
    }
}
